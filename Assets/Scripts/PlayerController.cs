/*
 * Author: Sean Fox (saf66)
 * Description:
 *    This class is responsible for allowing user input to control the main character.
 *    This covers player physics such as running, jumping, and collisions.
 */

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour {
	
	public const float speed = 0.2f;			// horizontal movement speed
	public const float accel = 6.0f;			// horizontal acceleration
	public const float friction = 4.0f;			// horizontal friction
	public const float jumpPower = 15.0f;		// initial vertical velocity
	public const float jumpSpeed = 1.8f;		// additional vertical velocity when holding jump
	public const float jumpTime = 0.35f;		// maximum time a jump can be held
	public const float gravity = 2.5f;			// gravity
	
	private CharacterController cc;				// allows for player control
	private Vector3 velocity = Vector3.zero;	// player movement vector
	private bool faceRight = true;				// is the player facing right?
	private bool jumpHeld = false;				// is the jump button being held?
	private float airTime = 0.0f;				// time spent in the air
	
	void Start () {
		// get the character controller
		cc = GetComponent<CharacterController>();
	}
	
	void FixedUpdate () {
		// get the current direction being held
		float dx = Input.GetAxis("Horizontal");
		
		// modify the player's horizontal movement based on current input
		// is there horizontal input?
		if (dx != 0.0f) {
			// do the movement and direction match?
			if (faceRight && dx < 0.0f || !faceRight && dx > 0.0f) {
				// turn the player to face the opposite direction
				faceRight = !faceRight;
				transform.localScale = -transform.localScale;
				velocity.x = 0.0f;
			}
			// move the player right
			velocity.x += Mathf.Sign(dx) * accel * speed * Time.fixedDeltaTime;
		} else {
			// apply friction based on current direction
			if (velocity.x < 0.0f) {
				velocity.x += friction * Time.fixedDeltaTime;
				velocity.x = -Mathf.Clamp(-velocity.x, 0.0f, speed);
			} else if (velocity.x > 0.0f) {
				velocity.x -= friction * Time.fixedDeltaTime;
				velocity.x = Mathf.Clamp(velocity.x, 0.0f, speed);
			}
		}
		// restrict maximum horizontal velocity
		velocity.x = Mathf.Clamp(velocity.x, -speed, speed);
		
		
		// modify the player's vertical movement based on current input
		// is the player grounded?
		if (cc.isGrounded) {
			// reset vertical velocity
			velocity.y = 0.0f;
			// reset jump timer
			airTime = 0.0f;
			// is the jump button pressed?
			if (Input.GetButton("Jump") && !jumpHeld) {
				jumpHeld = true;
				// give the player an initial vertical push
				velocity.y += jumpPower * Time.fixedDeltaTime;
			}
		} else {
			// is the jump button pressed?
			if (Input.GetButton("Jump")) {
				// has the jump button not been released and has the jump timer not run out?
				if (jumpHeld && airTime <= jumpTime) {
					// give the player additional vertical speed
					velocity.y += jumpSpeed * Time.fixedDeltaTime;
					// increment the jump timer
					airTime += Time.fixedDeltaTime;
				}
			} else {
				// player released the jump button in midair
				jumpHeld = false;
			}
			// apply gravity
			velocity.y -= gravity * Time.fixedDeltaTime;
		}
		// restrict maximum vertical velocity
		velocity.y = Mathf.Clamp(velocity.y, -gravity, Mathf.Infinity);
		
		
		// move the player
		CollisionFlags cf = cc.Move(velocity);
		//TODO: reset vertical velocity during ceiling collision
	}
	
	void OnControllerColliderHit (ControllerColliderHit hit) {
		if (hit.collider.tag == "Enemy") {
			//TODO: take damage
		}
	}
}
