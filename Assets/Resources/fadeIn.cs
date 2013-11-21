using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

public class fadeIn : MonoBehaviour {
	
	public Texture a;
	public Texture b;
	public bool primero = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
			if (Input.GetKey (KeyCode.RightArrow))
		{
			
		 iTween.FadeTo(gameObject,iTween.Hash("alpha", 1f, "time", 0.6f, "oncomplete", "changeTexture", "amount", 0.0f));	
		}
			if (Input.GetKey (KeyCode.LeftArrow))
		{
			
		 iTween.FadeTo(gameObject,iTween.Hash("alpha", 0f, "time", 0.6f, "amount", 1.0f));

		}
		
	}
	
	private void changeTexture(){
		if (primero)
			gameObject.renderer.material.mainTexture = b;	
		else
			gameObject.renderer.material.mainTexture = a;
		primero= !primero;
	}
}
