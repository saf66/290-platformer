using UnityEngine;
using System.Collections;

public class fakebridgetrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(){
	this.transform.parent.SendMessage("fall");
	}
}
