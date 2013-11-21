using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ballShot : MonoBehaviour {
	 public static int puntuacion = 0;
     public GameObject arco;
	 private bool kick = false;
     public float kickAngle = 15;
	 public float waitTime = 10.0f;
	 public float kickSecuence = 5.0f;
	 private Vector3 initialPos; 
	 private bool collicionDetected = false;
	 public TrailRenderer trailRenderer;
	 public List<AudioSource> audios;
	 public GameObject mainCamera;
	// public GameObject carpet;
	 public float cteVel= 1f;
	 public GameObject LineGK;
	 public static bool isVisibleCounts = false;
	private bool puntuacionAdded = false;
	public static int cantPenales = 10;
	public static int shootForce=0;
	public static bool canKickBall = false;

	//private bool kicking = false;
	private float jiggleAmt=0.0f; // how much to shake
	//public float shotPower = 80;
		public float kickPenaltyTime;
	float shake   = 0f;
	
	float shakeAmount   = 0.7f;
	
	float decreaseFactor   = 1.0f;
	public Quaternion initialRotate;
	public Dictionary<int,float> direcciones;
	public GameObject soccerPlayer;
	
	// private bool enabled = true;
	void Start () {
		initialPos = this.transform.position;
		initialRotate = this.transform.rotation;
		trailRenderer.enabled = true;

		//mainCamera  = GameObject.Find("Main Camera");
		direcciones = new Dictionary<int, float>();
		direcciones.Add(25,1.4f);
		direcciones.Add(20,1.25f);
		direcciones.Add(15,1f);
		direcciones.Add(10,0.8f);
		
	
	}

	private Vector3 KickForce(Vector3 dest, float angle)  {
	
//		Debug.Log(ball.position);
		  Vector3 dir = dest - this.transform.position;  // get target direction
		  float h = dir.y;  // get height difference
		  dir.y = 0;  // retain only the horizontal direction
		  float dist = dir.magnitude ;  // get horizontal distance
		
		 int valor = Random.Range(0,4);
		
		 shootForce = valor;
		canKickBall = true;
		
//		  Debug.Log(valor);		
		 int pos = valor*5 + 10;
		 angle = pos;
		 cteVel = direcciones[pos];
		//  cteVel = d
		
		  float a = angle * Mathf.Deg2Rad;  // convert angle to radians
		  dir.y = dist * Mathf.Tan(a);  // set dir to the elevation angle
		  dist += h / Mathf.Tan(a);  // correct for small height differences
		  // calculate the velocity magnitude
		  float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
		  return vel * dir.normalized * this.rigidbody.mass * cteVel;
    }

	// Remove ball collision for a short while to prevent accidental kicking after calibration
	private IEnumerator RemoveCollision()
	{
		GetComponent<SphereCollider>().enabled = false;
		rigidbody.useGravity = false;
		 
		yield return new WaitForSeconds(1.0f);
		if (cantPenales>0)
			audios[3].Play();
		GetComponent<SphereCollider>().enabled = true;
		rigidbody.useGravity = true;
		
		avatarController avatar  = soccerPlayer.GetComponent<avatarController>(); 
		avatar.kick = false;
		canKickBall = false;
		
	}
	
	 void OnCollisionEnter(Collision collision) {
//		Debug.Log(collision.collider.name);
		
		if (collision.collider.name.Contains("Mesh")){
			//	Debug.Log(collision.collider.name);
			//text.guiText.text = "Goool!";
			if (!puntuacionAdded){
					//puntuacion += 100;
					puntuacionAdded = true;
				}
			//if (!audios[2].isPlaying)
			//	audios[2].Play();
	       // foreach (ContactPoint contact in collision.contacts) {
	         //   Debug.DrawRay(contact.point, contact.normal, Color.red);
				//carpet.
				//carpet.transform.position = contact.point;//new Vector3( contact.point.x - carpet.renderer.bounds.size.x, contact.point.y - carpet.renderer.bounds.size.y,contact.point.z);
	       // }
	        if (collision.relativeVelocity.magnitude > 2){
							// === Some other script:
				// Calling from other code:
				//this.jiggleCam(0.5f,1.5f);
			   //shake = 2;
				
			}
	            
		}
		else{
			if (collision.collider.name.Contains("Box")){
				if (!puntuacionAdded){
					puntuacion += 1000;
					puntuacionAdded = true;
				}
				if (!audios[1].isPlaying)
					audios[1].Play();
					
			}
		}
        
    }
	

	// Others call this to cause an earthquake:
	public void jiggleCam(float amt, float duration) {
	  // Amt is how many meters the camera shakes. 0.5 is noticable
	  jiggleAmt = amt;
		Debug.Log(jiggleAmt);			
	  StartCoroutine(jiggleCam2(duration));
	}
	 
	IEnumerator jiggleCam2(float duration) {
	  yield return new WaitForSeconds(duration);
	  //jiggleAmt=0;
	}
	 

	
	// This method waits for the kick sequence to end, and restarts the game for the next kick
	private IEnumerator RestartBall()
	{
		// Wait a few seconds for the kick to end and the wall to come crashing down before resetting the game
		yield return new WaitForSeconds(waitTime);
		
		
		avatarController.enabledPlayer = false;
		// Reposition the ball at it's initial place
		transform.position = initialPos;
		transform.Rotate(initialRotate.x,initialRotate.y,initialRotate.z);
		trailRenderer.enabled = false;
		
		// Cancel all previous forces to keep the ball still
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		
		
		// Display the "Kick!" prompt
	//	wallGUIText.ShowKickPrompt = true;
		
		// Disable kick until the wall blocks all settle down
		StartCoroutine(RemoveCollision());
	}
	
	private IEnumerator KickPenalty(){
		//			Debug.Log("sisis");
		yield return new WaitForSeconds(kickSecuence);
		this.enabled = true;
		collicionDetected = false;
		kick = false;
		
	}
	
	public Vector3 Trayectoria(Vector3 r, Vector3 v, float t){
	    Vector3 g = new Vector3(0,-9.8F,0);   
	    return (r + v*t + 0.5f*g*t*t);
	}
	//int count = 0;
	
	
	private void enabledScores(){
		if (!isVisibleCounts){
			isVisibleCounts = true;
//			Debug.Log(isVisibleCounts);
		}
	}
	
	
	private IEnumerator kickBall(Vector3 kForce){
		//this.rigidbody.AddForce(transform.forward * shotPower, ForceMode.Impulse);
		if (cantPenales==10 && !audios[3].isPlaying)
			audios[3].Play();
		avatarController avatar  = soccerPlayer.GetComponent<avatarController>(); 
		avatarController.enabledPlayer = true;
	   
		avatar.kick = true;
		
		yield return new WaitForSeconds(kickPenaltyTime);
		 this.rigidbody.AddForce(kForce, ForceMode.Impulse);
		//this.rigidbody.velocity = kForce;
		// this.rigidbody.AddForce(-0.1f*kForce, ForceMode.Impulse);
		  collicionDetected = true;
		  audios[2].Stop();
		  audios[1].Stop();
		  if (!audios[0].isPlaying) {
		     audios[0].Play();
		 	 cantPenales--;
			 kick = true;
			//avatarController.enabledPlayer = false;
		  }
		  StartCoroutine(RestartBall());
	}
	
	
	
	void Update(){
		
		moveCamera c  = mainCamera.GetComponent<moveCamera>(); 
		//Debug.Log("oka " + c.game);
		
		
		
		if (c.game.Equals(moveCamera.stateGame.GAME)){
			enabledScores();
			
			if (cantPenales <= 0){
			
				//cantPenales = 10;
				moveCamera.gestoDetected = false;
				ballShot.isVisibleCounts = false;
				 unlockbar.swipeDetected = false;
				unlockbar.completeSlice = false;
				
				 GameObject g =  GameObject.Find("PlaneSlide");
				unlockbar u  = g.GetComponent<unlockbar>(); 
				
				u.restarUnlockBar();
				
				puntuacion = 0;
			}
		
		
		//	Debug.Log(kick);
			if (cantPenales > 0)
				StartCoroutine(KickPenalty());
			
			if (this.enabled && cantPenales > 0){
				
				
				trailRenderer.enabled = true;
				 //if (Input.GetButtonDown("Fire1")){
			   Vector3 position = new Vector3(Random.Range(0,Screen.width), Random.Range(0, Screen.height/4), 0);
				//	Vector3 position = Input.mousePosition;
				// Create a ray from the current mouse coordinates
			   // Debug.Log(Input.mousePosition);
			    
				
				Ray ray  = Camera.main.ScreenPointToRay(position);
			    RaycastHit hit = new RaycastHit();
			    // if something tagged "Ground" is hit...
			    if (Physics.Raycast(ray,out hit) && !kick){
	//		    	Debug.Log("ray = " +hit.collider.name);
				     if (hit.collider.name.Contains("Box") || hit.collider.name.Contains("Mesh")){//hit.collider.name.Contains("Arco") {
					    // carpet.transform.position = hit.point;  
						 Vector3 kForce = KickForce(hit.point, kickAngle); // calculate the force and kick!
						 this.enabled = false; 
						 puntuacionAdded = false;
	//					  Debug.Log(kForce);
						  if (kForce.x != float.NaN){
								StartCoroutine(kickBall(kForce));
						  }
				    }
				// }
			  }
				
			}
		}
	}
}