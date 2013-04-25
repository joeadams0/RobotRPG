using UnityEngine;
using System.Collections;

public class BuyZone : MonoBehaviour {
	
	// The shop UI Object
	public GameObject shopUI;
	
	// The player object
	public GameObject player;
	
	//Can the player activate the shop
	bool shopInRange;
	
	//is the shop activated
	bool shopActivated;

	// Use this for initialization
	void Start () {
		shopInRange = false;
		shopActivated = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey (KeyCode.Space))
		{
			if(shopInRange)
			{
				if(shopActivated)
					shopUI.SendMessage ("DeactivateMenu");
				if(!shopActivated)
					shopUI.SendMessage ("ActivateMenu");
			}
		}
	}
	
	/// <summary>
	/// Activates the shop when the player enters the buy zone
	/// </summary>
	/// <param name='enter'>
	/// The collider that entered the trigger
	/// </param>
	void OnTriggerEnter(Collider enter)
	{
		// Checks if the player is the thing that enetered
		if(true)
		{
			shopInRange = true;
		}
	}
	
	/// <summary>
	/// Deactivates the shop when the player the exits the buy zone
	/// </summary>
	/// <param name='exit'>
	/// The collider that entered the 
	/// </param>
	void OnTriggerExit(Collider exit)
	{
		// Checks to see if the player is the thing that exits
		if(true)
		{
			shopInRange = false;
			shopActivated = false;
			shopUI.SendMessage ("DeactivateMenu");
		}
	}
}
