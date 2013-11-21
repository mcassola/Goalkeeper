using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

class unlockbar : MonoBehaviour {
	
	public static bool swipeDetected = false;
	private int count = 0;
	public static bool completeSlice = false;
	// Use this for initialization
	void Start () {
		
	
}
	
	// Update is called once per frame
	void Update () {
		if (!completeSlice){
			if (Input.GetKey (KeyCode.RightArrow) || swipeDetected )
			{completeSlice = true;
				count++;
//				Debug.Log(count);
			  iTween.MoveBy(gameObject, iTween.Hash("x", -5.35, "easeType", "easeOutQuad", "loopType", "none", "delay", .2, "oncomplete","OnComplete"));	
				GameObject g2 =  GameObject.Find("textPlay");
			g2.renderer.enabled = false;
			}
			
		}
			
	}
	
	public void restarUnlockBar(){
			  iTween.MoveBy(gameObject, iTween.Hash("x", 5.35, "easeType", "easeOutQuad", "loopType", "none", "delay", .2,"oncomplete","OnCompleteRestar"));	
	}
	
	void OnComplete(){
		//Debug.Log("asdasd");
		completeSlice = true;
		moveCamera.gestoDetected = true;
		
		GameObject g =  GameObject.Find("SlideBar");
		g.renderer.enabled = false;
		
		GameObject g1 =  GameObject.Find("PlaneSlide");
		g1.renderer.enabled = false;
		
	
		
	}
	
	void OnCompleteRestar(){
		//	Debug.Log("xzzzzz");
		swipeDetected = false;
		completeSlice = false;
		
		 GameObject g =  GameObject.Find("Main Camera");
		 moveCamera m  = g.GetComponent<moveCamera>(); 
		 m.game = moveCamera.stateGame.MENU;
		ballShot.cantPenales = 10;
		
		GameObject g3 =  GameObject.Find("SlideBar");
		g3.renderer.enabled = true;
		
		GameObject g1 =  GameObject.Find("PlaneSlide");
		g1.renderer.enabled = true;
		
		GameObject g2 =  GameObject.Find("textPlay");
		g2.renderer.enabled = true;
		
	}
}