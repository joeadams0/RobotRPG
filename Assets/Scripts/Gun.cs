using UnityEngine;
using System.Collections;

public interface Gun {
	
	/// <summary>
	/// Checks if the gun can fire.
	/// </summary>
	/// <returns>
	/// True if the gun can fire.
	/// </returns>
	/// <param name='p'>
	/// If set to <c>true</c> p.
	/// </param>
	bool canFire(Player p);
	
	/// <summary>
	/// Player fires the gun.
	/// Returns if the firing was successful. 
	/// </summary>
	/// <param name='p'>
	/// If set to <c>true</c> p.
	/// </param>
	bool fire(Player p);
	
	/// <summary>
	/// Upgrade the gun.
	/// </summary>
	/// <param name='p'>
	/// If set to <c>true</c> p.
	/// </param>
	void upgrade(Player p);
	
	/// <summary>
	/// Gets the damage.
	/// </summary>
	/// <returns>
	/// The damage.
	/// </returns>
	float getDamage();
	
	/// <summary>
	/// Gets the range of the gun.
	/// </summary>
	/// <returns>
	/// The range.
	/// </returns>
	float getRange();
}
