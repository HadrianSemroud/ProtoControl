using UnityEngine;
using System.Collections;

public class CheckCamCollision : MonoBehaviour {
	
	[HideInInspector]
	public bool camIsColliding;
	
	[HideInInspector]
	public GameObject targetColliding;
	
	
	
	// Use this for initialization
	void Start () {
		camIsColliding = false;
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log (camIsColliding);
	}
	
	
	void OnCollisionEnter(Collision col)
	{
		camIsColliding = true;
		targetColliding = col.gameObject;
		
//		Debug.Log ("Camera Collide!");
	}
	
	void OnCollisionExit(Collision col)
	{
		camIsColliding = false;
	}
	
}
