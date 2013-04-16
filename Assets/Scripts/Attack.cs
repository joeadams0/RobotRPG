using UnityEngine;
using System.Collections;

// Script by all things that can attack
public class Attack : MonoBehaviour {
	// Attack types
	public const int ATTACK_TYPE_MELEE = 1;
	public const int ATTACK_TYPE_BULLET = 2;
	public const int ATTACK_TYPE_EXPLOSION = 3;
	
	// Attack method (use get component to get the attack component and then use that to call this method directly)
	public void attack(Vector3 direction, float distance, int attackType, int damage, int attackerTeam){
		RaycastHit hitInfo = new RaycastHit();
		if(Physics.Raycast(transform.position,direction,out hitInfo, distance)){
			hitInfo.collider.gameObject.SendMessage("hit",new float[attackType,damage,attackerTeam],SendMessageOptions.DontRequireReceiver);
		}
	}
}
