using UnityEngine;
using System.Collections;


/* Created by Rachid Lamouri (rxl244)
 * 
 * This script can be attached to any rectangular prism game object
 * Turn off the renderer to make the game object invisible
 * 
 * This script has a predefined amount of each enemy type.
 * It spawns a random enemy at a given time interval (plus a modifier to make the time somewhat random)
 * When enemies die, they signal their original spawner so that they can respawn
 * 
 */ 
public class SpawnEnemies : MonoBehaviour {
	
	// Number of bomber, rifle and rocket enemies respectively
	public readonly int[] NUM_ENEMIES = new int[]{
		3,
		2,
		1
	};
	
	// Keeps track of how many of each type are still alive
	private int[] numAlive = new int[]{0,0,0};
	
	private const float TIME_BETWEEN_SPAWNS = 2;
	private enum VARIABLE_TIME_RANGE {lowerBound = -1,upperBound = 1}; 
	
	private float timeStart;
	private float variableTime;
	
	private GameObject rifleEnemyTemplate;
	private GameObject bomberEnemyTemplate;
	private GameObject rocketEnemyTemplate;
	
	void Start () {
		// Assign game objects for instantiating enemies
		rifleEnemyTemplate = (GameObject)Resources.Load("AI/RifleEnemy");
		bomberEnemyTemplate = (GameObject)Resources.Load("AI/BomberEnemy");
		rocketEnemyTemplate = (GameObject)Resources.Load("AI/RocketEnemy");
		setVariableTime();
	}	
	
	void Update () {
		// Enters if statement after time between spawns has passed
		if(Time.time + variableTime - timeStart > TIME_BETWEEN_SPAWNS){
			// Selects a random unit type to spawn
			GameObject templateType;
			int range = UnitData.UNIT_TYPE_AI_ROCKET - UnitData.UNIT_TYPE_AI_RIFLE + 1;
			int randomType = Random.Range(0,range) % range;
			randomType = randomType + range;
			
			// Gets the template of the type chosen
			switch(randomType){
			case UnitData.UNIT_TYPE_AI_RIFLE:
				templateType = rifleEnemyTemplate;
				break;
			case UnitData.UNIT_TYPE_AI_BOMBER:
				templateType = bomberEnemyTemplate;
				break;
			case UnitData.UNIT_TYPE_AI_ROCKET:
				templateType = rocketEnemyTemplate;
				break;
			default:
				return;
			}
			
			// If the unit type has not reached its maximum allowance, spawn a new one
			if(numAlive[randomType - UnitData.UNIT_TYPE_AI_RIFLE] < NUM_ENEMIES[randomType - UnitData.UNIT_TYPE_AI_RIFLE]){
				float halfWidth = .5f*this.transform.localScale.x;
				float halfDepth = .5f*this.transform.localScale.z;
				
				// Spawns the unit randomly within the bounds of the spawner
				GameObject unit = (GameObject)Instantiate(
					templateType,
					new Vector3(
						Random.Range(this.transform.position.x - halfWidth,this.transform.position.x + halfWidth),
						this.transform.position.y,
						Random.Range(this.transform.position.z - halfDepth,this.transform.position.z + halfDepth)
					),
					Quaternion.Euler(new Vector3(-90,0,0))
				);
				
				// Sets the owner to this object, so that when the unit dies it can message this spawner
				UnitData unitData = unit.GetComponent<UnitData>();
				unitData.Owner = this.gameObject;
				
				// The script only resets the timer if a unit was created
				numAlive[randomType - UnitData.UNIT_TYPE_AI_RIFLE]++;
				setVariableTime();
			}
			
			
			
		}
	}
	
	private void setVariableTime(){
		// Makes the spawn time less predictable
		variableTime = Random.Range((float)VARIABLE_TIME_RANGE.lowerBound,(float)VARIABLE_TIME_RANGE.upperBound);
		timeStart = Time.time;
	}
	
	
	private void enemyDied(int type){
		// When units die, they message their original spawner so that new units can spawn
		if(type >= UnitData.UNIT_TYPE_AI_RIFLE && type <= UnitData.UNIT_TYPE_AI_ROCKET){
			numAlive[type - UnitData.UNIT_TYPE_AI_RIFLE]--;	
		}
	}
}
