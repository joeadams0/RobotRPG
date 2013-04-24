using UnityEngine;
using System.Collections;

public class BasicGun : Gun {
	
	private float damage;
	
	public BasicGun()
	{
		damage = 1;
	}

	public bool canFire(Player p)
	{
		return true;
	}
	
	public bool fire(Player p)
	{
		return true;
	}
	
	public void upgrade(Player p)
	{
		damage = damage + 0.2f;
	}
	
	public float getDamage()
	{
		return damage;
	}
}
