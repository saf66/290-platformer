/* Author:  Shehab Hasan sqh4 */

using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	
	const int startingLevel = 0;  //start at level 0
	const int fallDeathLimit = -100;  //kill player if he falls below this y value
	int currentLevel;
	Rect healthDisplay = new Rect(10,8,150,30);  //display health in upperlefthand corner
	int playerHealth;
	GameObject player;
	
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);
		currentLevel = startingLevel;
		playerHealth = PlayerController.health;
		player = GameObject.Find ("Player");
		GUI.contentColor = Color.white;
	}
	
	// Update player health and kill player if necessary
	void Update () {
		playerHealth = PlayerController.health;
		if (player.transform.position.y < fallDeathLimit)
			playerDeath ();
	
	}
	
	//display health
	void OnGUI() {
		GUI.Label (healthDisplay, "Health: " + playerHealth);
	}
	
	//reload level upon player death, and reset health
	void playerDeath() {
		Application.LoadLevel(currentLevel);
		PlayerController.health = 100;
	}
	
	//load next level and reset health
	void nextLevel() {
		currentLevel++;
		PlayerController.health = 100;
		Application.LoadLevel(currentLevel);
	}
}
