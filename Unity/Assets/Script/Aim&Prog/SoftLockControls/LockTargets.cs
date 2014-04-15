using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LockTargets : MonoBehaviour {
	
	//Public
	public GUITexture crosshairGUI;
	public Camera refCam;
	
	//Private
	[HideInInspector]
	public List<GameObject> targetList = new List<GameObject>();
	[HideInInspector]
	public List<GameObject> allPlatformsList = new List<GameObject>();
	
	[HideInInspector]
	public int idCurrentTarget; // In targetList
	
	private int lastCount;
	private int currentCount;

	private float targetDistance;
	
	private float textHeightRef;
	private float textWidthRef;
	private float pixelInsetHeight;
	private float pixelInsetWidth;
	private float pixelInsetX;
	private float pixelInsetY;
	
	private Vector3 targetOnViewPortPosition;
	
	private bool nextTarget;
	private bool previousTarget;
		
	private GameObject[] gameObjectList;
	
	// Use this for initialization
	void Start () {
		VarInitializer();
	}
	
	void VarInitializer()
	{
		lastCount = 0;
		currentCount = 0;
		idCurrentTarget = 0;
		nextTarget = false;
		previousTarget = false;
		
		textWidthRef = crosshairGUI.pixelInset.width;
		textHeightRef = crosshairGUI.pixelInset.height;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		GetPlatforms();	
		GetInFramePlatforms();
		
		CheckInputs();
		
		#region Debug Logs
//		Debug.Log("Crosshair X : " + crosshairX + " ; Crosshair Y : " + crosshairY);
//		Debug.Log ("distance : " + targetDistance);
//		Debug.Log("currentCount : " + currentCount);
//		Debug.Log ("lastCount : " + lastCount);
//		Debug.Log ("allPlatformsList : " + allPlatformsList.Count);
//		Debug.Log ("currentIndex : " + idCurrentTarget);
//		Debug.Log("targetList Count : " + targetList.Count);
//		int i = 0;
//		foreach(GameObject obj in targetList)
//		{
//			if(obj)
//			{
//				Debug.Log (obj.transform.tag + " " + i);
//				i++;
//			}
//		}
		#endregion
		
	}
	
	void LateUpdate()
	{
		SetIndexTarget();
		PlaceCrosshair();
	}
	
	void PlaceCrosshair()
	{
		if(targetList.Count != 0)
		{
			if(idCurrentTarget < targetList.Count && idCurrentTarget >=0)
			{
				targetOnViewPortPosition = refCam.WorldToViewportPoint(targetList[idCurrentTarget].transform.position);
//				targetList[idCurrentTarget].transform.GetChild(0).light.enabled = true;
			}
			else
			{
				idCurrentTarget = 0;
				targetOnViewPortPosition = refCam.WorldToViewportPoint(targetList[idCurrentTarget].transform.position);
//				targetList[idCurrentTarget].transform.GetChild(0).light.enabled = true;
			}
		}
	}
	
		
	void OnGUI()
	{
		if(targetList.Count != 0 
			&& (GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).isModifying == false 
			&& (GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).showCadran == false)
		{		
			targetDistance = targetOnViewPortPosition.z + refCam.GetComponent<TPCameraV2>().camOffset.z;
		
			crosshairGUI.transform.position = targetOnViewPortPosition;
			
			//Scale selon Z
			pixelInsetHeight = textHeightRef / (targetDistance * 0.2f);
			pixelInsetWidth = textWidthRef / (targetDistance * 0.2f);
			pixelInsetX = -(pixelInsetWidth/2);
			pixelInsetY = -(pixelInsetHeight/2);
			
			crosshairGUI.pixelInset = new Rect(pixelInsetX, pixelInsetY, pixelInsetWidth,pixelInsetHeight);
			
			crosshairGUI.gameObject.SetActive(true);
		}
		else
		{
			crosshairGUI.gameObject.SetActive(false);	
		}

	}
	
		
	void CheckInputs()
	{

		if(Input.GetButtonDown("RB_1"))
		{
			nextTarget = true;	
		}
		else if (Input.GetButtonDown("LB_1"))
		{
			
			previousTarget = true;
		}
		else
		{
			nextTarget = false;
			previousTarget = false;
		}
	
	}
	
	void SetIndexTarget()
	{
		if(nextTarget)
		{
			if(idCurrentTarget < targetList.Count-1)
			{
				idCurrentTarget = idCurrentTarget + 1	;
			}
			else
			{
				idCurrentTarget = 0;	
			}
		}
		
		if(previousTarget)
		{
			
			if(idCurrentTarget == 0)
			{
				idCurrentTarget = targetList.Count-1;
			}
			else
			{
				idCurrentTarget = idCurrentTarget - 1;	
			}
			
		}
	
		
	}
	
	void GetPlatforms()
	{
		currentCount = 0;
		gameObjectList = GameObject.FindGameObjectsWithTag("Cube");
		
		foreach(GameObject obj in gameObjectList)
		{
			currentCount = currentCount + 1;
		}
		
		if(currentCount != lastCount)
		{
			allPlatformsList.Clear();
			
			foreach(GameObject obj in gameObjectList)
			{
				allPlatformsList.Add(obj);	
			}
		
			lastCount = currentCount;
		}
	}
	
	void GetInFramePlatforms()
	{
		targetList.Clear();
		
		foreach (GameObject obj in allPlatformsList)
		{
			if(obj.GetComponent<CheckGlitch>().isInFrame == true)
			{
				targetList.Add(obj);
			}
		}
	}

}
