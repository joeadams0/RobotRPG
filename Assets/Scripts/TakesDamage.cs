using UnityEngine;
using System.Collections;

/* Created by Rachid Lamouri (rxl244)
 * 
 * This script gets added to anything that can take damage
 * It recieves a message that it was hit, it checks to see if the attacker
 * is not on the same team, and that the unit is not immune to that type of damage
 */ 
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
	public void hit(float[] attackParams){
		if(attackParams.Length != NUM_PARAMS){
			return;	
		}
		
		if(unitData.Team == UnitData.TEAM_PLAYER){
			print("Playerhit");
		}
	
		// Attack is valid if the attacker team and this unit's team are different
		if(unitData != null){
			if(unitData.Team != attackParams[(int)ATTACK_PARAMS.attackerTeam]){
				// modifyhealth is managed by the unitdata script
				unitData.SendMessage("modifyHealth",-attackParams[(int)ATTACK_PARAMS.damage]);	
			}
		}else{
			unitData = this.GetComponent<UnitData>();
		}
	}
}
