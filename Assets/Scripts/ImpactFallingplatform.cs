using UnityEngine;
using System.Collections;

public class ImpactFallingplatform : MonoBehaviour {


	public float safetime = 1; // Time player has until platform starts falling
	public float jumpdelay = 1; // Time player has while platform is falling, can still jump
	public float respawntime = 5; // Time it'll take for platform to respawn

	private bool falling = false;
	private int fadespeed = 30;

	private float startx;
	private float starty;
	private Quaternion startRotation;

	private Color darktint = new Color(242.0f/255, 242.0f/255, 242.0f/255, 1);

	SpriteRenderer mySprite;
	PolygonCollider2D myCollider;

	TriggerStalactite trig; 
	
	void Start () {
		mySprite = GetComponent<SpriteRenderer>();
		myCollider = GetComponent<PolygonCollider2D>();
		startx = transform.position.x;
		starty = transform.position.y;
		startRotation = transform.rotation;

		trig = GetComponent<TriggerStalactite>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(trig.stalacFall == true){
			//rigidbody2D.WakeUp ();
			rigidbody2D.Sleep();
		}

		// Fading out while falling
		if (falling) {
			// Dark tint fading to transparency
			mySprite.color = new Color(242.0f/255, 242.0f/255, 242.0f/255, fadespeed/30f);
		}
	}

	void Update(){


	}


	IEnumerator OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.tag == "Stalactite") {
			// Player can safely sit on this platform for a bit
			mySprite.color = darktint;
			yield return new WaitForSeconds (safetime);
			falling = true;
			//rigidbody2D.WakeUp();

			yield return new WaitForSeconds (jumpdelay);
			myCollider.enabled = false;

			yield return new WaitForSeconds (respawntime); // Set the fall time
			//respawn ();
		}
	}

	// Reset the platform
/*	void respawn() {
		falling = false; fadespeed = 30;
		rigidbody2D.Sleep();
		mySprite.color = new Color (1, 1, 1, 1);
		transform.position = new Vector3(startx, starty, 0);
		transform.rotation = startRotation;
		myCollider.enabled = true;
	}*/


	
}
