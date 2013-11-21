using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {
	
	 //public List<AudioSource> audios;
	public GameObject ball;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (ball.transform.position.z> this.transform.position.z){
			ballShot b  = ball.GetComponent<ballShot>(); 
	     
			if (!b.audios[2].isPlaying){
				b.audios[1].Stop();
				b.audios[2].Play();
				ballShot.puntuacion -=1000;
			}
		}
	}

	/*void OnCollisionEnter(Collision collision) {
     
//		Debug.Log(collision.collider.name);
		
		
		if (collision.collider.name.Contains("Ball")){
			//GameObject g = GameObject.Find("Ball");
			ballShot b  = ball.GetComponent<ballShot>(); 
	     
			if (!b.audios[2].isPlaying){
				b.audios[1].Stop();
				b.audios[2].Play();
			}
		}
	}*/
}
