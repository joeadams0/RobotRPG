using UnityEngine;
using System.Collections;

public class Explodes : MonoBehaviour {
	public const int DAMAGE_EXPLOSION = 50;
	
	void Start () {
	
	}
	
	void Update () {
	
	}
	
	void OnCollisionTrigger(Collider collider){
		collider.SendMessage("hit",new float[]{AttackScript.ATTACK_TYPE_EXPLOSION,DAMAGE_EXPLOSION,UnitData.TEAM_NONE});
	}
}
