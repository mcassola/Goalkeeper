using UnityEngine;
using System.Collections;

public class GameEvents : MonoBehaviour 
{
	public delegate void CollisionHandler(GameObject collider,GameObject collidee);
	public delegate void JointUpdateHandler(int o ,Transform t);
	
	static public event JointUpdateHandler     JointUpdateEvent;
	static public event CollisionHandler 		  CollisionEnterEvent;
	
	static public void OnJointUpdateEvent(int joint ,Transform t)
	{
		JointUpdateEvent(joint,t);	
	}
	
	static public void OnCollisionEnter(GameObject collider,GameObject collidee)
	{
		CollisionEnterEvent(collider,collidee);
	}
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	/*static public void Invoke(string strEvent,Object param1,Object param2)
	{
		switch(strEvent)
		{
			case "OnCollisionEnter" :
				OnCollisionEvent((GameObject)param1,(GameObject)param2);
				break;
			case "JointUpdateEvent" :
				OnJointUpdateEvent((GameObject)param1,(Transform)param2);
			break;
		}		
	}*/
}
