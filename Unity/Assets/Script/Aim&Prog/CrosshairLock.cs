using UnityEngine;
using System.Collections;

public class CrosshairLock : MonoBehaviour {
	
	
	//Public

	public Texture2D crosshairNormal;
	public Texture2D crosshairLock;
	public Texture2D cadran;
	public Texture2D cadran2;
	public Texture2D cadran3;
	
	public Texture2D cadran_TranslateX_Normal;
	public Texture2D cadran_TranslateX_Selected;
	public Texture2D cadran_TranslateY_Normal;
	public Texture2D cadran_TranslateY_Selected;
	public Texture2D cadran_TranslateZ_Normal;
	public Texture2D cadran_TranslateZ_Selected;
	
	public Texture2D cadran_ScaleX_Normal;
	public Texture2D cadran_ScaleX_Selected;
	public Texture2D cadran_ScaleY_Normal;
	public Texture2D cadran_ScaleY_Selected;
	public Texture2D cadran_ScaleZ_Normal;
	public Texture2D cadran_ScaleZ_Selected;
	
	public Texture2D cadran_RotateX_Normal;
	public Texture2D cadran_RotateX_Selected;
	public Texture2D cadran_RotateY_Normal;
	public Texture2D cadran_RotateY_Selected;
	public Texture2D cadran_RotateZ_Normal;
	public Texture2D cadran_RotateZ_Selected;
	
	public Texture2D Lang1;
	public Texture2D Lang2;
	public Texture2D Lang3;
	
	
	//Private
	[HideInInspector]
	public bool softLockEnabled;
	
	private int xRayTarget;
	private int yRayTarget;
	private Ray rayCamToTarget;
	
	[HideInInspector]
	public bool isLocking;
	[HideInInspector]
	public bool showCadran;
	
	[HideInInspector]
	public bool isModifying;
	
	//TranslateX
	private bool translateXActivated;
	
	//TranslateY
	private bool translateYActivated;
	
	//TranslateZ
	private bool translateZActivated;
	
	
	//ScaleX
	private bool scaleXActivated;
	
	//ScaleY
	private bool scaleYActivated;
	
	//ScaleZ
	private bool scaleZActivated;
	
	//RotateX
	private bool rotateXActivated;
	
	//RotateY
	private bool rotateYActivated;
	
	//RotateZ
	private bool rotateZActivated;
	
	private int activeMode = 0; // Langage actif
	private int nbModes = 3; // Nombre de langages
	private int cooldownCadran; // pour pas que l'appui sur le D-pad trigger le changement plusieurs fois
	
	private RaycastHit hitTarget;
	
	[HideInInspector]
	public GameObject targetToModify;
	
	[HideInInspector]
	public Vector3 targetStorePosition;
	[HideInInspector]
	public Vector3 targetStoreScale;
	[HideInInspector]
	public Quaternion targetStoreRotation;
	
	[HideInInspector]
	public Transform lastGameObjectLocked;

	private string option;
		
	// Use this for initialization
	void Start () 
	{
	
		VarInitialize();
		
	}
	
	// Update is called once per frame
	void Update () 
	{	
		//Check type de controles
		if(this.GetComponent<LockTargets>().enabled == true)
		{
			softLockEnabled = true;	
		}
		else
		{
			softLockEnabled = false;	
		}
		
		
		if(softLockEnabled == false)
		{
			xRayTarget = Screen.width/2;
			yRayTarget = Screen.height/2;
			
			//Raycast
			rayCamToTarget = camera.ScreenPointToRay(new Vector3(xRayTarget,yRayTarget));
			
			
			if(lastGameObjectLocked && lastGameObjectLocked != hitTarget.transform)
			{
				lastGameObjectLocked.transform.GetChild(0).light.enabled = false;	
				
			}
			
			//Check hit Method
			CheckRayHit();
		}
		
		//Check player's input
		CheckInputs();
		
		if(showCadran == true)
		{
			(GameObject.FindObjectOfType(System.Type.GetType ("TPCameraV2")) as TPCameraV2).playerCanRotate = false;
			(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).canMove = false;	
		}
			
	}
	
	void OnGUI () 
	{
		if(softLockEnabled == false)
		{
			// Affiche le crosshair de base
			if( isLocking == false & Time.timeScale !=0 )
			{
				GUI.DrawTexture(new Rect( Screen.width/2 - 16, Screen.height/2 - 16 , 32,32), crosshairNormal);
			}
			
			//Si passe sur un cube
			if(isLocking == true && showCadran == false)
			{
				GUI.DrawTexture(new Rect(Screen.width/2 - 16, Screen.height/2 - 16, 32,32), crosshairLock);	
			}
		}
	}
	
