/*
 * Author: Sean Fox (saf66)
 * Description:
 *    This class is responsible for damaging the player when they touch spikes.
 */

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class SpikeAI : MonoBehaviour {

	public int damage = 10;		// damage the spikes cause to the player
	
	// spikes can't be killed
	void ApplyDamage (int damage) {
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
