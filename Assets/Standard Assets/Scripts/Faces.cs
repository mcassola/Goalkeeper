using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

class Faces : MonoBehaviour {
	
	
	public List<GameObject> carasCubo;
	public Texture a;
	
	// Use this for initialization
	void Start () {
		
	
}
	
	// Update is called once per frame
	void Update () {
		carasCubo[2].renderer.material.mainTexture = a ;
		if (Input.GetKey (KeyCode.RightArrow))
		{
		  iTween.RotateBy(gameObject, iTween.Hash("y", -0.25, "easeType", "easeOutBack", "loopType", "none", "delay", .2));	
		}
		if (Input.GetKey (KeyCode.LeftArrow))
		{
		  iTween.RotateBy(gameObject, iTween.Hash("y", 0.25, "easeType", "easeOutBack", "loopType", "none", "delay", .2));	
		}
		
			
	}

}
