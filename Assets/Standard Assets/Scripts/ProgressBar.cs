using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {
	public float progress;
	public Texture progressBackground;
	public Texture progressForground;
	public GUISkin gSkin;

	void OnGUI () {
			
			if (ballShot.canKickBall){
				float shoot = ballShot.shootForce;
				float MAX = shoot/4.0f + 0.25f;
//				Debug.Log(MAX);
				if (progress<= MAX){
		            DrawProgress(new Vector2(Screen.width/2 - 100 ,Screen.height - Screen.height/6),new Vector2(200,50),progress);
		            progress+=0.01f;
		   		 }	
			}else	
			 progress = 0;
	}

	void DrawProgress(Vector2 location ,Vector2 size , float progress )
	
	{ 
		GUI.skin = gSkin;
		GUI.DrawTexture(new Rect(location.x, location.y, size.x, size.y), progressBackground);
	
	    GUI.DrawTexture(new Rect(location.x, location.y, size.x * progress, size.y), progressForground); 
	   
		GUI.color = Color.white;
		/*GUI.Label(new Rect(location.x, location.y, size.x, size.y),"disparo");*/
		GUILayout.BeginArea (new Rect(location.x, location.y - 30, size.x, size.y));
			GUILayout.Label ("disparo");	
		GUILayout.EndArea ();
		
	}
}
