/*
 * Author: Sean Fox (saf66)
 * Description:
 *    This class is responsible for displaying the player's current health.
 */

using UnityEngine;
using System.Collections;

public class HealthTracker : MonoBehaviour {
	
	public GameObject player = null;	// the player gameobject
	
	private float max_health;			// maximum player health
	private float max_width;			// maximum bar width
	private float offset;				// initial bar offset
	private PlayerController pc;		// player controller
	
	void Start () {
		renderer.material.color = Color.green;
		// cache some useful variables
		max_width = transform.localScale.x;
		offset = transform.localPosition.x;
		pc = player.GetComponent<PlayerController>();
		max_health = (float) pc.GetMaxHealth();
	}
	
	void FixedUpdate () {
		float health = (float) pc.GetHealth();
		Vector3 lp = transform.localPosition;
		Vector3 ls = transform.localScale;
		// calculate the new position/width of the health bar
		ls.x = max_width * (health / max_health);
		lp.x = offset - (max_width - ls.x) * 0.5f;
		// update it
		transform.localScale = ls;
		transform.localPosition = lp;
	}
}
