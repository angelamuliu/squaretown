using UnityEngine;
using System.Collections;

public class Fitin : MonoBehaviour {

	public Transform playerChecker;
	float checkradius = 0.2f;
	public LayerMask whatIsPlayer;

	private bool fitin = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (fitin) {
			Debug.Log ("GAME OVER");
		}
	
	}

	private void CollisionDetection() {
		fitin = Physics2D.OverlapCircle (playerChecker.position, checkradius, whatIsPlayer);
	}
}
