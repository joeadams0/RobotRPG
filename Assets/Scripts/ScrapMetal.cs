using UnityEngine;
using System.Collections;

public class ScrapMetal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other){
		if(other.tag == "player"){
			Destroy(this.gameObject);
		}
	}
}
