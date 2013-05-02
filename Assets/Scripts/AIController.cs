using UnityEngine;
using System.Collections;

/*	Created by: Rachid Lamouri (rxl244)
 * 	This script controls the behavior of every enemy AI entity:
 * 		suicide bombers, rifle units, rocket units, and missiles fired from rocket units
 */

public class AIController : MonoBehaviour {
	// Gravity, rocket unit lift and rocket rotation constants
	public readonly Vector3 GRAVITY = new Vector3(0,-1f,0);
	public readonly Vector3 ROCKET_LIFT = new Vector3(0,3f,0);
	public readonly Vector3 ROCKET_FIX = new Vector3(-90,0,0);
	
	// All relevant unit information is stored in unitdata
	private UnitData unitData;
	private AttackScript attackScript;
	private GameObject player;
	private GameObject explosionSphere;
	private GameObject missileTemplate;
	
	// Prevents missile spamming
	private const int MISSILE_COOLDOWN = 5;
	private float missileStart;
	
	void Start () {
		// Assign relevent components and properties
		unitData = this.gameObject.GetComponent<UnitData>();
		attackScript = this.gameObject.GetComponent<AttackScript>();
		player = GameObject.Find("Player");
		
		// bombers and missiles can explode, while rocket units can generate missiles
		if(unitData.UnitType == UnitData.UNIT_TYPE_AI_BOMBER || unitData.UnitType == UnitData.UNIT_TYPE_AI_MISSILE){
			explosionSphere = (GameObject)Resources.Load("AI/Explosion");
		}else if(unitData.UnitType == UnitData.UNIT_TYPE_AI_ROCKET){
			missileTemplate = (GameObject)Resources.Load("AI/Missile");
		}
		
		// This value is only used by rocket units
		missileStart = Time.time;
	}
	
	void Update () {
		if(player == null){
			// Stops the game from crashing if the player dies
			player = GameObject.Find("Player");
			return;
		}
		
		// Forces the unit to face the player
		Vector3 targetDirection = player.transform.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 20, 0);     
		transform.rotation = Quaternion.LookRotation(newDirection);
        
		// The rocket unit would not import properly and so it has a default x rotation that is off
		if(unitData.UnitType == UnitData.UNIT_TYPE_AI_ROCKET){
			transform.Rotate(ROCKET_FIX);
		}
		
		// Moves the unit towards (or away from) the player
		move();
		
		// Runs the walking animation for bombers and rifle units
		if(unitData.UnitType == UnitData.UNIT_TYPE_AI_BOMBER || unitData.UnitType == UnitData.UNIT_TYPE_AI_RIFLE){
			animation.Play("Walk");
		}
		
		// Attacks the player if it is in range
		attack();
	}
	
	private void move(){
		// Isolates the x and z coordinates of the player and AI unit
		Vector3 playerPositionXZ = new Vector3(player.transform.position.x,0,player.transform.position.z);
		Vector3 positionXZ = new Vector3(this.transform.position.x,0,this.transform.position.z);		
		
		// Only applies gravity to ground units and missiles
		Vector3 gravity = GRAVITY;
		if(unitData.UnitType == UnitData.UNIT_TYPE_AI_ROCKET){
			gravity = new Vector3(0,0,0);
		}
		
		/* Each unit has an optimal distance value which determines how far from the player they should be
		 * Ex: bombers will rush the player, while rifle units will keep their distance
		 * 
		 * This also applies the unit's speed and gravity
		 */ 
		if(Vector3.Distance(this.transform.position,playerPositionXZ) > unitData.OptimalDistance){
			this.rigidbody.velocity = Vector3.ClampMagnitude(playerPositionXZ - positionXZ,unitData.Speed) + gravity;
		}else{
			this.rigidbody.velocity = Vector3.ClampMagnitude(positionXZ - player.transform.position,unitData.Speed) + gravity;
		}	
		
		if(unitData.UnitType == UnitData.UNIT_TYPE_AI_ROCKET){
			// This code keeps the rocket unit floating at a certain distance in the air
			
			if(transform.position.y > 10){
				this.rigidbody.velocity = this.rigidbody.velocity - ROCKET_LIFT;
			}else if(transform.position.y < 5){
				this.rigidbody.velocity = this.rigidbody.velocity + ROCKET_LIFT;
			}
		}
	}
	
	private void attack(){
		// Only attacks if the player is within the attacking range of the enemy unit
		if(Vector3.Distance(player.transform.position,this.transform.position) <= unitData.AttackRange){
			switch(unitData.UnitType){
			// missiles and bombers self destruct to attack
			case UnitData.UNIT_TYPE_AI_MISSILE:
			case UnitData.UNIT_TYPE_AI_BOMBER:
				explode ();
				break;
			// Rifle unit uses a raycast to shoot at the player (see attackscript)
			case UnitData.UNIT_TYPE_AI_RIFLE:
			
				attackScript.attack(
					player.transform.position - this.transform.position,
					unitData.AttackRange,
					AttackScript.ATTACK_TYPE_BULLET,
					unitData.AttackDamage,
					unitData.Team
				);
				break;
			// Rocket units fire missiles with a cooldown between missiles
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
		// Instantiates an explosion sphere for a limitied duration
		Destroy(Instantiate(explosionSphere,this.transform.position,Quaternion.identity),.4f);
		
		// This unit will kill itself
		this.SendMessage("hit",new float[]{AttackScript.ATTACK_TYPE_BULLET,10000,UnitData.TEAM_NONE});
	}
}
