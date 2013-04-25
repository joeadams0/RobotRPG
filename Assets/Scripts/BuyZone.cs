using UnityEngine;
using System.Collections;

public class BuyZone : MonoBehaviour {
	
	// The shop UI Object
	public GameObject shopUI;
	
	// The player object
	public GameObject player;
	
	//The amount of time between button presses
	public float timeBetweenPress;
	
	//Can the player activate the shop
	bool shopInRange;
	
	//is the shop activated
	bool shopActivated;
	
	//Time since last shop button press
	float timeSinceLast;

	// Use this for initialization
	void Start () {
		timeSinceLast = 0f;
		shopInRange = false;
		shopActivated = false;
		renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey (KeyCode.Space))
		{
			if(shopInRange)
			{
				if(timeSinceLast >= timeBetweenPress)
				{
					ToggleShop();
					timeSinceLast = 0;
				}
				
			}
		}
		timeSinceLast += Time.deltaTime;
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
		if(enter.collider.gameObject.name == player.name)
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
		if(exit.collider.gameObject.name == player.name)
		{
			shopInRange = false;
			shopActivated = false;
			shopUI.SendMessage ("DeactivateMenu");
		}
	}
	
	void ToggleShop()
	{
		if(shopActivated)
			shopUI.SendMessage ("DeactivateMenu");
		if(!shopActivated)
			shopUI.SendMessage ("ActivateMenu");
		shopActivated = !shopActivated;
	}
}
