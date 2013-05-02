using UnityEngine;
using System.Collections;

// Script to hold all base information for all units including the player
public class UnitData : MonoBehaviour {
	// unit types (use these as reference when setting types in the inspector)
	public const int UNIT_TYPE_PLAYER = 1;
	public const int UNIT_TYPE_AI_MELEE = 2; 
	public const int UNIT_TYPE_AI_RIFLE = 3;
	public const int UNIT_TYPE_AI_BOMBER = 4;
	public const int UNIT_TYPE_AI_ROCKET = 5;
	public const int UNIT_TYPE_AI_MISSILE = 6;
	
	// Max health constants
	public const int MAX_HEALTH_PLAYER_DEFAULT = 150;
	public const int MAX_HEALTH_AI_SMALL = 100;
	public const int MAX_HEALTH_AI_MEDIUM = 200;
	public const int MAX_HEALTH_AI_LARGE = 300;
	
	// Attacking range constants
	public const int ATTACK_RANGE_RIFLE_UNIT = 30;
	public const int ATTACK_RANGE_BOMBER_UNIT = 3;
	public const int ATTACK_RANGE_ROCKET_UNIT = 40;
	public const int ATTACK_RANGE_MISSILE = 2;
	
	// Attack damage constants
	public const float ATTACK_DAMAGE_BULLET = 20;
	
	// Optimal distance constants
	public const float OPTIMAL_DISTANCE_RIFLE = 15;
	public const float OPTIMAL_DISTANCE_BOMBER = 2f;
	public const float OPTIMAL_DISTANCE_ROCKET = 30f;
	public const float OPTIMAL_DISTANCE_MISSILE = 0f;
	
	// Speed constants
	public const float SPEED_RIFLE_UNIT = 2f;
	public const float SPEED_BOMBER_UNIT = 2f;
	public const float SPEED_ROCKET_UNIT = 4f;
	public const float SPEED_MISSILE = 6.5f;
	
	//  Friendly fire id constants
	public const int TEAM_NONE = 0;
	public const int TEAM_PLAYER = 1;
	public const int TEAM_AI = 2;
	
	// Set this in the inspector: this value is read on startup
	public int SET_UNIT_TYPE_IN_INSPECTOR;
	
	// Unit attributes
	private int unitType;
	public int UnitType{
		set{}
		get{return unitType;}
	}
	
	private int maxHealth;
	public float health;
	public float Health{
		set{}
		get{return health;}
	}

	private int team;
	public int Team{
		set{}
		get{return team;}
	}
	
	private int attackRange;
	public int AttackRange{
		set{}
		get{return attackRange;}
	}
	
	private float attackDamage;
	public float AttackDamage{
		set{}
		get{return attackDamage;}
	}
	
	private float optimalDistance;
	public float OptimalDistance{
		set{}
		get{return optimalDistance;}
	}
	
	private float speed;
	public float Speed{
		set{}
		get{return speed;}
	}
	
	private GameObject owner;
	public GameObject Owner{
		set{owner = value;}
		get{return owner;}
	}
	
	// Initializes all attributes
	void Awake () {
		unitType = SET_UNIT_TYPE_IN_INSPECTOR;
		
		switch(unitType){
		case UNIT_TYPE_PLAYER:
			maxHealth = MAX_HEALTH_PLAYER_DEFAULT;
			team = TEAM_PLAYER;
			break;
		case UNIT_TYPE_AI_MELEE:
			maxHealth = MAX_HEALTH_AI_SMALL;
			team = TEAM_AI;
			break;
		case UNIT_TYPE_AI_RIFLE:
			maxHealth = MAX_HEALTH_AI_MEDIUM;
			team = TEAM_AI;
			attackRange = ATTACK_RANGE_RIFLE_UNIT;
			attackDamage = ATTACK_DAMAGE_BULLET;
			speed = SPEED_RIFLE_UNIT;
			optimalDistance = OPTIMAL_DISTANCE_RIFLE;
			break;
		case UNIT_TYPE_AI_BOMBER:
			maxHealth = MAX_HEALTH_AI_LARGE;
			attackRange = ATTACK_RANGE_BOMBER_UNIT;
			speed = SPEED_BOMBER_UNIT;
			optimalDistance = OPTIMAL_DISTANCE_BOMBER;
			team = TEAM_AI;
			break;
		case UNIT_TYPE_AI_ROCKET:
			maxHealth = MAX_HEALTH_AI_MEDIUM;
			speed = SPEED_ROCKET_UNIT;
			attackRange = ATTACK_RANGE_ROCKET_UNIT;
			optimalDistance = OPTIMAL_DISTANCE_ROCKET;
			team = TEAM_AI;
			break;
		case UNIT_TYPE_AI_MISSILE:
			maxHealth = 1000;
			speed = SPEED_MISSILE;
			attackRange = ATTACK_RANGE_MISSILE;
			optimalDistance = OPTIMAL_DISTANCE_MISSILE;
			team = TEAM_AI;
			break;
		default:
			maxHealth = 0;
			break;
		}
		
		health = maxHealth;
	}

	void Update (){
	}
	
	private void modifyHealth(float healthModifier){
		if(health + healthModifier > 0 && health + healthModifier <= maxHealth){
			health = health + healthModifier;	
		}else if(health + healthModifier <= 0){
			if (unitType == UNIT_TYPE_AI_BOMBER || unitType == UNIT_TYPE_AI_MELEE || unitType == UNIT_TYPE_AI_RIFLE || unitType == UNIT_TYPE_AI_ROCKET)
				Instantiate(Resources.Load("AI/ScrapMetal"), this.transform.position, Quaternion.identity);
			
			Destroy (gameObject);
			
			if(owner != null){
				owner.SendMessage("enemyDied",this.unitType);	
			}
		}
	}
}
