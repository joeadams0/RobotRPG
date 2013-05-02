using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {
	
	// Number of bomber, rifle and rocket enemies respectively
	public readonly int[] NUM_ENEMIES = new int[]{
		3,
		2,
		1
	};
	
	private int[] numAlive = new int[]{0,0,0};
	
	private const float TIME_BETWEEN_SPAWNS = 2;
	private enum VARIABLE_TIME_RANGE {lowerBound = -1,upperBound = 1}; 
	
	private float timeStart;
	private float variableTime;
	
	private GameObject rifleEnemyTemplate;
	private GameObject bomberEnemyTemplate;
	private GameObject rocketEnemyTemplate;
	
	void Start () {
		rifleEnemyTemplate = (GameObject)Resources.Load("AI/RifleEnemy");
		bomberEnemyTemplate = (GameObject)Resources.Load("AI/BomberEnemy");
		rocketEnemyTemplate = (GameObject)Resources.Load("AI/RocketEnemy");
		setVariableTime();
	}	
	
	void Update () {
		if(Time.time + variableTime - timeStart > TIME_BETWEEN_SPAWNS){
			GameObject templateType;
			int range = UnitData.UNIT_TYPE_AI_ROCKET - UnitData.UNIT_TYPE_AI_RIFLE + 1;
			int randomType = Random.Range(0,range) % range;
			randomType = randomType + range;
			
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
			
			if(numAlive[randomType - UnitData.UNIT_TYPE_AI_RIFLE] < NUM_ENEMIES[randomType - UnitData.UNIT_TYPE_AI_RIFLE]){
				float halfWidth = .5f*this.transform.localScale.x;
				float halfDepth = .5f*this.transform.localScale.z;
				
				GameObject unit = (GameObject)Instantiate(
					templateType,
					new Vector3(
						Random.Range(this.transform.position.x - halfWidth,this.transform.position.x + halfWidth),
						this.transform.position.y,
						Random.Range(this.transform.position.z - halfDepth,this.transform.position.z + halfDepth)
					),
					Quaternion.Euler(new Vector3(-90,0,0))
				);
				
				UnitData unitData = unit.GetComponent<UnitData>();
				unitData.Owner = this.gameObject;
				
				numAlive[randomType - UnitData.UNIT_TYPE_AI_RIFLE]++;
				setVariableTime();
			}
			
			
			
		}
	}
	
	private void setVariableTime(){
		variableTime = Random.Range((float)VARIABLE_TIME_RANGE.lowerBound,(float)VARIABLE_TIME_RANGE.upperBound);
		timeStart = Time.time;
	}
	
	
	private void enemyDied(int type){
		if(type >= UnitData.UNIT_TYPE_AI_RIFLE && type <= UnitData.UNIT_TYPE_AI_ROCKET){
			numAlive[type - UnitData.UNIT_TYPE_AI_RIFLE]--;	
		}
	}
}
