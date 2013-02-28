using UnityEngine;
using System.Collections;
/*Linneker Carvajal*/
public class spingPlataform : MonoBehaviour {
	public float rotationSpeed = 10; 
	// Use this for initialization
	void Start () {
		this.rigidbody.angularVelocity = new Vector3(0,0,rotationSpeed);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		this.rigidbody.angularVelocity = new Vector3(0,0,rotationSpeed);
	}
}
