using UnityEngine;
using System.Collections;

public class LaunchManager : MonoBehaviour {
	
	public GUIText viseTxt;
	
	private bool viseeNormal;
	private bool launchScene;
	
	// Use this for initialization
	void Start () {
		checkInitPrefControls();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(launchScene)
		{
			Application.LoadLevel("SoftLock_Crosshair_Dev");
			
		}
		
//		Debug.Log(PlayerPrefs.GetInt("Visee"));
		CheckInputs();
	}
	
	
	void OnGUI()
	{
	
		if(viseeNormal == true)
		{
			PlayerPrefs.SetInt("Visee", 0);
			viseTxt.text = "Visée Normale";	
		}
		else
		{
			PlayerPrefs.SetInt("Visee", 1);
			viseTxt.text = "Visée inversée";	
		}
		
		
		
	}
	
	void checkInitPrefControls(){
		
		if(PlayerPrefs.HasKey("Visee"))
		{
			if(PlayerPrefs.GetInt("Visee")== 0) //visee normale
			{
				viseeNormal = true;
			}
			else{
				viseeNormal = false;
			}
		}
		
	}
	
	
	void CheckInputs()	
	{
		
		if(Input.GetButtonDown("Start_1"))
		{
			launchScene = true;
		}
		else
		{
			launchScene = false;	
		}
		
		if(Input.GetButtonDown("Y_1"))
		{
		
			viseeNormal =! viseeNormal;
			
		}
		
	}
}
