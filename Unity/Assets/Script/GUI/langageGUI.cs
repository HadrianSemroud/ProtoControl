using UnityEngine;
using System.Collections;

public class langageGUI : MonoBehaviour {

// Option 1/2/3 = translate x/y/z
// Option 4/5/6 = scale x/y/z
// Option 7/8/9 = rotate x/y/z
// Option 10/11/12 = ????

    //unlockManager
    public bool langage1Unlocked;
    public bool langage2Unlocked;
    public bool langage3Unlocked;
    public bool langage4Unlocked;
    public Texture2D langageLocked;
	   
    // Draw ARRAY of destiny (contain Options for langages)
    [HideInInspector]
    public string[] currentLangage;
    [HideInInspector]
    public string currentOption;
	private string[] langage1;
	private string[] langage2;
	private string[] langage3;
	private string[] langage4;

	public Texture2D langageTexture1;
	public Texture2D langageTexture2;
	public Texture2D langageTexture3;
    public Texture2D langageTexture4;
	public Texture2D langage1Selected;
	public Texture2D langage2Selected;
	public Texture2D langage3Selected;
	public Texture2D langage4Selected;

	// 3 Options of langages 1
	public Texture2D option1;
	public Texture2D option2;
	public Texture2D option3;	
	public Texture2D option1Selected;
	public Texture2D option2Selected;
	public Texture2D option3Selected;

	// 3 Options of langage 2
	public Texture2D option4;
	public Texture2D option5;
	public Texture2D option6;	
	public Texture2D option4Selected;
	public Texture2D option5Selected;
	public Texture2D option6Selected;

	// 3 Options of langage 3
	public Texture2D option7;
	public Texture2D option8;
	public Texture2D option9;	
	public Texture2D option7Selected;
	public Texture2D option8Selected;
	public Texture2D option9Selected;

	// 3 Options of langage 4
	public Texture2D option10;
	public Texture2D option11;
	public Texture2D option12;	
	public Texture2D option10Selected;
	public Texture2D option11Selected;
	public Texture2D option12Selected;

	private bool selected1;
	private bool selected2;
	private bool selected3;
	private bool selected4;

	private bool selectedOption1;
	private bool selectedOption2;
	private bool selectedOption3;

	// Use this for initialization
	void Start () {
		initVarLangage();
	}
	
	// Update is called once per frame
	void Update () {
		checkInputs();
	}

