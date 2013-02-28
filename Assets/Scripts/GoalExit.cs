using UnityEngine;
using System.Collections;
/*Linneker Carvajal*/
public class GoalExit : MonoBehaviour {
	GameObject gameMaster;
		// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other){
	 	if (other.tag.Equals("Player"))
			gameMaster.SendMessage("LevelComplete");
	}
}
