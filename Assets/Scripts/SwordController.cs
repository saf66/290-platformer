/*
 * Author: Sean Fox (saf66)
 * Description:
 *    This class is responsible for swinging the sword to damage enemies.
 */

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class SwordController : MonoBehaviour {
	
	public const int damage = 34;				// amount of damage the sword does
	public const float max_attackTime = 0.1f;	// how long an attack lasts
	
	private bool isAttacking = false;			// is the attack currently ongoing?
	private float attackTime = 0.0f;			// timer for the current attack
	
	void Start () {
		// reset state
		Attack(false);
	}
	
	// enable or disable the player's sword
	void Attack (bool state) {
		collider.enabled = state;
		Vector3 ls = transform.localScale;
		Vector3 lp = transform.localPosition;
		if (state) {
			ls.x = 2.0f;
			lp.x = 1.4f;
		} else {
			ls.x = 0.1f;
			lp.x = 0.4f;
		}
		transform.localScale = ls;
		transform.localPosition = lp;
		isAttacking = state;
	}
	
	void FixedUpdate () {
		// is the player pressing attack?
		if (Input.GetButton("Fire1")) {
			if (isAttacking) {
				// is the attack animation ongoing?
				if (attackTime < max_attackTime) {
					// continue the attack
					attackTime += Time.fixedDeltaTime;
				} else {
					// end the attack
					Attack(false);
				}
			} else {
				if (attackTime == 0.0f) {
					// start the attack
					Attack(true);
				}
			}
		} else {
			if (isAttacking && attackTime < max_attackTime) {
				// end the attack early
				Attack(false);
			}
			attackTime = 0.0f;
		}
	}
	
	// damage enemies on touch
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Enemy") {
			other.SendMessage("ApplyDamage", damage);
		}
	}
	
	void OnTriggerStay (Collider other) {
		if (other.tag == "Enemy") {
			other.SendMessage("ApplyDamage", damage);
		}
	}
	
	void OnTriggerExit (Collider other) {
		if (other.tag == "Enemy") {
			other.SendMessage("ApplyDamage", damage);
		}
	}
}
