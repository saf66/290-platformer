using UnityEngine;
using System.Collections;

public class HealthTracker : MonoBehaviour {
	
	public GameObject player = null;
	
	private PlayerController pc;
	
	void Start () {
		renderer.material.color = Color.green;
		pc = player.GetComponent<PlayerController>();
	}
	
	void FixedUpdate () {
		float health = (float) pc.GetHealth();
		Vector3 lp = transform.localPosition;
		Vector3 ls = transform.localScale;
	}
}
