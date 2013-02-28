using UnityEngine;
using System.Collections;

public class fakebridge : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void fall (){
		rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
		this.rigidbody.useGravity = true;
		rigidbody.velocity = new Vector3 (0,5,0);
	}
	void OnCollisionEnter(Collision other){
		Collider obj = other.collider;
		Debug.Log ("Collision with: "+obj.name);
		if(obj.tag.Equals("Player")){
			rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
			this.rigidbody.useGravity = true;
			rigidbody.velocity = new Vector3 (0,5,0);
		} else if (obj.name.Equals("Slide"))
		Destroy(this.gameObject);
	}
}
