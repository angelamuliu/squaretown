using UnityEngine;
using System.Collections;

public class TriggerStalactite : MonoBehaviour {

	public bool stalacFall = false;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player"){
			stalacFall = true;
			
		}
		else{
			stalacFall = false;
		}
	}
}
