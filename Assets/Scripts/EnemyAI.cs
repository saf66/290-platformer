using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public int HP = 100;
	public int field_of_vision_degree = 35;
	public int field_of_vision_distance = 50;
	public int attack_range = 5;
	public int speed = 20;
	public string playerTag = "Player";
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (playerinfieldofvision()){
			attack ();
		}
	}
	void attack(){
		if(distance_to_player() <= attack_range){
		//Insert Attack script here
		}else {
		//insert movement script here (i.e. fly walk.. )
			GameObject player = GameObject.FindGameObjectWithTag(playerTag);
			//face player
			this.transform.rotation = Quaternion.LookRotation(player.transform.position - this.transform.position);
			this.transform.Translate( player.transform.forward * speed * Time.deltaTime);
			
		}
}
	float distance_to_player(){
		GameObject player = GameObject.FindGameObjectWithTag(playerTag);
		return Vector3.Distance (this.transform.position, player.transform.position);
	}
	bool playerinfieldofvision(){
		GameObject player = GameObject.FindGameObjectWithTag(playerTag);
		Vector3 direction = player.transform.position - this.transform.position;
		float angle = Vector3.Angle(this.transform.forward, direction);
		if(angle <= field_of_vision_degree && distance_to_player() <= field_of_vision_distance)
			return true;
		else
			return false;
	}
	/**
	 * This method reduces the hp by the specified amount 
	 * and destroys the gameobject if it reaches zero
	 */ 
	void ApplyDamage(int damage){
	
		HP -= damage;
		if (HP <= 0){
			//Insert destroy effects here
			Destroy(this.gameObject);
		}	
	}
	void OnCollisionEnter (Collision other){
		Collider obj = other.collider;
		if(obj.tag.Equals(playerTag)){
			
		}else {
			
		}
		
		
	}
}
