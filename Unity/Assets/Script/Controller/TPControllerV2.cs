using UnityEngine;
using System.Collections;

public class TPControllerV2 : MonoBehaviour {
	
	
	//Private
	private float XControllerAxis;
	private float YControllerAxis;
	private Vector3 stickDirection;
	private Vector3 movementDirection;
	
	private Vector3 previousAngle;
	private Vector3 cameraForward;
	private Vector3 cameraRight;
	private Vector3 composedTranslate;
	
	private bool playerIsReprogramming;
	private bool playerIsAiming;
	
	private bool isJumping;
	private bool jumpIsPressed;
	private bool softLockActivated;

	[HideInInspector]
	public bool isMoving;
	
	[HideInInspector]
	public bool isAiming;
	
	[HideInInspector]
	public bool canMove;
	
	[HideInInspector]
	public bool canBoost;
	[HideInInspector]
	public bool boostIsPressed;
	
	[HideInInspector]
	public bool isGrabbing;
	
	
	//Public
	public float movementSpeed;
	public float aimMovementSpeed;
	public float storedMovementSpeed;
	public float smoothRotation;
	
	public float boostCoef;
	
	public Transform refCam;
	public Transform aimCamera;
	public Transform playerGraphics;
	
	public float jumpHeight;
	public float jumpImpulse;
	public float risingTime;
	public float airControl;
	private float rising;
	private int peak;
	private float height; 
	private float length;   
	
	// Use this for initialization
	void Start () {
		VarInitialize();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
//		Debug.Log ("can Move : " + canMove);
		
		groundCheck ();
		CheckInputs();
		GetExternVar();
		JumpCheck();

		if((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).isModifying == true || isAiming)
		{
			Vector3 dirTarget = refCam.forward;
			dirTarget.y = 0;
			playerGraphics.forward = dirTarget;
		}
		
		if(stickDirection != Vector3.zero && canMove == true)
		{
			//Player movement
			Vector3 modifiedDirRight = refCam.transform.right;
			modifiedDirRight.y = 0;
			
			Vector3 modifiedDirForward = refCam.transform.forward;
			modifiedDirForward.y = 0;
			
			Vector3 xTranslate = modifiedDirRight * XControllerAxis;
			Vector3 yTranslate = modifiedDirForward * YControllerAxis;
			composedTranslate = Vector3.Lerp(xTranslate, yTranslate, 0.5f);
			
			composedTranslate = Vector3.Normalize(composedTranslate);
			composedTranslate.y = 0;
			
			if(isAiming)
			{
				movementSpeed = aimMovementSpeed;
			}
			else
			{
				movementSpeed = storedMovementSpeed;
			}
			
			if (!isJumping && !Physics.Raycast(transform.position, composedTranslate, length))
			{
				this.transform.Translate(composedTranslate * Time.deltaTime * movementSpeed);
			}
			else if (!Physics.Raycast(transform.position, composedTranslate, length))
			{
				this.transform.Translate(composedTranslate * Time.deltaTime * movementSpeed*(airControl/100));

			}
			//Player graphic rotation
			if (composedTranslate != Vector3.zero && !isAiming)
			{
				Quaternion newRotation = Quaternion.LookRotation(composedTranslate);
				playerGraphics.rotation = Quaternion.Slerp(playerGraphics.rotation, newRotation, Time.deltaTime * smoothRotation);
			}			
		}		
		else
		{
			isMoving = false;			
		}
		
	}
	

	void CheckInputs()
	{
		//Check jump input
		if((Input.GetButtonDown("A_1")) && (isJumping == false) &&  playerIsReprogramming == false)
		{
			Jump();
		}
		
		XControllerAxis = Input.GetAxis("L_XAxis_1");
		YControllerAxis = Input.GetAxis("L_YAxis_1");			
		stickDirection = new Vector3(-XControllerAxis, 0 , YControllerAxis);
		
		if(softLockActivated == false)
		{
			if(Input.GetAxis("TriggersL_1") >= 0.9)
			{
				isAiming = true;
			}
		
			else if(playerIsReprogramming == false)
			{
				if((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify)
				{
					(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.enabled = false;
				}				
				isAiming = false;	
			}
		}
	}
		
	void GetExternVar()
	{
		playerIsReprogramming = (GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).isModifying;
		softLockActivated = (GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).softLockEnabled;
    }
	
	void VarInitialize()
	{
		aimMovementSpeed = movementSpeed / 2;
		storedMovementSpeed = movementSpeed;
		isMoving = false;
		isGrabbing = false;
		canMove = true;
		height = (this.height/2)+1f;
		length = (this.length/2)+1f;
	}
	
	//Setters
	public void FreezeMovement()
	{
		canMove = false;	
		
	}
	
	public void FreeMovement()
	{
		canMove = true;	
	}
	
	void groundCheck()
	{
		//Check if caracter is grounded
		if (Physics.Raycast(transform.position, Vector3.down, height))
		{
			rigidbody.velocity = new Vector3(0,rigidbody.velocity.y,0);
			isJumping = false;	
			//FreeMovement();
		}
	}
	
	
	// Check death
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Water" || other.gameObject.tag == "enemy")
		{
			Application.LoadLevel("GameOver");
		}
		
		
	}
	
	// Check if level finished
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "LastCube")
		{
			Application.LoadLevel("Finish");
		}
		
		
	}

	
	/*void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.tag == "GrabZone")
		{
//			Debug.Log ("In GrabZone");
			
			Ray grabbingRay = new Ray(this.transform.GetChild(2).transform.position, this.transform.forward);
			RaycastHit hitTarget;
			
			if(Physics.Raycast(grabbingRay, out hitTarget,20))
			{
//				Debug.Log (hitTarget.transform.tag);
				
				if(hitTarget.transform.tag == "Cube")
				{
					isGrabbing = true;	
				}
				else
				{
					isGrabbing = false;
				}
			}
			
		}
		else
		{
			isGrabbing = false;
		}
		
	} */

	void Jump()
	{
		rising = 0;
		peak = 0;
		jumpIsPressed = false;
		isJumping = true;
		if (stickDirection != Vector3.zero)
		{
			this.rigidbody.AddForce(composedTranslate * Time.deltaTime * movementSpeed*jumpImpulse, ForceMode.Impulse);
		}
		//Jump force up an forward
//		this.rigidbody.AddForce(Vector3.up * jumpHeight*Time.deltaTime*1000 + this.transform.forward*jumpSpeed*Time.deltaTime*1000);
		
	}
	
	void JumpCheck()
	{
		
		// jump prototypal
		if (isJumping)
		{
			
			if (rising < risingTime)
			{
				this.transform.position += ((new Vector3(0, jumpHeight/risingTime, 0))/10)*((risingTime+1)/(rising+1));
				//this.rigidbody.AddForce(new Vector3(0,jumpHeight,0),ForceMode.VelocityChange);
				rising +=1;
			}
			/*else if (rising == risingTime)
			{
				
				//Debug.Log ("woohoopeak");
				rising +=1;
			}
			else 
			{
				
				
			}*/
		}
	}
	
}
