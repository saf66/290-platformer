using UnityEngine;
using System.Collections;
/*Linneker Carvajal*/
public class deathline_script : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider obj){
		GameObject gamemaster = GameObject.FindGameObjectWithTag("GameMaster");
		//If player falls, restart level
		if 	(obj.tag.Equals("Player"))
			gamemaster.SendMessage("playerDeath");
		else //anything else destroy it
			Destroy(obj.gameObject);
	}
}
