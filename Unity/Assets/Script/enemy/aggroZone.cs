using UnityEngine;
using System.Collections;

public class aggroZone : MonoBehaviour {

	//Private
	public bool aggro;

	// Use this for initialization
	void Start () {
		aggro = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			aggro = true;
		}
	
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			aggro = false;
		}
	
	}
}
