using UnityEngine;
using System.Collections;

public class ShopUI : MonoBehaviour {
	
	public Texture2D item;
	public string names;
	public string des;
	public int price;
	public GUISkin robotSkin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI ()
	{
		GUI.skin = robotSkin;
		GUI.Box (new Rect(100, 120, 1000, 120), "dfsafdsafs");
		if (shopBlock (new Rect(200,400,1000,120),item, names,des,price, "buy"))
			Debug.Log("clicked");
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
	bool shopBlock (Rect boxPosition, Texture2D itemPicture, string name, string itemDescription, 
		int price, string buttonText)
	{
		GUI.Box (boxPosition,"");
		float itemStartPosition = boxPosition.x + 10f;
		GUI.Box (new Rect(itemStartPosition, boxPosition.y + 10f, 200, 100), itemPicture);
		itemStartPosition += 220f;
		GUI.Label(new Rect(itemStartPosition, boxPosition.y + 10f , 600, 25), name);
		GUI.Label(new Rect(itemStartPosition, boxPosition.y + 50f, 600, 25), itemDescription);
		itemStartPosition = boxPosition.xMax - 450f;
		GUI.Label (new Rect(itemStartPosition,boxPosition.y + 10f, 100, 25), price.ToString());
		return GUI.Button (new Rect(boxPosition.xMax - 210f, boxPosition.y + 10f, 200, 100), "Buy");
	}
}
