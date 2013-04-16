using UnityEngine;
using System.Collections;

// Calculates damage taken 
public class TakesDamage : MonoBehaviour {
	private UnitData unitData;
	
	// Send message parameter indices for the "hit" method
	public enum ATTACK_PARAMS{
		attackType = 0,
		damage,
		attackerTeam
	};
	
	private readonly int NUM_PARAMS = System.Enum.GetValues(typeof(ATTACK_PARAMS)).Length;

	void Start(){
		unitData = this.GetComponent<UnitData>();		
	}
	
	// Processes a hit
	private void hit(float[] attackParams){
		if(attackParams.Length != NUM_PARAMS){
			return;	
		}
		
		if(unitData != null){
			if(unitData.Team != attackParams[(int)ATTACK_PARAMS.attackerTeam]){
				unitData.SendMessage("modifyHealth",-attackParams[(int)ATTACK_PARAMS.damage]);	
			}
		}else{
			unitData = this.GetComponent<UnitData>();
		}
	}
}
