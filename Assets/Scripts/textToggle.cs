using UnityEngine;
using System.Collections;

public class textToggle : MonoBehaviour {

	public KeyCode select_choice; // Button to press to toggle 'on' state
	public KeyCode switch_choice; // Button to press to toggle 'off' state

	public KeyCode confirm_choice; // Button to press to confirm
	public string selection; // name of selection

	private bool chosen;

	Animator textanimator;

	void Start () {
		textanimator = GetComponent<Animator> ();
	}

	void Update () {
		if (Input.GetKey (select_choice)) {
			chosen = true;
			textanimator.SetBool("selected", true);
		}
		if (Input.GetKey (switch_choice)) {
			chosen = false;
			textanimator.SetBool("selected", false);
		}

		if (Input.GetKey (confirm_choice) && chosen) {
			Debug.Log (selection);
			if (selection == "Stay") {
				transform.parent.parent.GetChild(0).GetComponent<Fitin>().gameEngine.EndGame();
			} else {
				transform.parent.parent.GetChild(0).GetComponent<Fitin>().gameEngine.ResumeGame();
			}
			// Deactivate Fitin detection script after one choice
			transform.parent.parent.GetChild(0).GetComponent<Fitin>().deactivate();
			HideText();
		}
	}

	// Hides text of all siblings
	void HideText() {
		SpriteRenderer[] siblingRenderers = transform.parent.GetComponentsInChildren<SpriteRenderer> ();
		for (int i=0; i<siblingRenderers.Length; i++) {
			siblingRenderers[i].renderer.enabled = false;
		}
	}
}
