using UnityEngine;
using System.Collections;
/** Linneker Carvajal
 * lac93
 */
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

	public float cooldowntime = 1.0f;
	private bool cooldown = false;
	public float arrow_speed = 15f;
	public int maxDamage = 15;

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
		Vector3 position = this.transform.position;
		position.z = 0;
		this.transform.position = position;
		if (playerinfieldofvision()){
			attack ();
		} else if (canMove)
			this.rigidbody.velocity = Vector3.zero;
	}
	void attack(){

		GameObject player = GameObject.FindGameObjectWithTag(playerTag);
		//face player
		this.transform.LookAt(player.transform.position);
		
		if(distance_to_player() <= attack_range && !cooldown){

			switch(this.type){
			case unit_type.meele: tackle();
				break;
			case unit_type.turret: fire ();
				break;
			default: Debug.Log("Undefined unit_type");
				break;
			}
			StartCoroutine(cool_down());
		}else if(canMove && !cooldown){
		//insert movement script here (i.e. fly walk.. )
			this.rigidbody.velocity =(this.transform.forward * speed);
			
		}
	}
	void fire(){
		//offset
		GameObject newMissile = (GameObject)Instantiate(arrow, this.transform.position + this.transform.forward*2, this.transform.rotation);
		newMissile.SetActive (true);
		newMissile.GetComponentInChildren<Rigidbody>().velocity =(arrow_speed * this.transform.forward);
		
	}
	void tackle(){
		GameObject player = GameObject.FindGameObjectWithTag(playerTag);
		//face player
		this.transform.rotation = Quaternion.LookRotation(player.transform.position - this.transform.position);
		this.rigidbody.velocity =(this.transform.forward * 2* speed);
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
			if (this.transform.parent != null && this.transform.parent.name.Equals("turret"))
				Destroy(this.transform.parent.gameObject);
			Destroy(this.gameObject);
		}	
	}
	void OnCollisionEnter (Collision other){
		Collider obj = other.collider;
		if(obj.tag.Equals(playerTag)){
			obj.SendMessage ("ApplyDamage", (int)Random.Range(maxDamage/2, maxDamage));
		}else {
			//Do nothing
		}
		
		
		
	}
	IEnumerator cool_down ()
	{
		cooldown = true;
		yield return new WaitForSeconds(cooldowntime);  
		cooldown = false;
		
	}
}
