using UnityEngine;
using System.Collections;

public class Lighting : MonoBehaviour {
	
	private GameObject player;
	
	// Use this for initialization
	void Start () {
		player =  GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(player.transform.position.x,50,player.transform.position.z);
	}
}
