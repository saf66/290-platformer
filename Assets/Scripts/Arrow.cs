using UnityEngine;
using System.Collections;
/** Linneker Carvajal
 * lac93
 */
public class Arrow : MonoBehaviour {
	public int maxDamage =  10;
	public string playerTag = "Player";
	
	void OnCollisionEnter(Collision other){
		Collider obj = other.collider;
		if(obj.tag.Equals(playerTag)){
			obj.SendMessage ("ApplyDamage", (int)Random.Range(maxDamage/2, maxDamage));
		}
		Debug.Log("Collision Arrow");
		Destroy(this.gameObject);
	}
	
}
