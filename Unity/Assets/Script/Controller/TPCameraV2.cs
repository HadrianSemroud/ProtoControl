using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TPCameraV2 : MonoBehaviour {
	
	public Transform player;
	
	protected Transform aimTarget; // that was public and a gameobject had to be dragged on it. - ben0bi
	
	public float smoothingTime = 10.0f; 
	
	public Vector3 pivotOffset = new Vector3(0.2f, 0.7f,  0.0f); 
	public Vector3 camOffset   = new Vector3(0.0f, 0.7f, -3.4f); 
	public Vector3 closeOffset = new Vector3(0.35f, 1.7f, 0.0f);
	
	public Vector3 aimPivotOffset = new Vector3(0.6f, 0.9f,  0.0f); 
	public Vector3 aimCamOffset   = new Vector3(0.0f, 0.25f, -2f); 
	public Vector3 aimCloseOffset = new Vector3(0.0f, 0.0f, 0.0f); 
	
	public float horizontalAimingSpeed = 800f; 
	public float verticalAimingSpeed = 800f;  
	public float maxVerticalAngle = 80f;
	public float minVerticalAngle = -80f;
	
	public float aimMaxVerticalAngle = 80f;
	public float aimMinVerticalAngle = -80f;
	
	public float mouseSensitivity = 0.3f;
		
	private float angleH = 0;
	private float angleV = 0;
	private Transform cam;
	private float maxCamDist = 1;
	private LayerMask mask;
	private Vector3 smoothPlayerPos;
	
	private Vector3 storedPivotOffset ;
	private Vector3 storedCamOffset;
	private Vector3 storedCloseOffset;
	private bool viseeNormal; 
	
	private List<Vector3> nearPoints = new List<Vector3>();
	
	[HideInInspector]
	public bool playerCanRotate;
	
	[HideInInspector]
	public bool playerIsRotatingCamera;
	
	[HideInInspector]
	public bool isColliding;
	
	// Use this for initialization
	void Start () 
	{
		VarInitialize();	
	}
	
	void Update()
	{
		if(this.transform.childCount >0)
		{
			isColliding = this.transform.GetChild(0).GetComponent<CheckCamCollision>().camIsColliding;
		}

		
		if(PlayerPrefs.GetInt("Visee")==0)
		{
			viseeNormal = true;
		}
		else
		{
			viseeNormal = false;	
		}	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		#region CheckMode
		if(this.GetComponent<LockTargets>().enabled == false)
		{
			if((GameObject.FindObjectOfType(System.Type.GetType("TPControllerV2")) as TPControllerV2).isAiming == true)
			{
				this.GetComponent<CrosshairLock>().enabled = true;
				this.GetComponent<ReprogrammingStuff>().enabled = true;
				
				pivotOffset = aimPivotOffset;
				camOffset = aimCamOffset;
				closeOffset = aimCloseOffset;		
			}
			else
			{
				this.GetComponent<CrosshairLock>().enabled = false;
				this.GetComponent<ReprogrammingStuff>().enabled = false;
				
				pivotOffset = storedPivotOffset;
				camOffset = storedCamOffset;
				closeOffset = storedCloseOffset;	
			}
		}
		else
		{
			this.GetComponent<CrosshairLock>().enabled = true;
			this.GetComponent<ReprogrammingStuff>().enabled = true;
		}
		#endregion
		
		#region ifPaused
		if (Time.deltaTime == 0 || Time.timeScale == 0 || player == null) 
		{
			return;
		}
		#endregion
		
		// Si le joueur peut diriger la caméra
		if( playerCanRotate == true)	
		{
			CheckRightStickInput();
		}
		
		//Si on est en train de modifier un objet : l'Objet devient la target de la caméra
        if (((GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).isModifying == true))
		{
			cam.LookAt((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform);

			
		}
		// Sinon, on calcule la position de la cam
        else
		{
			#region ifAiming
			if((GameObject.FindObjectOfType(System.Type.GetType("TPControllerV2")) as TPControllerV2).isAiming == false)
			{
				//Y ne peut pas dépasser un min et un max définis dans l'inspector
				angleV = Mathf.Clamp(angleV, minVerticalAngle, maxVerticalAngle);
			}
			else
			{
				//Y ne peut pas dépasser un min et un max définis dans l'inspector
				angleV = Mathf.Clamp(angleV, aimMinVerticalAngle, aimMaxVerticalAngle);
			}
			#endregion
			
			// Before changing camera, store the prev aiming distance.
			// If we're aiming at nothing (the sky), we'll keep this distance.
			float prevDist = (aimTarget.position - cam.position).magnitude;
			
			#region rotation
			// Set aim rotation
			Quaternion aimRotation = Quaternion.Euler(-angleV, angleH, 0);
			Quaternion camYRotation = Quaternion.Euler(0, angleH, 0);
			
			cam.rotation = aimRotation;
			#endregion
			
			#region Récupère la position du joueur et détermine la position de la caméra
			// Find far and close position for the camera
			smoothPlayerPos = Vector3.Lerp(player.position, player.position, smoothingTime * Time.deltaTime);
			smoothPlayerPos.x = player.position.x;
			smoothPlayerPos.z = player.position.z;
			
			Vector3 farCamPoint = smoothPlayerPos + camYRotation * pivotOffset + aimRotation * camOffset;
			Vector3 closeCamPoint = player.position + camYRotation * closeOffset;
			
			//Distance entre far et close. 
			//Distance = (Vecteur1-Vecteur2).magnitude
			float farDist = Vector3.Distance(farCamPoint, closeCamPoint);
			
			// Smoothly increase maxCamDist up to the distance of farDist
			maxCamDist = Mathf.Lerp(maxCamDist, farDist, 50 * Time.deltaTime);

			#endregion
		
			Vector3 closeToFarDir = (farCamPoint - closeCamPoint) / farDist;

			float aimTargetDist;
			aimTargetDist = Mathf.Max(5, prevDist);

			aimTarget.position = (this.transform.position + this.transform.forward * aimTargetDist);
			
			cam.position = closeCamPoint + closeToFarDir * maxCamDist;
			
			if((GameObject.FindObjectOfType(System.Type.GetType("TPControllerV2")) as TPControllerV2).isAiming == false 
				&&	isColliding == true)
			{
				HandleCamviewCollisions();
			}
			
//			cam.position = HandleCollisionZoom(this.transform.position,aimTarget.position,1.0f,nearPoints);

		}
		
	}
	

	
	void CheckRightStickInput()
	{
	
		//Définition de l'horizontalité entre -1 et 1
		angleH += Mathf.Clamp(Input.GetAxis("R_XAxis_1")  , -1, 1) * horizontalAimingSpeed * Time.deltaTime;
		
		
		if(viseeNormal)
		{
						
			//Définition de la verticalité entre -1 et 1
			angleV += Mathf.Clamp(Input.GetAxis("R_YAxis_1")  , -1, 1) * verticalAimingSpeed * Time.deltaTime;
		}
		else
		{							
			//Définition de la verticalité entre -1 et 1
			angleV += Mathf.Clamp(Input.GetAxis("R_YAxis_2")  , -1, 1) * verticalAimingSpeed * Time.deltaTime;
		}
		
		if (Input.GetAxis("R_XAxis_1") !=0 || Input.GetAxis("R_YAxis_1") != 0 )
		{
			playerIsRotatingCamera = true;				
		}
		else
		{
			playerIsRotatingCamera = false;	
		}
		
	}
	
	void VarInitialize()
	{
		storedPivotOffset = pivotOffset;
		storedCamOffset = camOffset;
		storedCloseOffset = closeOffset;
		
		playerCanRotate = true;
			
		// [edit] no aimtarget gameobject needs to be placed anymore - ben0bi
		GameObject g=new GameObject();
		aimTarget=g.transform;
		
		// Add player's own layer to mask
		mask = 1 << player.gameObject.layer;
		
		// Add Igbore Raycast layer to mask
		mask |= 1 << LayerMask.NameToLayer("Ignore Raycast");
		
		// Invert mask
		mask = ~mask;
		
		cam = this.transform;
		smoothPlayerPos = player.position;
		
		maxCamDist = 3;
	}
	
	
	
	void HandleCamviewCollisions()
	{
		// Setup des rays
		float distTP = Vector3.Distance(this.transform.position, player.position);
		
		Vector3 blV = this.camera.ViewportToWorldPoint(new Vector3(0, 0, this.camera.nearClipPlane));
		Vector3 trV = this.camera.ViewportToWorldPoint(new Vector3(1, 1, this.camera.nearClipPlane));
		Vector3 brV = this.camera.ViewportToWorldPoint(new Vector3(0, 1, this.camera.nearClipPlane));
		Vector3 tlV = this.camera.ViewportToWorldPoint(new Vector3(1, 0, this.camera.nearClipPlane));
		Vector3 camToBL = this.transform.position - blV;
		Vector3 camToTR = this.transform.position - trV;
		Vector3 camToBR = this.transform.position - brV;
		Vector3 camToTL = this.transform.position - tlV;

		RaycastHit hit;
		List<Ray> rayList = new List<Ray>();
		rayList.Add(new Ray(blV, this.transform.forward * distTP));
		rayList.Add(new Ray(trV, this.transform.forward * distTP));
		rayList.Add(new Ray(brV, this.transform.forward * distTP));
		rayList.Add(new Ray(tlV, this.transform.forward * distTP));
		
		rayList[0] = new Ray(rayList[0].origin + this.transform.forward * distTP, -rayList[0].direction * distTP);
		rayList[1] = new Ray(rayList[1].origin + this.transform.forward * distTP, -rayList[1].direction * distTP);
		rayList[2] = new Ray(rayList[2].origin + this.transform.forward * distTP, -rayList[2].direction * distTP);
		rayList[3] = new Ray(rayList[3].origin + this.transform.forward * distTP, -rayList[3].direction * distTP);
		
		// Select ray le plus loin
		float minRatio = 1.0f;
		int cIndex = 0;
		
		for(int i = 0; i < 4; i++)
		{
			if(Physics.Raycast(rayList[i], out hit, distTP))
			{
				float distToHit = Vector3.Distance(rayList[i].origin, hit.point);
				float iRatio = distToHit/distTP;
				minRatio = Mathf.Min(iRatio, minRatio);
				
				if(minRatio == iRatio)
				{
					cIndex = i;
				}
			}
		}
		
		// Trouver le point
		Vector3 camNPoint = new Vector3(0, 0, 0);
		
		if(minRatio != 1.0f)
		{
			if(Physics.Raycast(rayList[cIndex], out hit, distTP))
			{
//				Debug.DrawRay(rayList[cIndex].origin, rayList[cIndex].direction * distTP, Color.cyan);
				camNPoint = hit.point;
			}
		}
		
		// Set la caméra selon le ray choisi
		switch(cIndex)
		{
			//blV
			case 0:
				camNPoint = camNPoint + camToBL;
				break;
			
			//trV
			case 1:
				camNPoint = camNPoint + camToTR;
				break;
			
			//brV
			case 2:
				camNPoint = camNPoint + camToBR;
				break;
			
			//tlV
			case 3:
				camNPoint = camNPoint + camToTL;
				break;
		}
		
		if(minRatio != 1.0f)
		{
			cam.position = camNPoint;
		}
	}
	

}
