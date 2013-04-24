using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	public const int NUM_ENEMIES = 10;
	private const float TIME_BETWEEN_SPAWNS = 2;
	private enum VARIABLE_TIME_RANGE {lowerBound = -1,upperBound = 1}; 
	
	private float timeStart;
	private float variableTime;
	
	private GameObject rifleEnemyTemplate;
	
	void Start () {
		rifleEnemyTemplate = (GameObject)Resources.Load("AI/RifleEnemy");
		setVariableTime();
	}	
	
	void Update () {
		if(Time.time + variableTime - timeStart > TIME_BETWEEN_SPAWNS){
			float halfWidth = .5f*this.transform.localScale.x;
			float halfDepth = .5f*this.transform.localScale.z;
			
			Instantiate(
				rifleEnemyTemplate,
				new Vector3(
					Random.Range(this.transform.position.x - halfWidth,this.transform.position.x + halfWidth),
					this.transform.position.y,
					Random.Range(this.transform.position.z - halfDepth,this.transform.position.z + halfDepth)
				),
				Quaternion.identity
			);
			
			setVariableTime();
		}
	}
	
	private void setVariableTime(){
		variableTime = Random.Range((float)VARIABLE_TIME_RANGE.lowerBound,(float)VARIABLE_TIME_RANGE.upperBound);
		timeStart = Time.time;
	}
}
