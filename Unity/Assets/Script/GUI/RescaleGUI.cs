using UnityEngine;
using System.Collections;

public class RescaleGUI : MonoBehaviour{
	
	//private
	private float screenHeightRef;
	private float screenWidthRef;
	private float textHeightRef;
	private float textWidthRef;
	private float ratioObj;
	
	// Use this for initialization
	void Start () {
		InitVar();
	}
	
	// Update is called once per frame
	void Update () {
		AutoResize(Screen.width, Screen.height);
	}
	
	void AutoResize(int screenWidthCurrent, int screenHeightCurrent)
	{
		
		float pixelInsetWidth = (float)((textWidthRef*(screenWidthCurrent/screenWidthRef)));
		float pixelInsetHeight = (float)(pixelInsetWidth*ratioObj) ;
		
		float pixelInsetX = -(pixelInsetWidth/2)*ratioObj;
		float pixelInsetY = -(pixelInsetHeight/2)*ratioObj;
			
		this.guiTexture.pixelInset = new Rect(pixelInsetX, pixelInsetY, pixelInsetWidth, pixelInsetHeight) ;
	}
	
	void InitVar(){
		
		screenHeightRef = (float)Screen.height;
		screenWidthRef = (float)Screen.width;
		
		textHeightRef = this.guiTexture.pixelInset.height;
		textWidthRef = this.guiTexture.pixelInset.width;	
		
		ratioObj = textHeightRef/textWidthRef;
		//Debug.Log("ratio Objet" + ratioObj);
		
	}
}
