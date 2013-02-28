using UnityEngine;
using System.Collections;
/* Linneker Carvajal*/
public class GameMasterScript : MonoBehaviour {
	public int level = Application.loadedLevel;
	public int numberlevels = Application.levelCount;
	public int playerLifes = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void LevelComplete(){
		if(level < numberlevels){
			Application.LoadLevel(level++);
		}
		else {
			//PLayer won
		}
	}
	
}
