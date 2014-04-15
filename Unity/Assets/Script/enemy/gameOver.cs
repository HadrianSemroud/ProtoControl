using UnityEngine;
using System.Collections;

public class gameOver : MonoBehaviour {

	public Texture2D screen;
	public bool Dead;

	// Use this for initialization
	void Start () {
		Dead = false;
	}
	
	// Update is called once per frame
	void Update () {
			if (Dead == true) 
			{
				Time.timeScale = 0;
				checkInputs();
			}
	}

	void OnGUI()
	{
		if (Dead == true)
		{
			GUI.DrawTexture(new Rect(Screen.width/2 - (screen.width), Screen.height/2 - (screen.width), 500,500), screen);
		}
	}

	void checkInputs()
	{
		if (Input.GetButtonDown("A_1"))
		{
			Application.LoadLevel(1);
			Time.timeScale = 1;
		}

		if (Input.GetButtonDown("B_1"))
		{
			Debug.Log("Ok ok man ...");
			Application.Quit();
		}
	}

}
