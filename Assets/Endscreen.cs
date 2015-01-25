using UnityEngine;
using System.Collections;

public class Endscreen : MonoBehaviour {
	
	// Click any button to reset
	void Update () {
		if (Input.anyKey) {
			Debug.Log ("ANYKEY");
		}
	}

	public void timedReset() {
		Invoke("resetgame", 2);
	}

	void resetgame() {
		transform.parent.GetChild(0).GetComponent<Fitin>().gameEngine.Restart();
	}


}

