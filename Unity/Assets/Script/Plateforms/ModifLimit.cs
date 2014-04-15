using UnityEngine;
using System.Collections;

public class ModifLimit : MonoBehaviour {
	
	public float maxTranslateX = 20.0f;
	public float maxTranslateY = 20.0f;
	public float maxTranslateZ = 20.0f;
	
	public float maxScaleX = 100.0f;
	public float maxScaleY = 100.0f;
	public float maxScaleZ = 100.0f;
	public float minScaleX = 1.0f;
	public float minScaleY = 1.0f;
	public float minScaleZ = 1.0f;
	
	[HideInInspector]
	public Vector3 initialPos;
	[HideInInspector]
	public Vector3 initialScale;
	[HideInInspector]
	public bool mustBeStopped;
	
	private bool isColliding;


	// Use this for initialization
	void Start () {
		initialPos = this.transform.position;
		initialScale = this.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(isColliding)
		{
			mustBeStopped = true;	
		}
		else
		{
			mustBeStopped = false;	
		}
		
	
//		Debug.Log (mustBeStopped);
		
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if( collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Cube")
		{
			isColliding = true;
		}	
	}
	
	void OnCollisionExit(Collision collision)
	{
		if( collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Cube")
		{
			isColliding = false;
		}
	}
	
}
