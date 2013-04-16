using UnityEngine;
using System.Collections;

public class ShopUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	///<description>
	///The basic building block used in the shop's interface
	///Contains a picture, a name, a short description for the item, the price, and a buy button 
	///</description>
	bool shopBlock (Rect boxPosition, Texture2D itemPicture, string name, string itemDescription, 
		string price, string buttonText)
	{
		float itemStartPosition = boxPosition.x + 10f;
		return true;
	}
}
