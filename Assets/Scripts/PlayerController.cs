/*
 * Author: Sean Fox (saf66)
 * Description:
 *    This class is responsible for allowing user input to control the main character.
 *    This covers player physics such as running, jumping, and collisions.
 */

//TODO: HUD (health, lives)

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour {
	
	public Transform body = null;				// the player's body
	public Transform sword = null;				// the player's sword
	
	public const int max_health = 100;			// initial player health
	public const float speed = 0.2f;			// horizontal movement speed
	public const float accel = 6.0f;			// horizontal acceleration
	public const float friction = 4.0f;			// horizontal friction
	public const float jumpPower = 15.0f;		// initial vertical velocity
	public const float jumpSpeed = 1.8f;		// additional vertical velocity when holding jump
	public const float max_jumpTime = 0.35f;	// maximum time a jump can be held
	public const float max_invTime = 0.75f;		// maximum time spent in invincible mode
	public const float gravity = 2.5f;			// gravity
	public const float knockback_x = 2.8f;		// horizontal knockback force
	public const float knockback_y = 1.8f;		// vertical knockback force
	public const float jump_error = 0.1f;		// vertical velocity error correction
	public const float invincibleFactor = 10.0f;// frame multiplication for invincibility animation
	
	private CharacterController cc;				// allows for player control
	private Vector3 velocity = Vector3.zero;	// player movement vector
	private int health = max_health;			// current player health
	private bool isFacingRight = true;			// is the player facing right?
	private bool isJumpHeld = false;			// is the jump button being held?
	private float jumpTime = 0.0f;				// time spent in the air
	private bool isInvincible = false;			// can the player take damage?
	private float invincibleTime = 0.0f;		// time spent in invincible mode
	
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
			if (isFacingRight && dx < 0.0f || !isFacingRight && dx > 0.0f) {
				// turn the player to face the opposite direction
				isFacingRight = !isFacingRight;
				Vector3 ls = transform.localScale;
				ls.x = -ls.x;
				transform.localScale = ls;
				velocity.x = 0.0f;
			}
			// move the player in the proper direction
			velocity.x += Mathf.Sign(dx) * accel * speed * Time.fixedDeltaTime;
			// is the player grounded?
			if (cc.isGrounded) {
				//TODO: play the "run" animation
				//body.renderer.material.color = Color.yellow;
			}
		} else {
			// apply friction based on current direction
			if (velocity.x < 0.0f) {
				velocity.x += friction * Time.fixedDeltaTime;
				velocity.x = -Mathf.Clamp(-velocity.x, 0.0f, speed);
			} else if (velocity.x > 0.0f) {
				velocity.x -= friction * Time.fixedDeltaTime;
				velocity.x = Mathf.Clamp(velocity.x, 0.0f, speed);
			}
			// is the player grounded?
			if (cc.isGrounded) {
				//TODO: play the "idle" animation
				//body.renderer.material.color = Color.white;
			}
		}
		// restrict maximum horizontal velocity
		velocity.x = Mathf.Clamp(velocity.x, -speed, speed);
		
		
		// modify the player's vertical movement based on current input
		// is the player grounded?
		if (cc.isGrounded) {
			// reset vertical velocity
			velocity.y = 0.0f;
			// is the jump button pressed?
			if (Input.GetButton("Jump") && !isJumpHeld) {
				// reset jump timer
				jumpTime = 0.0f;
				isJumpHeld = true;
				// give the player an initial vertical push
				velocity.y += jumpPower * Time.fixedDeltaTime;
				//TODO: play the "start jump" animation
				//body.renderer.material.color = Color.magenta;
			}
		} else {
			// is the jump button pressed?
			if (Input.GetButton("Jump")) {
				// has the jump button not been released and has the jump timer not run out?
				if (isJumpHeld && jumpTime < max_jumpTime) {
					// give the player additional vertical speed
					velocity.y += jumpSpeed * Time.fixedDeltaTime;
					// increment the jump timer
					jumpTime += Time.fixedDeltaTime;
				}
			} else {
				// player released the jump button in midair
				isJumpHeld = false;
			}
			// apply gravity
			velocity.y -= gravity * Time.fixedDeltaTime;
			// check which jump animation to play
			if (velocity.y > 0.0f) {
				//TODO: play the "jumping" animation
				//body.renderer.material.color = Color.green;
			} else if (velocity.y < -jump_error) {
				//TODO: play the "falling" animation
				//body.renderer.material.color = Color.blue;
			}
		}
		// restrict maximum vertical velocity
		velocity.y = Mathf.Clamp(velocity.y, -gravity, gravity);
		
		
		// move the player
		CollisionFlags cf = cc.Move(velocity);
		
		// player should quit jumping if they hit the ceiling
		if ((cf & CollisionFlags.Above) == CollisionFlags.Above) {
			velocity.y = 0.0f;
			jumpTime = max_jumpTime;
		}
		
		// is the player in invincible mode?
		if (isInvincible) {
			// has the invincible mode timer ran out?
			if (invincibleTime < max_invTime) {
				// make the sprite flash
				sword.renderer.enabled = body.renderer.enabled = ((int) (invincibleTime * invincibleFactor)) % 2 == 1;
				// increment the timer
				invincibleTime += Time.fixedDeltaTime;
			} else {
				// reset invincible mode
				isInvincible = false;
				invincibleTime = 0.0f;
				sword.renderer.enabled = body.renderer.enabled = true;
				sword.renderer.material.color = body.renderer.material.color = Color.white;
			}
		}
	}
	
	public int GetHealth () {
		return health;
	}
	
	void ApplyDamage (int damage) {
		if (!isInvincible) {
			// take damage
			health -= damage;
			// check if dead
			if (health <= 0) {
				//TODO: respawn
			}
			// turn invincible
			isInvincible = true;
			// push the player away from the enemy
			cc.Move(new Vector3(isFacingRight ? -knockback_x : knockback_x, knockback_y));
			//TODO: play "damage" animation
			sword.renderer.material.color = body.renderer.material.color = Color.red;
		}
	}
}
