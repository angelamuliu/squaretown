using UnityEngine;
using System.Collections;

public class Fallingplatform : MonoBehaviour {


	public float safetime = 1; // Time player has until platform starts falling
	public float jumpdelay = 1; // Time palyer has while platform is falling, can still jump
	public float respawntime = 5; // Time it'll take for platform to respawn

	private bool falling = false;
	private int fadespeed = 30;

	private float startx;
	private float starty;

	private Color darktint = new Color(242.0f/255, 242.0f/255, 242.0f/255, 1);

	SpriteRenderer mySprite;
	PolygonCollider2D collider;
	
	void Start () {
		mySprite = GetComponent<SpriteRenderer> ();
		collider = GetComponent<PolygonCollider2D>();
		startx = transform.position.x;
		starty = transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Fading out while falling
		if (falling) {
			// Dark tint fading to transparency
			mySprite.color = new Color(242.0f/255, 242.0f/255, 242.0f/255, fadespeed/30f);
			fadespeed --;
		}
	}

	IEnumerator OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log ("Hit");
			// Player can safely sit on this platform for a bit
			mySprite.color = darktint;
			yield return new WaitForSeconds (safetime);
			falling = true;
			rigidbody2D.isKinematic = false;

			yield return new WaitForSeconds (jumpdelay);
			collider.enabled = false;

			yield return new WaitForSeconds (respawntime); // Set the fall time
			respawn ();
		}
	}

	// Reset the platform
	void respawn() {
		falling = false; fadespeed = 30;
		rigidbody2D.isKinematic = true;
		mySprite.color = new Color (1, 1, 1, 1);
		transform.position = new Vector3(startx, starty, 0);
		collider.enabled = true;
	}


	
}
