/// <summary>
/// Shop UI and basic UI.
/// </summary>
/// By Joseph Satter

using UnityEngine;
using System.Collections;

public class ShopUI : MonoBehaviour {
	
	//The player  script gameobject
	public Player player;
	
	//The index of the basic gun on the player
	int basicGunIndex = 0;
	
	//The index of the shotgun on the player
	int shotgunIndex;
	
	//The left start of the shop background
	public float shopBackgroundLeftStart;
	
	//The top start of the shop background
	public float shopBackgroundRightStart;
	
	//The length of the shop background
	public float shopBackgroundLength;
	
	//The height of the shop background
	public float shopBackroundHeight;
	
	// The picute for the wepons category
	public Texture2D generalWeaponPicture;
	
	// The name for the weapons category
	public string weaponsCategory;

	// The description for the weapons category
	public string weaponsCatergoryDescription;
	
	//Price is zero for a category
	public int categoryPrice;
	
	//The picture for the powers category
	public Texture2D generalPowerPicture;
	
	//The name for the powers category
	public string powersCategory;
	
	// The description for the powers category
	public string powersCategoryDescription;
	
	// The picture of the first weapon
	public Texture2D firstWeaponPicture;
	
	// The name of the first weapon
	public string firstWeaponName;
	
	// price of first weapon upgrade
	public int firstWeaponPrice;
	
	// The description of the frist weapon
	public string firstWeaponDescription;
	
	// The picture of the first power
	public Texture2D secondWeaponPicture;
	
	// The name of the first power
	public string secondWeaponName;
	
	// The description of the first power
	public string secondWeaponDescription;
	
	// The price of the first power
	public int secondWeaponPrice;
	
	// The text on a button to enter a category
	public string categoryButtonText;
	
	// The text on a button to buy something
	public string buyText;

	//The main GUI Skin
	public GUISkin robotSkin;
	
	// is The first level of the shop menu active
	bool startLevel;
	
	// is The weapons selection level of the shop menu active
	bool weaponsBaseLevel;
	
	// is The power selection menu of the shop active
	bool powersBaseLevel;
	
	// is the menu active?
	bool menuOn;
	
	// is the base level active
	bool baseLevel;
	
	//has the second weapon been bought
	bool weapon2Bought;

	// Use this for initialization
	void Start () {
		menuOn = false;
		baseLevel = false;
		weaponsBaseLevel = false;
		powersBaseLevel = false;
		weapon2Bought = false;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	//Stuff that's on the gui
	void OnGUI ()
	{
		//The scrap counter
		GUI.Box (new Rect (10, 10, 100, 30), player.ScrapMetal.ToString());
		//Display this if the shop is open
		if(menuOn)
		{
			GUI.skin = null;
			GUI.Box (new Rect(190, 60, 1020, 700), "");
			GUI.skin = robotSkin;
			//Displays the main shop menu
			if(baseLevel)
			{
				//Switches to weapons level
				if (ShopBlock (new Rect(200,285,1000,120),generalWeaponPicture, weaponsCategory, weaponsCatergoryDescription,
					categoryPrice, categoryButtonText))
				{
					baseLevel = false;
					weaponsBaseLevel = true;
				}
				// Switches to powers menu
				if (ShopBlock (new Rect(200,415,1000,120),generalPowerPicture, powersCategory, powersCategoryDescription,
					categoryPrice, categoryButtonText))
				{
					baseLevel = false;
					powersBaseLevel = true;
				}
			}
			//The weapon upgrade level
			if(weaponsBaseLevel)
			{
				//Tries to buy an upgrade for the basic weapon
				if(ShopBlock (new Rect(200,285,1000,120), firstWeaponPicture, firstWeaponName, firstWeaponDescription, firstWeaponPrice,
					buyText))
				{
					//try to buy it
					//upgrade the weapon
					if(tryBuy (firstWeaponPrice))
					{
						player.Guns[basicGunIndex].upgrade (player);
					}
					Debug.Log (player.gunDamage());
				}
				//Tries to buy the shotgun or upgrade it
				if (ShopBlock (new Rect(200,415,1000,120),secondWeaponPicture, secondWeaponName, secondWeaponDescription,
					secondWeaponPrice, buyText))
				{
					//Upgrades the shotgun if it is bought
					if(weapon2Bought)
					{
						if(tryBuy (secondWeaponPrice))
						{
							player.Guns[shotgunIndex].upgrade (player);
						}
					}
					//Buys the shotgun
					else
					{
						if(tryBuy (secondWeaponPrice))
						{
							Shotgun shotgun = new Shotgun();
							player.Guns.Add (shotgun);
							shotgunIndex = player.Guns.IndexOf (shotgun);
							weapon2Bought = true;
						}
					}
				}
			}
			//The "powers" menu
			if(powersBaseLevel)
			{
				GUI.Box(new Rect(200, 285, 1000, 120), "Powers not yet authorized! Pay 1600 Joe Bucks for powers DLC!");
				if(GUI.Button (new Rect(200, 415, 1000, 120), "I'm too poor, back to main menu."))
				{
					powersBaseLevel = false;
					baseLevel = true;
				}
			}
		}
	}
	
	/// <summary>
	/// Deactivates the menu.
	/// </summary>
	void DeactivateMenu()
	{
		menuOn = false;
		weaponsBaseLevel = false;
		powersBaseLevel = false;
	}
	
	/// <summary>
	/// Activates the menu.
	/// </summary>
	void ActivateMenu()
	{
		menuOn = true;
		baseLevel = true;
	}
	
	/// <summary>
	/// Tries to buy the weapon.
	/// </summary>
	/// <returns>
	/// If the weapon is bought.
	/// </returns>
	/// <param name='cost'>
	/// The cost of the weapon.
	/// </param>
	bool tryBuy(int cost)
	{
		if(player.ScrapMetal >= cost)
		{
			player.ScrapMetal = player.ScrapMetal - cost;
			return true;
		}
		
		else
		{
			return false;
		}
	}
	
	/// <summary>
	/// The basic design used in creating the tiles in the shop
	/// </summary>
	/// <returns>
	/// If the button has been clicked
	/// </returns>
	/// <param name='boxPosition'>
	/// The position of the box on the screen
	/// </param>
	/// <param name='itemPicture'>
	/// The picture of the item
	/// </param>
	/// <param name='name'>
	/// The name of the item
	/// </param>
	/// <param name='itemDescription'>
	/// The description of the item
	/// </param>
	/// <param name='price'>
	/// The price of the item
	/// </param>
	/// <param name='buttonText'>
	/// The text on the button
	/// </param>
	bool ShopBlock (Rect boxPosition, Texture2D itemPicture, string name, string itemDescription, 
		int price, string buttonText)
	{
		GUI.Box (boxPosition,"");
		float itemStartPosition = boxPosition.x + 10f;
		GUI.Box (new Rect(itemStartPosition, boxPosition.y + 10f, 200, 100), itemPicture);
		itemStartPosition += 220f;
		GUI.Label(new Rect(itemStartPosition, boxPosition.y + 10f , 300, 25), name);
		GUI.Label(new Rect(itemStartPosition, boxPosition.y + 50f, 600, 25), itemDescription);
		itemStartPosition = boxPosition.xMax - 450f;
		GUI.Label (new Rect(itemStartPosition,boxPosition.y + 10f, 100, 25), price.ToString());
		return GUI.Button (new Rect(boxPosition.xMax - 210f, boxPosition.y + 10f, 200, 100), buttonText);
	}
}
