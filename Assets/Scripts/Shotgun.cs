using UnityEngine;
using System.Collections;

public class Shotgun : Gun {
	
	// The damage that the gun does
	public float damage;
	
	//The time between shots
	public float cooldownTime = 1.5f;
	
	//The last time the gun was fired
	private float lastFireTime = 0f;
	
	public float range = 10f;
	
	public Shotgun (){
		damage = 100;
	}
	
	public bool canFire(Player p)
	{
		if (Time.time > lastFireTime + cooldownTime)
			return true;
		else
			return false;
	}
	
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
	
	public void upgrade(Player p)
	{
		cooldownTime = cooldownTime - .1f;
	}
	
	public float getDamage()
	{
		return damage;
	}
	
	public float getRange()
	{
		return range;
	}
}
