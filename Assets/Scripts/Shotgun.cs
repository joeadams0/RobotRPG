/// <summary>
/// A Shotgun.
/// </summary>
/// By Joseph Satterfield

using UnityEngine;
using System.Collections;

public class Shotgun : Gun {
	
	// The damage that the gun does
	public float damage;
	
	//The time between shots
	public float cooldownTime = 1.5f;
	
	//The last time the gun was fired
	private float lastFireTime = 0f;
	
	//The range of the gun
	public float range = 10f;
	
	//Initializes the shotgun
	public Shotgun (){
		damage = 100;
	}
	
	/// <summary>
	/// Can the gun fire
	/// </summary>
	/// <returns>
	/// if the gun can fire
	/// </returns>
	/// <param name='p'>
	/// the player with the gun
	/// </param>
	public bool canFire(Player p)
	{
		if (Time.time > lastFireTime + cooldownTime)
			return true;
		else
			return false;
	}
	
	/// <summary>
	/// Fire the shotgun
	/// </summary>
	/// <param name='p'>
	/// the player
	/// </param>
	public bool fire(Player p)
	{
		if (Time.time > lastFireTime + cooldownTime)
		{
			lastFireTime = Time.time;
			return true;
		}
		else
			return false;
	}
	
	/// <summary>
	/// Upgrade the shotgun.
	/// </summary>
	/// <param name='p'>
	/// the player
	/// </param>
	public void upgrade(Player p)
	{
		cooldownTime = cooldownTime - .1f;
	}
	
	/// <summary>
	/// Gets the damage.
	/// </summary>
	/// <returns>
	/// The damage.
	/// </returns>
	public float getDamage()
	{
		return damage;
	}
	
	/// <summary>
	/// Gets the range.
	/// </summary>
	/// <returns>
	/// The range.
	/// </returns>
	public float getRange()
	{
		return range;
	}
}
