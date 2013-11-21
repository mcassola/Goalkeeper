using UnityEngine;
using System.Collections;

public class Gloves : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	 void OnCollisionEnter(Collision collision) {
		Debug.Log(collision.collider.name);
		
		if (collision.collider.name.Contains("Box")){
			Debug.Log(collision.collider.name);
		}
	}
	
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
