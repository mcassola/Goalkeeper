using UnityEngine;
using System.Collections;

public class avatarController : MonoBehaviour {

	public Animator anim;
	public bool kick = false;
	public static bool enabledPlayer = false;
	private GameObject geo;
	void Start () {
		this.transform.position = new Vector3(8.600008f,1.102389f,0.2201434f);
		geo = GameObject.Find("Geo");
		geo.renderer.enabled = enabledPlayer;
		
		//this.enabled = false;
	//anim.animation.Play("soccer_penalty_kick_1");
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(8.600008f,1.102389f,0.2201434f);
		geo.renderer.enabled = enabledPlayer;
		if (kick)//(Input.GetKey (KeyCode.RightArrow)|| kick)
			anim.SetBool("active",true);
		else
			anim.SetBool("active",false);
		
	}	
}
