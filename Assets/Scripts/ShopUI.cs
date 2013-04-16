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
		if (shopBlock (new Rect(200,600,1000,120),item, names,des,price, "buy"))
			Debug.Log("clicked");
	}
	
	/// <summary>
	/// Shops the block.
	/// </summary>
	/// <returns>
	/// The block.
	/// </returns>
	/// <param name='boxPosition'>
	/// If set to <c>true</c> box position.
	/// </param>
	/// <param name='itemPicture'>
	/// If set to <c>true</c> item picture.
	/// </param>
	/// <param name='name'>
	/// If set to <c>true</c> name.
	/// </param>
	/// <param name='itemDescription'>
	/// If set to <c>true</c> item description.
	/// </param>
	/// <param name='price'>
	/// If set to <c>true</c> price.
	/// </param>
	/// <param name='buttonText'>
	/// If set to <c>true</c> button text.
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
