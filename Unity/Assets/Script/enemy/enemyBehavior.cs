using UnityEngine;
using System.Collections;

public class enemyBehavior : MonoBehaviour {

	//Public Var
	public int speedEnemy;
	public int paternMax;
	public GameObject perso;
	public GameObject startPoint;
	public Texture2D deathScreen;

	//Private
	private Vector3 pos;
	private Vector3 initPos;
	private Vector3 firstPos;
	private Vector3 tempPos;
	private Quaternion firstRotation;  
	private bool isMoving;
	private float patternLenght;
	private float tempPatternLenght;
	private bool hasAggro;

	// Use this for initialization
	void Start ()
	{
		initVar();
		createSpawnPoint();
	}

	void initVar()
	{
		initPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
		patternLenght = Vector3.Distance(initPos, pos);
		firstPos = initPos;
		firstRotation = this.transform.rotation;
		isMoving = true;
		hasAggro = false;
	}

	void createSpawnPoint()
	{
		// Create a spawnPoint
		Instantiate(startPoint, initPos, firstRotation);
	}
	
	// Update is called once per frame
	void Update () {
		move();
		storePos();
		checkPattern();	
		testAggro();
	}

	void move()
	{	
		//Enemy is moving every frame in front of him
		if ((isMoving == true) && (hasAggro == false))
		{
			this.transform.Translate(Vector3.forward*speedEnemy*Time.deltaTime);
		}
	}
		
	void checkPattern()
	{
		//Check if enemy is at the max of the distance allowed for the movement pattern
		if (patternLenght > paternMax)
		{
			this.transform.forward = -this.transform.forward;
			resetPattern();
		}
	}

	void chasePlayer()
	{
			hasAggro = true;
			// enemy is chasing the player
			// get the player position
			this.transform.rotation = Quaternion.LookRotation(perso.transform.position - this.transform.position);
			// move the enemy
			this.transform.Translate(Vector3.forward*speedEnemy*Time.deltaTime);		
	}

	void returnToPattern()
	{
		// target the spawnPoint
		this.transform.rotation = Quaternion.LookRotation(firstPos - this.transform.position);
		// move to SpawnPoint
		this.transform.Translate(Vector3.forward*speedEnemy*Time.deltaTime);	
	}
	//Check Pattern lenght every frame
	void storePos()
	{
	 	pos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
	 	patternLenght = Vector3.Distance(initPos, pos);
	}

	//Turn back when pattern is over
	void resetPattern()
	{
		initPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
	}

	void testAggro()
	{
		// test from aggroZone script if aggro is triggered
		if ((GameObject.FindObjectOfType(System.Type.GetType ("aggroZone")) as aggroZone).aggro == true)
		{
			chasePlayer();		
		}

		// player has moved away !
		if (((GameObject.FindObjectOfType(System.Type.GetType ("aggroZone")) as aggroZone).aggro == false) && (hasAggro == true))
		{
			returnToPattern();
		}
	}

	/*void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			
		}
	}*/

	void OnTriggerEnter(Collider other)
	{
		// check collision with spawnPoint to go back to main pattern
		if ((other.gameObject.tag == "spawnZone") && (hasAggro == true))
		{
			this.transform.rotation = firstRotation;
			hasAggro = false;
		}	
	}
}
