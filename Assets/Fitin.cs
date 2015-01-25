using UnityEngine;
using System.Collections;

public class Fitin : MonoBehaviour {


	private bool fitin = false;
	public MainMenu gameEngine;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			gameEngine.EndGame();
		}
	}

}