	void CheckRayHit()
	{
		if(Physics.Raycast(rayCamToTarget, out hitTarget) == true)
		{
			
			
			if(hitTarget.transform.gameObject.tag == "Cube" && hitTarget.transform.gameObject.GetComponent<CheckGlitch>().isInFrame)
			{
				isLocking = true;
				
				if((GameObject.FindObjectOfType(System.Type.GetType ("CheckGlitch")) as CheckGlitch).isInFrame == true)
				{
					hitTarget.transform.GetChild(0).light.enabled = true;
				}
				
				lastGameObjectLocked = hitTarget.transform;
			}
			else{
				isLocking = false;	
			}
		}
	}
	
	
	//________
	
	void CheckInputs()
	{	
		#region ControlsSoftLock
		if(softLockEnabled == true && (GameObject.FindObjectOfType(System.Type.GetType ("LockTargets")) as LockTargets).targetList.Count != 0)
		{	
			if(Input.GetAxis("TriggersL_1") >= 0.9 && Time.timeScale !=0)
			{		
				getCurrentOption();
			}
			
			// Si le bouton est lache sur translateX ou translateY, on lance la modification. A terme on va avoir un "else if"
			// pour chaque action possible, pas tres propre :s
			else 
			{
				ReleaseCadran();
			
			}
		}
		
		else if(isModifying == true)
		{
			(GameObject.FindObjectOfType(System.Type.GetType ("TPCameraV2")) as TPCameraV2).playerCanRotate = false;
			(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).FreezeMovement();
		}
		
		else
		{
			(GameObject.FindObjectOfType(System.Type.GetType ("TPCameraV2")) as TPCameraV2).playerCanRotate = true;
			(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).FreeMovement();
		}
		#endregion
		
		#region ControlsTPS
		if( softLockEnabled==false && isLocking == true 
			&& (GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).isAiming == true)
		{			
			if(Input.GetButtonDown("B_1") || (Input.GetButtonDown("X_1")) || (Input.GetButtonDown("Y_1")))
			{
                Debug.Log("ButtonGot");
                getCurrentOption();
			}
			
			// Si le bouton est lache sur translateX ou translateY, on lance la modification. A terme on va avoir un "else if"
			// pour chaque action possible, pas tres propre :s
			else 
			{
				ReleaseCadran();
			}
		}
		
		else if(isModifying == true)
		{
			(GameObject.FindObjectOfType(System.Type.GetType ("TPCameraV2")) as TPCameraV2).playerCanRotate = false;
			(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).FreezeMovement();
			
		}
		else
		{
			(GameObject.FindObjectOfType(System.Type.GetType ("TPCameraV2")) as TPCameraV2).playerCanRotate = true;
			(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).FreeMovement();
		}
		#endregion

		#region Changement cadran
		// Changement de cadran
		if (cooldownCadran >= 15)
		{
			if(showCadran && Input.GetAxis ("DPad_YAxis_1") >= 0.5)
			{
				activeMode +=1;
				if (activeMode == nbModes)
				{
					activeMode = 0;
				}
				cooldownCadran = 0;
			}
			else if(showCadran && Input.GetAxis ("DPad_YAxis_1") <= -0.5)
			{				
				activeMode -=1;
				
				if (activeMode < 0)
				{
					activeMode = nbModes-1;
				}
				cooldownCadran = 0;
			}
		}
		else
		{
			cooldownCadran +=1;
		}
		#endregion

	}
			
