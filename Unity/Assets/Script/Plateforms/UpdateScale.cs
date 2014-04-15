using UnityEngine;
using System.Collections;

public class UpdateScale : MonoBehaviour {
	
	public float minScale = 4.0f;
	
	private Transform parent;
	private float parentScaleX;
	private float parentScaleY;
	private float parentScaleZ;
		
	// Use this for initialization
	void Start () {
	
		parent = this.transform.parent.transform;
		
	}
	
	// Update is called once per frame
	void Update () {
	
		parentScaleX = parent.transform.localScale.x;
		parentScaleY = parent.transform.localScale.y;

		if(parentScaleX>= parentScaleY && light.range>= minScale)
		{
		
			light.range = parentScaleX + 1;
			
		}
		else if(parentScaleY >= parentScaleX && light.range >= minScale) 
		{
			light.range = parentScaleY + 1;	
		}
		else if(light.range <= minScale)
		{
			light.range = minScale;
		}
		
	}
}
