/*
 * Author: Sean Fox (saf66)
 * Description:
 *    This class is responsible for controlling the behavior of the final boss.
 */

using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour {
	
	public GameObject boss = null;				// the boss gameobject
	public Transform player = null;				// the player object
	
	public const float aggroRadius = 21.0f;		// radius at which the boss starts targeting the player
	public const float max_invTime = 0.75f;		// maximum time spent in invincible mode
	public const float invincibleFactor = 10.0f;// frame multiplication for invincibility animation
	
	private int health = 400;					// current health
	private bool isInvincible = false;			// can the boss take damage?
	private float invincibleTime = 0.0f;		// time spent in invincible mode
	
	void FixedUpdate () {
		// is the player nearby?
		Vector3 dp = transform.position - player.transform.position;
		if (dp.magnitude < aggroRadius) {
			//TODO: engauge the player
		}
		
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
				Destroy(boss);
			}
			// turn invincible
			isInvincible = true;
			//TODO: play "damage" animation
			renderer.material.color = Color.red;
		}
	}
}
