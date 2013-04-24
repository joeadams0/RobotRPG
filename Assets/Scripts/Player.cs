using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	
	/// <summary>
	/// The player's health.
	/// </summary>
	public float Health = 100F;
	
	/// <summary>
	/// The player's attack multiplier.
	/// </summary>
	public float Attack = 5F;
	
	/// <summary>
	/// The player's defense multiplier.
	/// </summary>
	public float Defense = 5F;
	
	/// <summary>
	/// The players energy.
	/// </summary>
	public float Energy = 100;
	
	/// <summary>
	/// The amount of scrap metal the player has.
	/// </summary>
	public int ScrapMetal = 0;
	
	/// <summary>
	/// The player's abilities.
	/// </summary>
	public List<Ability> Abilities = new List<Ability>();
	
	/// <summary>
	/// The player's guns.
	/// </summary>
	public List<Gun> Guns = new List<Gun>();
	
	/// <summary>
	/// The index of the currently selected gun.
	/// </summary>
	private int _gunIndex = 0;
	
	/// <summary>
	/// The index of the currently selected ability.
	/// </summary>
	private int _abilityIndex = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/// <summary>
	/// Switchs the selected gun.
	/// </summary>
	/// <param name='i'>
	/// The index of the gun to switch to.
	/// </param>
	public void switchGun(int i){
		if(i<Guns.Count){
			_gunIndex = i;
		}
	}
	
	/// <summary>
	/// Switchs the selected ability.
	/// </summary>
	/// <param name='i'>
	/// The index of the ability to switch to.
	/// </param>
	public void switchAbility(int i){
		if(i<Abilities.Count){
			_abilityIndex = i;
		}
	}
	
	/// <summary>
	/// Fires the currently selected gun.
	/// </summary>
	public void fireGun(){
		if(Guns[_gunIndex].canFire(this)){
			Guns[_gunIndex].fire(this);
		}
	}
	
	/// <summary>
	/// Uses the currently selected ability.
	/// </summary>
	public void useAbility(){
		if(Abilities[_abilityIndex].canUse(this)){
			Abilities[_abilityIndex].use(this);
		}
	}
	
}
