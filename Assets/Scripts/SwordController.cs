/*
 * Author: Sean Fox (saf66)
 * Description:
 *    This class is responsible for swinging the sword to damage enemies.
 */

using UnityEngine;
using System.Collections;

public class SwordController : MonoBehaviour {
	
	public Transform player = null;				// the player gameobject
	
	public const int damage = 34;				// amount of damage the sword does
	public const float max_attackTime = 0.1f;	// how long an attack lasts
	
	private bool isAttacking = false;
	private float attackTime = 0.0f;
	
	void Start () {
		Attack(false);
	}
	
	void Attack (bool state) {
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
	}
	
	void FixedUpdate () {
		if (Input.GetButton("Fire1")) {
			if (isAttacking) {
				if (attackTime < max_attackTime) {
					// continue the attack
					attackTime += Time.fixedDeltaTime;
				} else {
					// end the attack
					isAttacking = false;
					Attack(isAttacking);
				}
			} else {
				if (attackTime == 0.0f) {
					// start the attack
					isAttacking = true;
					Attack(isAttacking);
				}
			}
		} else {
			if (isAttacking && attackTime < max_attackTime) {
				// end the attack early
				isAttacking = false;
				Attack(isAttacking);
			}
			attackTime = 0.0f;
		}
	}
}
