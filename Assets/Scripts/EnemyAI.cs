using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public int HP = 100;
	public int field_of_vision_degree = 35;
	public int field_of_vision_distance = 50;
	public int attack_range = 5;
	public int speed = 20;
	public string playerTag = "Player";
	public bool canMove;
	public enum unit_type {meele, turret}; 
	public unit_type type;
	public GameObject arrow;
	public float coolDownTime = 1.0f;
	private bool coolDown = false;
	// Use this for initialization
	void Start () {
	switch(this.type){
			case unit_type.meele: canMove = true;
				break;
			case unit_type.turret: canMove = false;
				break;
			default: Debug.Log("Undefined unit_type");
				break;
			}
	}
	
	// Update is called once per frame
	void Update () {
		if (playerinfieldofvision()){
			attack ();
		}
	}
	void attack(){
		if(distance_to_player() <= attack_range && !coolDown){
			switch(this.type){
			case unit_type.meele: tackle();
				break;
			case unit_type.turret: fire ();
				break;
			default: Debug.Log("Undefined unit_type");
				break;
			}
			coolDown = true;
			StartCoroutine(cool_down());
		}else if(canMove){
		//insert movement script here (i.e. fly walk.. )
			GameObject player = GameObject.FindGameObjectWithTag(playerTag);
			//face player
			this.transform.rotation = Quaternion.LookRotation(player.transform.position - this.transform.position);
			this.transform.Translate( player.transform.forward * speed * Time.deltaTime);
			
		}
	}
	void fire(){
	}
	void tackle(){
		GameObject player = GameObject.FindGameObjectWithTag(playerTag);
		//face player
		this.transform.rotation = Quaternion.LookRotation(player.transform.position - this.transform.position);
		this.transform.Translate( player.transform.forward * 10*speed * Time.deltaTime);
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
	IEnumerator cool_down ()
	{
		yield return new WaitForSeconds(coolDownTime);  
		coolDown = false;
		
	}
}
