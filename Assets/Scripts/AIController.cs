using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {
	
	public readonly Vector3 GRAVITY = new Vector3(0,-1f,0);
	
	private UnitData unitData;
	private AttackScript attackScript;
	private GameObject player;
	
	void Start () {
		unitData = this.gameObject.GetComponent<UnitData>();
		attackScript = this.gameObject.GetComponent<AttackScript>();
		player = GameObject.Find("Player");
	}
	
	void Update () {
		Vector3 targetDirection = player.transform.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 10, 0);     
        transform.rotation = Quaternion.LookRotation(newDirection);
  
		move();
		
		if(unitData.UnitType == UnitData.UNIT_TYPE_AI_BOMBER || unitData.UnitType == UnitData.UNIT_TYPE_AI_RIFLE){
			animation.Play("Walk");
		}
		
		attack();
	}
	
	private void move(){
		Vector3 playerPositionXZ = new Vector3(player.transform.position.x,0,player.transform.position.z);
		Vector3 positionXZ = new Vector3(this.transform.position.x,0,this.transform.position.z);		
		
		if(Vector3.Distance(this.transform.position,playerPositionXZ) > unitData.OptimalDistance){
			this.rigidbody.velocity = Vector3.ClampMagnitude(playerPositionXZ - positionXZ,unitData.Speed) + GRAVITY;
		}else{
			this.rigidbody.velocity = Vector3.ClampMagnitude(positionXZ - player.transform.position,unitData.Speed) + GRAVITY;
		}	
	}
	
	private void attack(){
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
