using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {
	
	public const float GRAVITY = -1f;
	
	private UnitData unitData;
	private AttackScript attackScript;
	private GameObject player;
	
	void Start () {
		unitData = this.gameObject.GetComponent<UnitData>();
		attackScript = this.gameObject.GetComponent<AttackScript>();
		player = GameObject.Find("Player");
	}
	
	void Update () {
		move();
		
		switch(unitData.UnitType){
		case UnitData.UNIT_TYPE_AI_MELEE:
			updateMeleeUnit();
			break;
		case UnitData.UNIT_TYPE_AI_RIFLE:
			updateRifleUnit();
			break;
		case UnitData.UNIT_TYPE_AI_ROCKET:
			break;
		}
	}
	
	
	private void move(){
		if(Vector3.Distance(this.transform.position,player.transform.position) > unitData.OptimalDistance){
			this.rigidbody.velocity = Vector3.ClampMagnitude(player.transform.position - this.transform.position,unitData.Speed);
		}else{
			this.rigidbody.velocity = Vector3.ClampMagnitude(this.transform.position - player.transform.position,unitData.Speed);
		}	
	}
	
	private void updateMeleeUnit(){
	}
	
	private void updateRifleUnit(){
		if(Vector3.Distance(player.transform.position,this.transform.position) <= unitData.AttackRange){
			attackScript.attack(
				player.transform.position - this.transform.position,
				unitData.AttackRange,
				AttackScript.ATTACK_TYPE_BULLET,
				unitData.AttackDamage,
				unitData.Team
			);
		}
	}
}
