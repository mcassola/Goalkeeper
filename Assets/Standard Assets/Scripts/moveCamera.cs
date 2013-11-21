using UnityEngine;
using System.Collections;

public class moveCamera : MonoBehaviour {
	public Transform initialPos, endPos;	
	
	public float deltaPos = 0.11f;
	public static bool gestoDetected = false;
	private bool ready = false;
	public float factor = 0.01f;
	public enum stateGame { 
	    MENU = 0, 
	    GAME = 1, 
	    RANKING = 2
	}
	public stateGame game = stateGame.MENU;

	// Use this for initialization
	void Start () {
		this.transform.position = initialPos.position;
	}
	
	private void changeCamaraPosition(Transform start, Transform end){
		if (Mathf.Abs(start.transform.position.x - end.position.x) > deltaPos || Mathf.Abs(start.transform.position.y - end.position.y) > deltaPos || Mathf.Abs(start.transform.position.z - end.position.z) > deltaPos){	
		
			float x = start.transform.position.x,y = start.transform.position.y,z = start.transform.position.z;
			
		  if (Mathf.Abs(start.transform.position.x - end.position.x) > deltaPos){		
//						Debug.Log(Mathf.Abs(transform.position.x - endPos.position.x) +","+Mathf.Abs(transform.position.y - endPos.position.y) +","+Mathf.Abs(transform.position.z - endPos.position.z) );

				if ((start.transform.position.x - end.position.x) > 0){
					x-=factor;
				}else{
					if ((start.transform.position.x - end.position.x) < 0){
						x+=factor;
					}
				}
			}
			
			if (Mathf.Abs(start.transform.position.y - end.position.y) > deltaPos){			
				if ((start.transform.position.y - end.position.y) > 0){
					y-=factor;
				}else{
					if ((start.transform.position.y - end.position.y) < 0){
						y+=factor;
					}
				}
			}
			
			if (Mathf.Abs(start.transform.position.z - end.position.z) > deltaPos){			
				if ((start.transform.position.z - end.position.z) > 0){
					z-=factor;
				}else{
					if ((start.transform.position.z - end.position.z) < 0){
						z+=factor;
					}
				}
			}
			this.transform.position = new Vector3(x,y,z);
		}else{
			StartCoroutine(KickPenalty());
		}
		
	}
	
	private IEnumerator KickPenalty(){
		//			Debug.Log("sisis");
		yield return new WaitForSeconds(3.0f);
		game = stateGame.GAME;
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log(gestoDetected);
		if (game.Equals(stateGame.MENU) && gestoDetected){
			changeCamaraPosition(this.transform,endPos);
		}
		
		if (game.Equals(stateGame.GAME) && !gestoDetected){
				game = stateGame.MENU;
			//Debug.Log("sisisi");
				changeCamaraPosition(initialPos,this.transform);
		}
	}
}
