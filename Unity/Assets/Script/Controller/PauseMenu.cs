using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	
	private bool pauseState;
	[HideInInspector]
	public bool isPausing;
	private float savedTime;
	private bool viseeNormal;
	
	public GUIText pauseText;
	
	// Use this for initialization
	void Start () {
		pauseState = false;
	}
	
	// Update is called once per frame
	void Update () {
		
//		Debug.Log (pauseState);
		CheckInputs();		
	}
	
	void OnGUI()
	{
				
		if(isPausing)
		{
			GUI.Label (new Rect(Screen.width/2, Screen.height/2, 100,100), "PAUSE");
		
			GUI.Label(new Rect(Screen.width/2 , Screen.height/2 + 20, 300,300), "Appuyer sur Y pour changer le mode de visée");
			GUI.Label(new Rect(Screen.width/2 , Screen.height/2 + 40, 300,300), "Visée actuelle : ");
			
			if(PlayerPrefs.GetInt("Visee")== 0)
			{
				GUI.Label(new Rect(Screen.width/2 + 100 , Screen.height/2 + 40, 300,300), "Visée normale");
			}
			else
			{
				GUI.Label(new Rect(Screen.width/2 + 100 , Screen.height/2 + 40, 300,300), "Visée Inversée");
			}
			
			GUI.Label(new Rect(Screen.width/2 , Screen.height/2 + 60, 300,300),"Appuyer sur Select pour quitter");
		}
	}
	
	void CheckInputs()	
	{
		if(Input.GetButtonDown("Start_1"))
		{
			pauseState = ! pauseState;
			Pause ();
		}
		
		if(pauseState)
		{
			if(Input.GetButtonDown ("Y_1"))
			{
				viseeNormal =! viseeNormal;
			}
			if(Input.GetButtonDown("Back_1"))
			{
				Application.Quit();	
			}
		}

		
		if(viseeNormal == true)
		{
			PlayerPrefs.SetInt("Visee", 0);
		}
		else
		{
			PlayerPrefs.SetInt("Visee", 1);	
		}
		
	}
	
	void Pause()
	{
		if(pauseState)
		{
			isPausing = true;
			savedTime = Time.timeScale;
			Time.timeScale = 0;
			isPausing = true;
		}
		else
		{
			isPausing = false;
			Time.timeScale = savedTime;
		}
		
	}
}
