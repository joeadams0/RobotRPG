using UnityEngine;
using System.Collections;


/* Created by Rachid Lamouri (rxl244)
 * 
 * Script used by the explosion prefab for damaging other entities on collision
 * 
 */
public class Explodes : MonoBehaviour {
	public const int DAMAGE_EXPLOSION = 50;
	
	void Start () {
	
	}
	
	void Update () {
	
	}
	
	void OnCollisionTrigger(Collider collider){
		// This object's collider is not solid, if it makes contact with anything, it deals damage
		collider.SendMessage("hit",new float[]{AttackScript.ATTACK_TYPE_EXPLOSION,DAMAGE_EXPLOSION,UnitData.TEAM_NONE});
	}
}
