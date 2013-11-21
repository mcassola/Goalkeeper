using UnityEngine;
using System.Collections;

public class VisibleCS : MonoBehaviour 
{
	public bool visible = false;
	// Use this for initialization
	void Start () 
	{
		renderer.enabled = visible;
	}
}
