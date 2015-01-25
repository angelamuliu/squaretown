using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	private bool started;
	public GameObject player;

	// Use this for initialization
	void Start () {
		started = false;
	}
	
	// Update is called once per frame
	void Update () {

		if ( !started && Input.anyKey )
		{
			started = true;
			player.GetComponent<Rigidbody2D>().WakeUp();
			player.GetComponent<Controller>().enabled = true;
		}
	}

	public void EndGame() {

		Debug.Log ("GAME OVER");

		// pause the game
		player.GetComponent<Rigidbody2D>().Sleep();
		player.GetComponent<Controller>().enabled = false;

		

	}
}
