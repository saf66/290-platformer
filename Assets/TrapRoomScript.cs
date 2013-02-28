using UnityEngine;
using System.Collections;
/*Linneker Carvajal*/
public class TrapRoomScript : MonoBehaviour {
	public GameObject wallA;
	public GameObject wallB;
	public GameObject enemies;
	// Use this for initialization
	void Start () {
		wallA.SetActive(false);
		wallB.SetActive(false);
		enemies.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(enemies.transform.childCount <=0){
			wallA.SetActive(false);
			wallB.SetActive(false);
		}
	}
	
	void OnTriggerEnter(){
		if(!enemies.activeSelf)
			StartCoroutine( spawn());
	}
	
	IEnumerator spawn ()
	{
		yield return new WaitForSeconds(0.25f);  
		wallA.SetActive(true);
		wallB.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		enemies.SetActive(true);
		
		
	}

}
