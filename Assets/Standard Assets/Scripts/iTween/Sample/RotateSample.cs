using UnityEngine;
using System.Collections;

public class RotateSample : MonoBehaviour
{	
	void Start(){
		iTween.RotateBy(gameObject, iTween.Hash("y", -0.25, "easeType", "easeOutBack", "loopType", "none", "delay", .4));
	}
}

