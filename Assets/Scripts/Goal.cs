/* author: Shehab Hasan sqh4 */

using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//move to next stage when the player enter the goal area
	void OnTriggerEnter(Collider other) {
		Debug.Log("level trigger");
		GameObject.FindGameObjectWithTag("GameMaster").SendMessage("nextLevel");
	}
	

}
