using UnityEngine;
using System.Collections;

public interface Ability{
	
	/// <summary>
	/// Tells if the ability can be used by the given
	/// </summary>
	/// <returns>
	/// The true if it can be used.
	/// </returns>
	bool canUse(Player p);
	
	/// <summary>
	/// Called when the given player uses the ability. 
	/// Returns true when the ability was successfully used.
	/// </summary>
	/// <param name='p'>
	/// If set to <c>true</c> p.
	/// </param>
	bool use(Player p);
	
	/// <summary>
	/// Upgrade the ability.
	/// </summary>
	/// <param name='p'>
	/// P.
	/// </param>
	void upgrade(Player p);
}
