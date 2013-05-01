using UnityEngine;
using System.Collections;

public class ShopUI : MonoBehaviour {
	
	//The player  script gameobject
	public Player player;
	
	//The index of the basic gun on the player
	int basicGunIndex = 0;
	
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
	
	//is the menu for the first weapon active
	bool weapon1Level;
	
	//is the menu for the first power active
	bool power1Level;
	
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
		weapon1Level = false;
		power1Level = false;
		weapon2Bought = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey (KeyCode.LeftArrow))
		{
			menuOn = !menuOn;
			baseLevel = !baseLevel;
			Debug.Log (menuOn.ToString ());
		}
	}
	
	void OnGUI ()
	{
		if(menuOn)
		{
			GUI.skin = null;
			GUI.Box (new Rect(190, 60, 1020, 700), "");
			GUI.skin = robotSkin;
			if(baseLevel)
			{
				if (ShopBlock (new Rect(200,285,1000,120),generalWeaponPicture, weaponsCategory, weaponsCatergoryDescription,
					categoryPrice, categoryButtonText))
				{
					baseLevel = false;
					weapon1Level = true;
				}
				if (ShopBlock (new Rect(200,415,1000,120),generalPowerPicture, powersCategory, powersCategoryDescription,
					categoryPrice, categoryButtonText))
				{
					Debug.Log("clicked 1");
				}
			}
			if(weapon1Level)
			{
				if(ShopBlock (new Rect(200,285,1000,120), firstWeaponPicture, firstWeaponName, firstWeaponDescription, firstWeaponPrice,
					buyText))
				{
					//try to buy it
					//upgrade the weapon
					if(player.ScrapMetal >= firstWeaponPrice)
					{
						player.ScrapMetal = player.ScrapMetal - firstWeaponPrice;
						player.Guns[basicGunIndex].upgrade (player);
					}
					Debug.Log (player.gunDamage());
				}
				if (ShopBlock (new Rect(200,415,1000,120),secondWeaponPicture, secondWeaponName, secondWeaponDescription,
					secondWeaponPrice, buyText))
				{
					if(weapon2Bought)
					{
						//Try to buy it
						//upgrade the weapon
					}
					else
					{
						//create the weapon 2 entity on player
						weapon2Bought = true;
					}
				}
			}
		}
	}
	
	void DeactivateMenu()
	{
		menuOn = false;
		weaponsBaseLevel = false;
		powersBaseLevel = false;
		weapon1Level = false;
		power1Level = false;
	}
	
	void ActivateMenu()
	{
		menuOn = true;
		baseLevel = true;
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
