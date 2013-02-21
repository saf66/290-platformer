using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour {
	
	public float speed = 2.0f;
	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;
	
	private CharacterController ctrl;
	private Vector3 moveDirection = Vector3.zero;
	
	void Start () {
		ctrl = GetComponent<CharacterController>();
	}
	
	void Update () {
		//TODO: modify velocity during jump to a lesser extent
		//TODO: holding jump for longer makes the character jump slightly higher
		if (ctrl.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
			moveDirection *= speed;
			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
		}
		moveDirection.y -= gravity * Time.deltaTime;
		ctrl.Move(moveDirection * Time.deltaTime);
	}
	
	void OnControllerColliderHit (ControllerColliderHit hit) {
	}
}