	void OnGUI()
	{
		if ((GameObject.FindObjectOfType(System.Type.GetType ("TPControllerV2")) as TPControllerV2).isAiming == true)
		{
		//Draw standard GUI for 4 langages & check if langage is unlocked
            if (langage1Unlocked == true)
            {
                GUI.DrawTexture(new Rect(Screen.width / 8, 6 * Screen.height / 8 - 64, 64, 64), langageTexture1);
            }
            else GUI.DrawTexture(new Rect(Screen.width / 8, 6 * Screen.height / 8 - 64, 64, 64), langageLocked);
           
            if (langage2Unlocked == true)
            {
                GUI.DrawTexture(new Rect(Screen.width / 8 - 64, 6 * Screen.height / 8, 64, 64), langageTexture2);
            } else GUI.DrawTexture(new Rect(Screen.width / 8 - 64, 6 * Screen.height / 8, 64, 64), langageLocked);

		     if (langage3Unlocked == true)
            {
                		GUI.DrawTexture(new Rect(Screen.width/8, 6*Screen.height/8 + 64, 64,64), langageTexture3);
            } else GUI.DrawTexture(new Rect(Screen.width/8, 6*Screen.height/8 + 64, 64,64), langageLocked);

             if (langage3Unlocked == true)
            {
              GUI.DrawTexture(new Rect(Screen.width/8 + 64, 6*Screen.height/8, 64,64), langageTexture4);

            } else GUI.DrawTexture(new Rect(Screen.width/8 + 64, 6*Screen.height/8, 64,64), langageLocked);

            
		
		//Draw the selected Texture if input is right
		if (currentLangage == langage1)
		{
			GUI.DrawTexture(new Rect(Screen.width/8, 6*Screen.height/8 - 64, 64,64), langage1Selected);
		}
		
		if (currentLangage == langage2)
		{
			GUI.DrawTexture(new Rect(Screen.width/8 - 64, 6*Screen.height/8, 64,64), langage2Selected);
		}

		if (currentLangage == langage3)
		{
			GUI.DrawTexture(new Rect(Screen.width/8, 6*Screen.height/8 + 64, 64,64), langage3Selected);
		}

		if (currentLangage == langage4)
		{
			GUI.DrawTexture(new Rect(Screen.width/8 + 64, 6*Screen.height/8, 64,64), langage4Selected);
		}

		// Draw selected option if input is right
		// if (selectedOption1 == true)
		// {
		// 	GUI.DrawTexture(new Rect(7*Screen.width/8 -64, 6*Screen.height/8 + 64, 64,64), option1Selected);

		// }

		// if (selectedOption2 == true)
		// {
		// 	GUI.DrawTexture(new Rect(7*Screen.width/8, 6*Screen.height/8, 64,64), option2Selected);
		// }

		// if (selectedOption3 == true)
		// {
        //GUI.DrawTexture(new Rect(7 * Screen.width / 8 + 64, 6 * Screen.height / 8 + 64, 64, 64), option3Selected);
		// }

		// DRAW TEXTURE FOR THE SELECTED LANGAGE
		switch (currentLangage[0]) 
		{
			case  "option1":
			  GUI.DrawTexture(new Rect(7*Screen.width/8 -64, 6*Screen.height/8 + 64, 64,64), option1);
			  break;

			 case "option4":
			   GUI.DrawTexture(new Rect(7*Screen.width/8 -64, 6*Screen.height/8 + 64, 64,64), option4);
			   break;

			 case "option7":
			    GUI.DrawTexture(new Rect(7*Screen.width/8 -64, 6*Screen.height/8 + 64, 64,64), option7);
			    break;

			 case "option10":
			    GUI.DrawTexture(new Rect(7*Screen.width/8 -64, 6*Screen.height/8 + 64, 64,64), option10);
			    break; 
		}

		switch (currentLangage[1]) 
		{
			case  "option2":
			  GUI.DrawTexture(new Rect(7*Screen.width/8, 6*Screen.height/8, 64,64), option2);
			  break;

			 case "option5":
			   GUI.DrawTexture(new Rect(7*Screen.width/8, 6*Screen.height/8, 64,64), option5);
			   break;

			 case "option8":
			    GUI.DrawTexture(new Rect(7*Screen.width/8, 6*Screen.height/8, 64,64), option8);
			    break;

			 case "option11":
			    GUI.DrawTexture(new Rect(7*Screen.width/8, 6*Screen.height/8, 64,64), option11);
			    break; 
		}

		switch (currentLangage[2]) 
		{
			case  "option3":
			  GUI.DrawTexture(new Rect(7*Screen.width/8 +64, 6*Screen.height/8 + 64, 64,64), option3);
			  break;

			 case "option6":
			   GUI.DrawTexture(new Rect(7*Screen.width/8 +64, 6*Screen.height/8 + 64, 64,64), option6);
			   break;

			 case "option9":
			    GUI.DrawTexture(new Rect(7*Screen.width/8 +64, 6*Screen.height/8 + 64, 64,64), option9);
			    break;

			 case "option12":
			    GUI.DrawTexture(new Rect(7*Screen.width/8 +64, 6*Screen.height/8 + 64, 64,64), option12);
			    break; 
		}

		// DRAW TEXTURE FOR THE SELECTED OPTION

		switch (currentOption) 
		{
			case  "option1":
			  GUI.DrawTexture(new Rect(7*Screen.width/8 -64, 6*Screen.height/8 + 64, 64,64), option1Selected);
			  break;

			case "option2":
			   GUI.DrawTexture(new Rect(7*Screen.width/8, 6*Screen.height/8, 64,64), option2Selected);
			   break;

			case "option3":
			    GUI.DrawTexture(new Rect(7*Screen.width/8 +64, 6*Screen.height/8 + 64, 64,64), option3Selected);
			    break;

			case "option4":
			    GUI.DrawTexture(new Rect(7*Screen.width/8 -64, 6*Screen.height/8 + 64, 64,64), option4Selected);
			    break; 
			case  "option5":
			  GUI.DrawTexture(new Rect(7*Screen.width/8, 6*Screen.height/8, 64,64), option5Selected);
			  break;

			case "option6":
			   GUI.DrawTexture(new Rect(7*Screen.width/8 +64, 6*Screen.height/8 + 64, 64,64), option6Selected);
			   break;

			case "option7":
			    GUI.DrawTexture(new Rect(7*Screen.width/8 -64, 6*Screen.height/8 + 64, 64,64), option7Selected);
			    break;

			case "option8":
			    GUI.DrawTexture(new Rect(7*Screen.width/8, 6*Screen.height/8, 64,64), option8Selected);
			    break;
			case  "option9":
			  GUI.DrawTexture(new Rect(7*Screen.width/8 +64, 6*Screen.height/8 + 64, 64,64), option9Selected);
			  break;

			case "option10":
			   GUI.DrawTexture(new Rect(7*Screen.width/8 -64, 6*Screen.height/8 + 64, 64,64), option10Selected);
			   break;

			case "option11":
			    GUI.DrawTexture(new Rect(7*Screen.width/8, 6*Screen.height/8, 64,64), option11Selected);
			    break;

			case "option12":
			    GUI.DrawTexture(new Rect(7*Screen.width/8 +64, 6*Screen.height/8 + 64, 64,64), option12Selected);
			    break;         	
		}	

	}
	}

	void initVarLangage()
	{
		// init selected to false
		// selected1 = false;
		// selected2 = false;
		// selected3 = false;
		// selected4 = false;
		langage1 = new string[] {"option1", "option2", "option3"};
		langage2 = new string[] {"option4", "option5", "option6"};
		langage3 = new string[] {"option7", "option8", "option9"};
		langage4 = new string[] {"option10", "option11", "option12"};
		currentLangage = langage1;
	}

	void checkInputs()
	{
		
		// Check Dpad inputs for langages 
		if ((Input.GetAxis("DPad_YAxis_1") == 1) && (langage1Unlocked == true))
		{
			// initVarLangage();
			currentLangage = langage1;
            currentOption = "";
		}

		if ((Input.GetAxis("DPad_YAxis_1") == -1) && (langage3Unlocked == true))
		{
			currentLangage = langage3;
            currentOption = "";
		}

		if ((Input.GetAxis("DPad_XAxis_1") == 1) && (langage4Unlocked == true))
		{
			currentLangage = langage4;
            currentOption = "";
		}

		if ((Input.GetAxis("DPad_XAxis_1") == -1) && (langage2Unlocked == true))
		{
			currentLangage = langage2;
            currentOption = "";
		}

		// Check buttons inputs for options

		if (Input.GetButtonDown("X_1") == true)
		{
			currentOption = currentLangage[0];
		}

		if (Input.GetButtonDown("Y_1") == true)
		{
			currentOption = currentLangage[1];
		}


		if (Input.GetButtonDown("B_1") == true)
		{
			currentOption = currentLangage[2];
		}
	}
}
