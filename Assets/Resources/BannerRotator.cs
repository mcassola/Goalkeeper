
using UnityEngine;
using System.Collections.Generic;
using System;


public class BannerRotator : MonoBehaviour {

	float duration = 2.5f;
	public List<Texture2D> textures;
	int pos = 0;
	bool text = true;
	string textureName = "_TextMat1";
	float prevLerp = 0.0f;
	bool crece = true;
	bool wait = false;
	System.DateTime initialTime;
	System.DateTime currentTime;
	int cantidadTexturas;
	
	
	// Use this for initialization
	void Start () {
		cantidadTexturas = textures.Count;
	
	
	}
	
	// Update is called once per frame
	public void Update () {
		if (!wait){
			updateLerp();
		}
		else{
			//tiempo entre inicial y actual es mayor a cierto numero wait se vuelve false y reiniciar el contador
			//actualizar la hora actual aca
			TimeSpan diff = currentTime - initialTime;
			int milliseconds = (int) diff.TotalMilliseconds;
			if (milliseconds>5000){
				wait=false;	
			}
			currentTime = System.DateTime.Now;
		}
	}
	
	public void updateLerp(){
		if (setLerp()){
			if (text){
				textureName = "_TexMat1";
				text = false;
			}
		else {
				textureName = "_TexMat2";
				text = true;
			}
		renderer.material.SetTexture(textureName, textures[pos]); //ESTO PERMITE CAMBIAR LA TEXTURA 1, VER DONDE PONERLA
		pos = (pos + 1) % cantidadTexturas;
		}		
	}
	
	private bool setLerp(){
		float lerp = Mathf.PingPong (Time.time, duration) / duration;	
		renderer.material.SetFloat( "_Blend", lerp );
		if (crece){
			if (prevLerp<lerp){
				prevLerp = lerp;
				return false;	
			}
			wait = true;
			//reiniciar la hora inicial
			initialTime = System.DateTime.Now;
			prevLerp = lerp;
			crece = false;
			return true;
		}
		else{
			if (prevLerp>lerp){
				prevLerp = lerp;
				return false;	
			}
			wait = true;
			//reiniciar la hora inicial
			initialTime = System.DateTime.Now;
			prevLerp = lerp;
			crece = true;
			return true;
		}
	}
	
	
}