using UnityEngine;
using System.Collections;

public class scorer : MonoBehaviour {
	
	public GUISkin newSkin;   //  Assign this in the inspector.
	public Texture2D text;

 



		
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}
	void OnGUI(){
   GUI.skin = newSkin;
		GUIStyle myCustomStyle = new GUIStyle();
		myCustomStyle.font = Resources.Load("CollegiateFLF") as Font;
		myCustomStyle.fontSize = 35;
		myCustomStyle.normal.textColor = Color.yellow;
		myCustomStyle.border = new RectOffset(0,0,0,0);
		myCustomStyle.normal.background = text;
		myCustomStyle.padding = new RectOffset(20,20,20,20);
		//myCustomStyle.
	//	GUI.TextField(new Rect(0,0,200,50), "hello world!", myCustomStyle);
		
		
    	
		GUI.TextArea(new Rect(10,10,250,75), "Click me", myCustomStyle);

   }
}
