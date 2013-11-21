using UnityEngine;
using System.Collections;

public class marcadores : MonoBehaviour {
	
   public GUISkin gSkin;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
	//	Debug.Log(ballShot.isVisibleCounts);
		if (ballShot.isVisibleCounts){
			GUI.skin = gSkin;
			int screenW = Screen.width;
			int screenH = Screen.height;
				
			
			GUI.Box (new Rect (10,screenH - screenH/5, 180, 100), "");
			GUI.Label(new Rect(0,screenH-screenH/5,45, 45), "","penaltyButton");// "penaltyButton");	
			GUILayout.BeginArea (new Rect(10,screenH - screenH/5, screenW/3-20, 160));
				GUILayout.Label (ballShot.cantPenales.ToString());	
			GUILayout.EndArea ();
			
			
			GUI.Box (new Rect (screenW - screenW/5 - 50,screenH - screenH/5, 310, 100), "");
			GUI.Label(new Rect(screenW - screenW/5 - 50,screenH-screenH/5,45, 45), "","goldCoinsButton");
			GUILayout.BeginArea(new Rect (screenW - screenW/5 - 50,screenH - screenH/5, 300, 100));
				GUILayout.Label (ballShot.puntuacion.ToString());	
			GUILayout.EndArea ();
		}
	}
}
