using UnityEngine;
using System.Collections;

public class BasicGun : Gun {
	
	private float damage;
	public float cooldownTime = 0.5f;
	float lastFireTime = 0f;
	public float range = 50f;
	
	public BasicGun()
	{
		damage = 500;
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
		damage = damage + 0.2f;
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
