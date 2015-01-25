using UnityEngine;
using System.Collections;

public class Fitin : MonoBehaviour
{

		private bool fitin = false;
		public MainMenu gameEngine;
		public GameObject dialogue;

		void Start() {
			dialogue.SetActive (false);
		}

		void OnTriggerEnter2D (Collider2D other)
		{
				if (other.tag == "Player") {
						gameEngine.Pause_Game ();
						activateDialogue();
				}
		}

		public void deactivate ()
		{
				GetComponent<CircleCollider2D> ().enabled = false;
		}
	
		// Show and activate listeners for the dialogue
		public void activateDialogue () {
			dialogue.SetActive (true);
			foreach (Transform child in dialogue.transform) {
				child.gameObject.SetActive(true);
			}
		}

}



	