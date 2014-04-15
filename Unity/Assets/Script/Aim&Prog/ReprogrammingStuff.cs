using UnityEngine;
using System.Collections;

public class ReprogrammingStuff : MonoBehaviour {

	public float translateStep;
	public float translateSpeed;
	public float reboundValue;
	
	public float scaleStep;
	public float scaleSpeed;
	
	public float rotateStep;
	public float rotateSpeed;
	
	public float glitchTime;
	
	[HideInInspector]
	public bool isTranslatingX;
	[HideInInspector]
	public bool isTranslatingY;
	[HideInInspector]
	public bool isTranslatingZ;
	
	//scale
	[HideInInspector]
	public bool isScalingX;
	[HideInInspector]
	public bool isScalingY;
	[HideInInspector]
	public bool isScalingZ;
	
	//rotate
	[HideInInspector]
	public bool isRotatingX;
	[HideInInspector]
	public bool isRotatingY;
	[HideInInspector]
	public bool isRotatingZ;
	
	private Vector3 translateVector;
	
	private Vector3 scaleVector;
	
	private Vector3 rotateVector;
	
	private bool translateXMax = false;
	private bool translateXMin = false;
	private bool translateYMax = false;
	private bool translateYMin = false;
	private bool translateZMax = false;
	private bool translateZMin = false;
	
	private bool scaleXMax = false;
	private bool scaleXMin = false;
	private bool scaleYMax = false;
	private bool scaleYMin = false;
	private bool scaleZMax = false;
	private bool scaleZMin = false;
	
	private float lightRange;
	
	private bool isColliding;
	private bool platformCanMove;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		GetExtVar();
		
		LimitManager();
		

