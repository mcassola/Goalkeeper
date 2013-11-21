using UnityEngine;

using System.Collections;

 

public class SlideShow : MonoBehaviour

{

public Object[] slides = new Texture2D[500];
	
public MovieTexture movie;
	
public float changeTime = 10.0f;

private int currentSlide = 0;

private float timeSinceLast = 1.0f;

public Texture2D textureFondo;

	void Start()
	
	{
		// slides = Resources.LoadAll("videos");
		this.renderer.material.mainTexture = textureFondo;
		//guiTexture.pixelInset = new Rect(-slides[currentSlide].width/2, -slides[currentSlide].height/2, slides[currentSlide].width, slides[currentSlide].height);
		
		//currentSlide++;
	
	}

 

	void Update()	
	{	
		
		 GameObject g =  GameObject.Find("Main Camera");
		 moveCamera m  = g.GetComponent<moveCamera>(); 
		// m.game = moveCamera.stateGame.MENU;
		
		if (m.game.Equals(moveCamera.stateGame.GAME)){
			//this.renderer.material.mainTexture.
			renderer.material.mainTexture = movie;
			if (!((MovieTexture) renderer.material.mainTexture).isPlaying) {
				
				((MovieTexture) renderer.material.mainTexture).Play();
			}
			/*	if(timeSinceLast > changeTime && currentSlide < slides.Length)
			{	
				this.renderer.material.mainTexture = (Texture2D)slides[currentSlide];			
				//guiTexture.pixelInset = new Rect(-slides[currentSlide].width/2, -slides[currentSlide].height/2, slides[currentSlide].width, slides[currentSlide].height);
				timeSinceLast = 0.0f;			
				currentSlide++;		
			}		
			timeSinceLast += Time.deltaTime;	
			if(currentSlide == slides.Length)	
			{	
				currentSlide = 0;	
			}*/
		}
		else{
			if (m.game.Equals(moveCamera.stateGame.MENU)){
				
				renderer.material.mainTexture = movie;
				if (((MovieTexture) renderer.material.mainTexture).isPlaying) {
					((MovieTexture) renderer.material.mainTexture).Stop();}
				}
				this.renderer.material.mainTexture = textureFondo;
			
			
		}
	}
	
	void OnGUI()
	{
	    // GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), movie);
	}

}