	void getCurrentOption()
	{
		option = (GameObject.FindObjectOfType(System.Type.GetType ("langageGUI")) as langageGUI).currentOption;
		switch (option) {
			case  "option1":
			  translateXActivated = true;
			  break;

			case "option2":
			   translateYActivated = true;
			   break;

			case "option3":
			    translateZActivated = true;
			    break;

			case "option4":
			    scaleXActivated = true;
			    break; 
			case  "option5":
			 	scaleYActivated = true;
			  break;

			case "option6":
			   scaleZActivated = true;
			   break;

			// case "option7":
			//     GUI.DrawTexture(new Rect(7*Screen.width/8 -64, 6*Screen.height/8 + 64, 64,64), option7Selected);
			//     break;

			// case "option8":
			//     GUI.DrawTexture(new Rect(7*Screen.width/8, 6*Screen.height/8, 64,64), option8Selected);
			//     break;
			// case  "option9":
			//   GUI.DrawTexture(new Rect(7*Screen.width/8 +64, 6*Screen.height/8 + 64, 64,64), option9Selected);
			//   break;

			// case "option10":
			//    GUI.DrawTexture(new Rect(7*Screen.width/8 -64, 6*Screen.height/8 + 64, 64,64), option10Selected);
			//    break;

			// case "option11":
			//     GUI.DrawTexture(new Rect(7*Screen.width/8, 6*Screen.height/8, 64,64), option11Selected);
			//     break;

			// case "option12":
			//     GUI.DrawTexture(new Rect(7*Screen.width/8 +64, 6*Screen.height/8 + 64, 64,64), option12Selected);
			//     break;         	
			}	
		}
	//____
	
	void PrepareToModif()
	{
		showCadran = false;
		isModifying = true;
		
		if(softLockEnabled)
		{
			int indexTarget = (GameObject.FindObjectOfType(System.Type.GetType ("LockTargets")) as LockTargets).idCurrentTarget;
			targetToModify = (GameObject.FindObjectOfType(System.Type.GetType ("LockTargets")) as LockTargets).targetList[indexTarget];
			
		}
		else
		{
			targetToModify = hitTarget.transform.gameObject;
		}
		
		targetStorePosition = targetToModify.transform.position;
		targetStoreScale = targetToModify.transform.localScale;
		targetStoreRotation = targetToModify.transform.rotation;
		
		(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isTranslatingX = false;
		(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isTranslatingY = false;
		(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isTranslatingZ = false;
			
		(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isScalingX = false;
		(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isScalingY = false;
		(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isScalingZ = false;
		
		(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isRotatingX = false;
		(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isRotatingY = false;
		(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isRotatingZ = false;
	}
	
	void ReleaseCadran()
	{
		if(translateXActivated)
			{
						
				translateXActivated = false;
				PrepareToModif();
				
				
//				targetToModify.transform.GetChild(0);
				
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isTranslatingX = true;				
				
			}
			
			
			else if(translateYActivated)
			{
				
				translateYActivated = false;
				
				PrepareToModif();
						
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isTranslatingY = true;
				
			}
			
			else if(translateZActivated)
			{
					
				translateZActivated = false;
				
				PrepareToModif();
						
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isTranslatingZ = true;
			
		
			}
			
			else if(scaleXActivated)
			{
									
				scaleXActivated = false;
				
				PrepareToModif();
						
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isScalingX = true;
			
			}
			
			else if(scaleYActivated)
			{
				
				scaleYActivated = false;
				
				PrepareToModif();
						
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isScalingY = true;
			
			}
			
			else if(scaleZActivated)
			{
				
				scaleZActivated = false;
				
				PrepareToModif();
						
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isScalingZ = true;
				
			}
		
		
			//rotate 
		
			else if(rotateXActivated)
			{
								
				rotateXActivated = false;
				
				PrepareToModif();
						
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isRotatingX = true;
			
			}
			
			else if(rotateYActivated)
			{
						
				rotateYActivated = false;
				
				PrepareToModif();
						
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isRotatingY = true;

			
			}
			
			else if(rotateZActivated)
			{
					
				rotateZActivated = false;
				
				PrepareToModif();
						
				(GameObject.FindObjectOfType(System.Type.GetType ("ReprogrammingStuff")) as ReprogrammingStuff).isRotatingZ = true;
				
			}
			
			
			else if(isModifying == true)
			{
				showCadran = false;
				
				(GameObject.FindObjectOfType(System.Type.GetType ("TPCameraV2")) as TPCameraV2).playerCanRotate = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).canMove = false;
			}
			
			
			else
			{
				
				showCadran = false;
				
				(GameObject.FindObjectOfType(System.Type.GetType ("TPCameraV2")) as TPCameraV2).playerCanRotate = true;
				(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).canMove = true;
			}
	}
	
	

	void VarInitialize()
	{
		isLocking = false;
		showCadran = false;
		translateXActivated = false;
		translateYActivated = false;
		translateZActivated = false;
		
		scaleXActivated = false;
		scaleYActivated = false;
		scaleZActivated = false;
		
		rotateXActivated = false;
		rotateYActivated = false;
		rotateZActivated = false;
		
		cooldownCadran = 15;
			
	}
	
}

