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

	// Called by the fitin trigger to start a prompt for user input
	// Temporarily turns off character physics and input until Player makes a choice
	// Locks player input to two keys
	public void Pause_Game() {
		player.GetComponent<Rigidbody2D> ().Sleep ();
		player.GetComponent<Controller>().enabled = false;
	}

	public void ResumeGame() {
		player.GetComponent<Rigidbody2D>().WakeUp();
		player.GetComponent<Controller>().enabled = true;
	}

	// Officially ends the game
	public void EndGame() {
		Debug.Log ("GAME OVER");
	}

}
