using UnityEngine;
using System.Collections;

public enum MovementMode
{
	Human,
	Zombie
}

[AddComponentMenu("Mixamo/Demo/Root Motion Character")]
public class RootMotionCharacter : MonoBehaviour
{
	public float turningSpeed = 90f;
	public RootMotionComputer computer;
	public CharacterController character;
	public MovementMode mode = MovementMode.Human;

	void Start()
	{
		// validate component references
		if (computer == null) computer = GetComponent(typeof(RootMotionComputer)) as RootMotionComputer;
		if (character == null) character = GetComponent(typeof(CharacterController)) as CharacterController;
		
		// tell the computer to just output values but not apply motion
		computer.applyMotion = false;
		// tell the computer that this script will manage its execution
		computer.isManagedExternally = true;
		// since we are using a character controller, we only want the z translation output
	//	computer.computationMode = RootMotionComputationMode.tra;
		// initialize the computer
		computer.Initialize();
		
		// set up properties for the animations
	/*	animation["idle"].layer = 0; 
		animation["idle"].wrapMode = WrapMode.Loop;
		animation["walk"].layer = 1; animation["walk"].wrapMode = WrapMode.Loop;
		animation["run"].layer = 1; animation["run"].wrapMode = WrapMode.Loop;
	*/
		
	//	animation.Play(0);
	}
	
	void Update()
	{
		
	}
	
	void LateUpdate()
	{
		computer.ComputeRootMotion();
		
		// move the character using the computer's output
		character.SimpleMove(transform.TransformDirection(computer.deltaPosition)/Time.deltaTime);
	}
}