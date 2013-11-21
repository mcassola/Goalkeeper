using UnityEngine;
using System.Collections;

public class video : MonoBehaviour {
	public MovieTexture movTex;
	
	// Use this for initialization
	void Start () {
	movTex.loop = true;

        movTex.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	//MovieTexture movTex = (MovieTexture)renderer.material.mainTexture;

        
	}
}
