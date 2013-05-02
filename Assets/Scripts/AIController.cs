using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {
	
	public readonly Vector3 GRAVITY = new Vector3(0,-1f,0);
	public readonly Vector3 ROCKET_LIFT = new Vector3(0,3f,0);
	public readonly Vector3 ROCKET_FIX = new Vector3(-90,0,0);
	
	private UnitData unitData;
	private AttackScript attackScript;
	private GameObject player;
	private GameObject explosionSphere;
	private GameObject missileTemplate;
	
	
	private const int MISSILE_COOLDOWN = 5;
	private float missileStart;
	
	void Start () {
		unitData = this.gameObject.GetComponent<UnitData>();
		attackScript = this.gameObject.GetComponent<AttackScript>();
		player = GameObject.Find("Player");
		
		if(unitData.UnitType == UnitData.UNIT_TYPE_AI_BOMBER || unitData.UnitType == UnitData.UNIT_TYPE_AI_MISSILE){
			explosionSphere = (GameObject)Resources.Load("AI/Explosion");
		}else if(unitData.UnitType == UnitData.UNIT_TYPE_AI_ROCKET){
			missileTemplate = (GameObject)Resources.Load("AI/Missile");
		}
		
		missileStart = Time.time;
	}
	
	void Update () {
		if(player == null){
			player = GameObject.Find("Player");
			return;
		}
		
		Vector3 targetDirection = player.transform.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 20, 0);     
		transform.rotation = Quaternion.LookRotation(newDirection);
        
		if(unitData.UnitType == UnitData.UNIT_TYPE_AI_ROCKET){
			transform.Rotate(ROCKET_FIX);
		}
		
		move();
		
		if(unitData.UnitType == UnitData.UNIT_TYPE_AI_BOMBER || unitData.UnitType == UnitData.UNIT_TYPE_AI_RIFLE){
			animation.Play("Walk");
		}
		
		attack();
	}
	
	private void move(){
		Vector3 playerPositionXZ = new Vector3(player.transform.position.x,0,player.transform.position.z);
		Vector3 positionXZ = new Vector3(this.transform.position.x,0,this.transform.position.z);		
		
		Vector3 gravity = GRAVITY;
		if(unitData.UnitType == UnitData.UNIT_TYPE_AI_ROCKET){
			gravity = new Vector3(0,0,0);
		}
		
		if(Vector3.Distance(this.transform.position,playerPositionXZ) > unitData.OptimalDistance){
			this.rigidbody.velocity = Vector3.ClampMagnitude(playerPositionXZ - positionXZ,unitData.Speed) + gravity;
		}else{
			this.rigidbody.velocity = Vector3.ClampMagnitude(positionXZ - player.transform.position,unitData.Speed) + gravity;
		}	
		
		if(unitData.UnitType == UnitData.UNIT_TYPE_AI_ROCKET){
			if(transform.position.y > 10){
				this.rigidbody.velocity = this.rigidbody.velocity - ROCKET_LIFT;
			}else if(transform.position.y < 5){
				this.rigidbody.velocity = this.rigidbody.velocity + ROCKET_LIFT;
			}
		}
	}
	
	private void attack(){
		if(Vector3.Distance(player.transform.position,this.transform.position) <= unitData.AttackRange){
			switch(unitData.UnitType){
			case UnitData.UNIT_TYPE_AI_MISSILE:
			case UnitData.UNIT_TYPE_AI_BOMBER:
				explode ();
				break;
			case UnitData.UNIT_TYPE_AI_RIFLE:
				attackScript.attack(
					player.transform.position - this.transform.position,
					unitData.AttackRange,
					AttackScript.ATTACK_TYPE_BULLET,
					unitData.AttackDamage,
					unitData.Team
				);
				break;
			case UnitData.UNIT_TYPE_AI_ROCKET:
				if(Time.time - missileStart > MISSILE_COOLDOWN){
					Instantiate(missileTemplate,transform.position,Quaternion.identity);
					
					missileStart = Time.time;
				}
				
				break;
			}
		}
	}
	
	private void explode(){
		Destroy(Instantiate(explosionSphere,this.transform.position,Quaternion.identity),.4f);
		this.SendMessage("hit",new float[]{AttackScript.ATTACK_TYPE_BULLET,10000,UnitData.TEAM_NONE});
	}
}