		if(isTranslatingX)
		{
			Translate ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify, 0);
		}
		if(isTranslatingY)
		{
			Translate ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify, 1);
		}
		if(isTranslatingZ)
		{
			Translate ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify, 2);

		}
		
		
		
		if(isScalingX)
		{
			Scale ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify, 0);
			
		}
		if(isScalingY)
		{
			Scale ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify, 1);
		}
		if(isScalingZ)
		{
			Scale ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify, 2);
        }
		
		
		if(isRotatingX)
		{
			Rotate ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify, 0);
			
		}
		if(isRotatingY)
		{
			Rotate ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify, 1);

		}
		if(isRotatingZ)
		{
			Rotate ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify, 2);

		}
		
		
		CheckValidation();
		
	}
	
	void GetExtVar()
	{
		if((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify)
		{
			isColliding = (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent<ModifLimit>().mustBeStopped;
		}
	}
	
	
	public void Translate(GameObject target, int TranslateDirection )
	{		
		if(!translateXMax && !translateXMin && !translateYMax && !translateYMin && !translateZMax && !translateZMin)
		{
			target.transform.GetChild(0).light.enabled = true;
			target.transform.GetChild(0).light.color = Color.blue;
		}
		
		if( TranslateDirection == 0) // Si Translate X
		{	
			
			if((Input.GetAxis("L_XAxis_1") >= 0.1 && !translateXMax))
			{
				if(!isColliding)
				{
					translateVector = new Vector3(translateStep, 0,0);
					target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
				}
				else
				{
//					GlitchCam();
					target.transform.GetChild(0).light.color = Color.red;
					
					translateVector = new Vector3(-translateStep*reboundValue, 0,0);
					target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
				}
			}
			else if( (Input.GetAxis ("L_XAxis_1")<= -0.1 && !translateXMin))
			{
			
				if(!isColliding)
				{
					translateVector = new Vector3(-translateStep, 0,0);
					target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
				}
				else
				{
//					GlitchCam();
					target.transform.GetChild(0).light.color = Color.red;
					translateVector = new Vector3(translateStep*reboundValue, 0,0);
					target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
				}
				
//				translateVector = new Vector3(-translateStep, 0,0);
//				target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
				
			}
			
		}
		
		if (TranslateDirection == 1) //Si Translate Y
		{

			if(Input.GetAxis("L_YAxis_1") >= 0.1 && !translateYMax)
			{
				if(!isColliding)
				{
					translateVector = new Vector3(0, translateStep,0);
					target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
				}
				else
				{
					target.transform.GetChild(0).light.color = Color.red;
					translateVector = new Vector3(0, -translateStep*reboundValue,0);
					target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
				}
			}
			else if(Input.GetAxis ("L_YAxis_1") <= -0.1 && !translateYMin)
			{
				
				if(!isColliding)
				{
					translateVector = new Vector3(0, -translateStep,0);
					target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
				}
				else
				{
					target.transform.GetChild(0).light.color = Color.red;
					
					translateVector = new Vector3(0, translateStep*reboundValue,0);
					target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
				}
				
//				translateVector = new Vector3(0, -translateStep,0);
//				target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
				
			}
			
		}
		
		if (TranslateDirection == 2) //Si Translate Z
		{

			
			if(Input.GetAxis("L_YAxis_1") >= 0.1 && !translateZMax)
			{
				if(!isColliding)
				{
					translateVector = new Vector3(0, 0,translateStep);
					target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
				}
				else
				{
					target.transform.GetChild(0).light.color = Color.red;
					translateVector = new Vector3(0, 0,-translateStep*reboundValue);
					target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
				}
			}
			else if(Input.GetAxis ("L_YAxis_1") <= -0.1 && !translateZMin)
			{
				
				if(!isColliding)
				{
					translateVector = new Vector3(0, 0,-translateStep);
					target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
				}
				else
				{
					target.transform.GetChild(0).light.color = Color.red;
					
					translateVector = new Vector3(0, 0,translateStep*reboundValue);
					target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
				}
				
//				translateVector = new Vector3(0, -translateStep,0);
//				target.transform.Translate(translateVector*Time.deltaTime*translateSpeed);
				
			}
			
		}
		
	}
		
	public void Scale(GameObject target, int ScaleDirection )
	{		
		if(!scaleXMax && !scaleXMin && !scaleYMin && !scaleYMax)
		{
			target.transform.GetChild(0).light.enabled = true;
			target.transform.GetChild(0).light.color = Color.blue;
		}
			
		if(ScaleDirection == 0) // Si Scale X
		{	
			
			if(Input.GetAxis("L_XAxis_1") >= 0.1 && !scaleXMax)
			{
				if(!isColliding)
				{
					scaleVector = new Vector3(scaleStep, 0,0);
					target.transform.localScale += (scaleVector*Time.deltaTime*scaleSpeed);
				}
				else
				{
//					Debug.Log ("Passe ici");
					scaleVector = new Vector3(-scaleStep, 0,0);
					target.transform.localScale += (scaleVector*Time.deltaTime*scaleSpeed);
				}
				
			}
			else if(Input.GetAxis ("L_XAxis_1")<= -0.1 && !scaleXMin)
			{
				
				scaleVector = new Vector3(-scaleStep, 0,0);
				target.transform.localScale += (scaleVector*Time.deltaTime*scaleSpeed);
				
			}
			
		}
		
		if( ScaleDirection == 1) // Si Scale Y
		{	
			
			if(Input.GetAxis("L_YAxis_1") >= 0.1 && !scaleYMax)
			{
				scaleVector = new Vector3(0, scaleStep,0);
				target.transform.localScale += (scaleVector*Time.deltaTime*scaleSpeed);
			}
			else if(Input.GetAxis ("L_YAxis_1")<= -0.1 && !scaleYMin)
			{
			
				scaleVector = new Vector3(0, -scaleStep,0);
				target.transform.localScale += (scaleVector*Time.deltaTime*scaleSpeed);
				
			}
			
		}
		
		if( ScaleDirection == 2) // Si Scale Z
		{	
			
			if(Input.GetAxis("L_YAxis_1") >= 0.1 && !scaleZMax)
			{
				scaleVector = new Vector3(0, 0,scaleStep);
				target.transform.localScale += (scaleVector*Time.deltaTime*scaleSpeed);
			}
			else if(Input.GetAxis ("L_YAxis_1")<= -0.1 && !scaleZMin)
			{
			
				scaleVector = new Vector3(0, 0,-scaleStep);
				target.transform.localScale += (scaleVector*Time.deltaTime*scaleSpeed);
				
			}
			
		}
		
	}
	
	
	public void Rotate(GameObject target, int RotateDirection )
	{		
		/*if(!scaleXMax && !scaleXMin && !scaleYMin && !scaleYMax)
		{
			target.transform.GetChild(0).light.enabled = true;
			target.transform.GetChild(0).light.color = Color.blue;
		} */
			
		if(RotateDirection == 0) // Si Rotate X
		{	
			
			if(Input.GetAxis("L_YAxis_1") >= 0.1)
			{
				if(!isColliding)
				{
					rotateVector = new Vector3(rotateStep, 0,0);
					target.transform.Rotate(rotateVector*Time.deltaTime*rotateSpeed);
				}
				else
				{
//					Debug.Log ("Passe ici");
					rotateVector = new Vector3(-rotateStep, 0,0);
					target.transform.Rotate(rotateVector*Time.deltaTime*rotateSpeed);
				}
				
			}
			else if(Input.GetAxis ("L_YAxis_1")<= -0.1)
			{
				
				rotateVector = new Vector3(-rotateStep, 0,0);
				target.transform.Rotate(rotateVector*Time.deltaTime*rotateSpeed);
				
			}
			
		}
		
		if( RotateDirection == 1) // Si Scale Y
		{	
			
			if(Input.GetAxis("L_XAxis_1") >= 0.1)
			{
				rotateVector = new Vector3(0, rotateStep,0);
				target.transform.Rotate(rotateVector*Time.deltaTime*rotateSpeed);
			}
			else if(Input.GetAxis ("L_XAxis_1")<= -0.1)
			{
			
				rotateVector = new Vector3(0, -rotateStep,0);
				target.transform.Rotate(rotateVector*Time.deltaTime*rotateSpeed);
				
			}
			
		}
		
		if( RotateDirection == 2) // Si rotate Z
		{	

			
			if(Input.GetAxis("L_YAxis_1") >= 0.1)
			{
				rotateVector = new Vector3(0, 0,rotateStep);
				target.transform.Rotate(rotateVector*Time.deltaTime*rotateSpeed);
			}
			else if(Input.GetAxis ("L_YAxis_1")<= -0.1)
			{
			
				rotateVector = new Vector3(0, 0,-rotateStep);
				target.transform.Rotate(rotateVector*Time.deltaTime*rotateSpeed);
				
			}
			
		}
		
	}
	
	
	private void CheckValidation()
	{
		if(isTranslatingX || isTranslatingY || isScalingX || isScalingY || isTranslatingZ || isScalingZ || isRotatingX || isRotatingY || isRotatingZ)
		{
			if(Input.GetButton ("A_1"))
			{	
				
				translateXMax = false;
				
				isTranslatingX = false;
				isTranslatingY = false;
				isScalingX = false;
				isScalingY = false;
				isTranslatingZ = false;
				isScalingZ = false;
				
				isRotatingX = false;
				isRotatingY = false;
				isRotatingZ = false;
				
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.white;
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.enabled = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).isModifying = false;

				(GameObject.FindObjectOfType(System.Type.GetType ("TPCameraV2")) as TPCameraV2).playerCanRotate = false;
				(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).FreeMovement();
			}
			
			if(Input.GetButton ("B_1"))
			{
				ResetTarget();
			}
			
		}
		
	}
	
	private void ResetTarget()
	{
		(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.position = (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetStorePosition;
		(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale = (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetStoreScale;
		(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.rotation = (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetStoreRotation;


		
		translateXMax = false;
		
		isTranslatingX = false;
		isTranslatingY = false;
		isScalingX = false;
		isScalingY = false;
		isTranslatingZ = false;
		isScalingZ = false;
		
		isRotatingX = false;
		isRotatingY = false;
		isRotatingZ = false;

		(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.white;
		(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.enabled = false;
		
		(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).isModifying = false;
		(GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).FreeMovement();
	}
	
	//______
	
	private void LimitManager()
	{
		// Verifier les limites du translate
		
		if((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify)
		{

			#region TranslateLimits
			//verifier max X
			if ( isTranslatingX && (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.position.x >= (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialPos.x + ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxTranslateX))
			{
				
				translateXMax = true;
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
			}
			else
			{
				
				translateXMax = false;
			}
			
			
			//verifier min X
			if ( isTranslatingX && (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.position.x <= (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialPos.x - ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxTranslateX))
			{
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
				translateXMin = true;
			}
			else
			{
				translateXMin = false;
			}
			
			
			//verifier max Y
			if ( isTranslatingY && (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.position.y >= (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialPos.y + ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxTranslateY))
			{
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
				translateYMax = true;
			}
			else
			{
				translateYMax = false;
			}
			
			
			//verifier min Y
			if ( isTranslatingY && (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.position.y <= (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialPos.y - ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxTranslateY))
			{
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
				translateYMin = true;
			}
			else
			{
				translateYMin = false;
			}	
			
			
			//verifier max Z
			if ( isTranslatingZ && (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.position.z >= (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialPos.z + ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxTranslateZ))
			{
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
				translateZMax = true;
			}
			else
			{
				translateZMax = false;
			}
			
			
			//verifier min Z
			if ( isTranslatingZ && (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.position.z <= (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialPos.z - ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxTranslateZ))
			{
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
				translateZMin = true;
			}
			else
			{
				translateZMin = false;
			}	
			#endregion
			
			#region ScaleLimits
			//verifier max X
			if ( isScalingX && (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale.x >= (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialScale.x + ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxScaleX))
			{
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
				scaleXMax = true;
			}
			else
			{
				scaleXMax = false;
			}
			
			
			//verifier min X
			if ( isScalingX && (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale.x <= /* (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialScale.x - */ ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).minScaleX) //)
			{
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
				scaleXMin = true;
			}
			else
			{
				scaleXMin = false;
			}
			
			//verifier max Y
			if ( isScalingY && (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale.y >= (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialScale.y + ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxScaleY))
			{
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
				scaleYMax = true;
			}
			else
			{
				scaleYMax = false;
			}
			
			//verifier min Y
			if ( isScalingY &&(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale.y <= /* (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialScale.x - */ ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).minScaleY) //)
			{
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
				scaleYMin = true;
			}
			else
			{
				scaleYMin = false;
			}
			
			
			//verifier max Z
			if ( isScalingZ && (GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale.z >= (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialScale.z + ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxScaleZ))
			{
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
				scaleZMax = true;
			}
			else
			{
				scaleZMax = false;
			}
			
			//verifier min Z
			if ( isScalingZ &&(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale.z <= /* (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialScale.x - */ ((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).minScaleZ) //)
			{
				(GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
				scaleZMin = true;
			}
			else
			{
				scaleZMin = false;
			}
			#endregion

            #region ScaleLimit
            //verifier max X
            if (isScalingX && (GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale.x >= (((GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialScale.x + ((GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxScaleX))
            {
                (GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
                scaleXMax = true;
            }
            else
            {
                scaleXMax = false;
            }


            //verifier min X
            if (isScalingX && (GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale.x <= /* (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialScale.x - */ ((GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).minScaleX) //)
            {
                (GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
                scaleXMin = true;
            }
            else
            {
                scaleXMin = false;
            }

            //verifier max Y
            if (isScalingY && (GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale.y >= (((GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialScale.y + ((GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxScaleY))
            {
                (GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
                scaleYMax = true;
            }
            else
            {
                scaleYMax = false;
            }

            //verifier min Y
            if (isScalingY && (GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale.y <= /* (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialScale.x - */ ((GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).minScaleY) //)
            {
                (GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
                scaleYMin = true;
            }
            else
            {
                scaleYMin = false;
            }


            //verifier max Z
            if (isScalingZ && (GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale.z >= (((GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialScale.z + ((GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).maxScaleZ))
            {
                (GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
                scaleZMax = true;
            }
            else
            {
                scaleZMax = false;
            }

            //verifier min Z
            if (isScalingZ && (GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).targetToModify.transform.localScale.z <= /* (((GameObject.FindObjectOfType(System.Type.GetType ("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).initialScale.x - */ ((GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).targetToModify.GetComponent("ModifLimit") as ModifLimit).minScaleZ) //)
            {
                (GameObject.FindObjectOfType(System.Type.GetType("CrosshairLock")) as CrosshairLock).targetToModify.transform.GetChild(0).light.color = Color.red;
                scaleZMin = true;
            }
            else
            {
                scaleZMin = false;
            }
            #endregion
		}
	
	}

	
//	private void GlitchCam()
//	{
//		StartCoroutine("GlitchWaitAndStop", glitchTime);
//	}
//	
//	IEnumerator GlitchWaitAndStop(float time)
//	{
//		this.GetComponent<GlitchEffect>().enabled = true;
//		yield return new WaitForSeconds(time);
//		this.GetComponent<GlitchEffect>().enabled = false;
//	}
}
