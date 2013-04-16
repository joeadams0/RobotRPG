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
	
	// Max health constants
	public const int MAX_HEALTH_PLAYER_DEFAULT = 150;
	public const int MAX_HEALTH_AI_SMALL = 100;
	public const int MAX_HEALTH_AI_MEDIUM = 200;
	public const int MAX_HEALTH_AI_LARGE = 300;
	
	//  Friendly fire id constants
	public const int TEAM_PLAYER = 1;
	public const int TEAM_AI = 2;
	
	// Set this in the inspector: this value is read on startup
	public int SET_UNIT_TYPE_IN_INSPECTOR;
	
	// Unit attributes
	private int unitType;
	private int team;
	private int maxHealth;
	private float health;
	
	// Unit properties
	public int Team{
		get{return team;}
	}
	
	// Initializes all attributes
	void Start () {
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
			break;
		case UNIT_TYPE_AI_BOMBER:
			maxHealth = MAX_HEALTH_AI_LARGE;
			team = TEAM_AI;
			break;
		case UNIT_TYPE_AI_ROCKET:
			maxHealth = MAX_HEALTH_AI_MEDIUM;
			team = TEAM_AI;
			break;
		default:
			maxHealth = 0;
			break;
		}
		
		health = maxHealth;
	}

	void Update (){
		if(this.health == 0){	
		}
	}
	
	private void modifyHealth(float healthModifier){
		if(health + healthModifier > 0 && health + healthModifier <= maxHealth){
			health = health + healthModifier;	
		}
	}
}
