/*
 * Author: Sean Fox (saf66)
 * Description:
 *    This class is responsible for controlling the behavior of the final boss.
 */

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class BossAI : MonoBehaviour {
	
	public GameObject boss = null;				// the boss gameobject
	public Transform player = null;				// the player object
	public GameObject eventwall = null;			// the wall blocking the goal
	public Transform[] platforms = null;		// platforms to ignore
	
	public const float aggroRadius = 21.0f;		// radius at which the boss starts targeting the player
	public const float damage = 30.0f;			// amount of damage the boss deals
	public const float max_invTime = 0.75f;		// maximum time spent in invincible mode
	public const float invincibleFactor = 10.0f;// frame multiplication for invincibility animation
	public const float speed = 0.07f;			// horizontal movement speed
	public const float jumpPower = 15.0f;		// initial vertical velocity
	public const float gravity = 0.4f;			// gravity
	
	private CharacterController cc;				// allows for boss control
	private Vector3 velocity = Vector3.zero;	// boss movement vector
	private int health = 350;					// current health
	private bool isInvincible = false;			// can the boss take damage?
	private float invincibleTime = 0.0f;		// time spent in invincible mode
	
	void Start () {
		// get the character controller
		cc = boss.GetComponent<CharacterController>();
		// ignore certain platforms
		for (int i = 0; i < platforms.Length; i++) {
			Physics.IgnoreCollision(platforms[i].collider, boss.collider);
		}
	}
	
	void FixedUpdate () {
		// is the player nearby?
		Vector3 dp = player.transform.position - transform.position;
		if (dp.magnitude < aggroRadius) {
			// move horizontally towards the player
			velocity.x += Mathf.Sign(dp.x) * speed * Time.fixedDeltaTime;
		}
		// restrict maximum horizontal velocity
		velocity.x = Mathf.Clamp(velocity.x, -speed, speed);
		
		// vertical movement
		if (cc.isGrounded) {
			// jump in the direction of the player
			velocity.y = jumpPower * Time.fixedDeltaTime;
		} else {
			// apply gravity
			velocity.y -= gravity * Time.fixedDeltaTime;
		}
		// restrict maximum vertical velocity
		velocity.y = Mathf.Clamp(velocity.y, -gravity, gravity);
		
		// move the boss
		cc.Move(velocity);
		
		// is the boss in invincible mode?
		if (isInvincible) {
			// has the invincible mode timer ran out?
			if (invincibleTime < max_invTime) {
				// make the sprite flash
				renderer.enabled = ((int) (invincibleTime * invincibleFactor)) % 2 == 1;
				// increment the timer
				invincibleTime += Time.fixedDeltaTime;
			} else {
				// reset invincible mode
				isInvincible = false;
				invincibleTime = 0.0f;
				renderer.enabled = true;
				renderer.material.color = Color.white;
			}
		}
	}
	
	void ApplyDamage (int damage) {
		if (!isInvincible) {
			// take damage
			health -= damage;
			// check if dead
			if (health <= 0) {
				Destroy(eventwall);
				Destroy(boss);
			}
			// turn invincible
			isInvincible = true;
			renderer.material.color = Color.red;
		}
	}
	
	// damage the player on touch
	void OnTriggerEnter (Collider other) {
		PlayerController pc = other.GetComponent<PlayerController>();
		if (pc != null) {
			pc.SendMessage("ApplyDamage", damage);
		}
	}
	
	void OnTriggerStay (Collider other) {
		PlayerController pc = other.GetComponent<PlayerController>();
		if (pc != null) {
			pc.SendMessage("ApplyDamage", damage);
		}
	}
	
	void OnTriggerExit (Collider other) {
		PlayerController pc = other.GetComponent<PlayerController>();
		if (pc != null) {
			pc.SendMessage("ApplyDamage", damage);
		}
	}
}
