using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	private ColorShape colorshape;

	public KeyCode up;
	public KeyCode down;
	public KeyCode right;
	public KeyCode left;
	public float speed = 0;
	public float jumpForce = 1;
	public float movespeed = 1;

	// Keeping track of player contact with ground
	bool grounded = false; // Start false b/c player starts out falling
	public Transform side1Check;
	public Transform side2Check;
	public Transform side3Check;
	float groundradius = 0.2f;
	public LayerMask whatIsGround;

	AudioSource jump_sound; AudioSource powerup_1; AudioSource powerup_2; AudioSource powerup_3;

	void Start() {
		colorshape = GetComponent <ColorShape> ();
		AudioSource[] audios = GetComponents<AudioSource> ();
		jump_sound = audios [0];
		powerup_1 = audios [1];
		powerup_2 = audios [2];
		powerup_3 = audios [3];
	}

	void FixedUpdate () {
		// Checking if any of its sides are touching the ground, first detect which is the lowest to use
		if (side1Check.position.y < side2Check.position.y) {
			if (side1Check.position.y < side3Check.position.y) {
				grounded = Physics2D.OverlapCircle (side1Check.position, groundradius, whatIsGround);
			} else { // side1 is greater than side 3 but less than side 2
				grounded = Physics2D.OverlapCircle (side3Check.position, groundradius, whatIsGround);
			}
		} else { // side 1 is greather than side 2
			if (side2Check.position.y < side3Check.position.y) {
				grounded = Physics2D.OverlapCircle (side2Check.position, groundradius, whatIsGround);
			} else { // side 2 is less than side 1 but greater than side 3
				grounded = Physics2D.OverlapCircle (side3Check.position, groundradius, whatIsGround);
			}
		}
	}

	void Update() {
		// Horizontal movement
		if (Input.GetKey (right)) 
		{
			rigidbody2D.velocity = new Vector2 (movespeed, rigidbody2D.velocity.y);
		} 
		else if (Input.GetKey (left)) {
			rigidbody2D.velocity = new Vector2 (-movespeed, rigidbody2D.velocity.y);
		}
		else {
			rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
		}

		// Vertical movement
		if (grounded && Input.GetKey (up)) {
			jump_sound.Play ();
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, jumpForce);
		} else {
			// Maybe put triples jumps here one day...
		}

	}

	// Deactivate objects player collides with if they are pickups
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Tri_pickup") {
			powerup_1.Play ();
			other.gameObject.SetActive (false);
			colorshape.pickup (0);
		}
		if (other.gameObject.tag == "Sqr_pickup") {
			powerup_2.Play ();
			other.gameObject.SetActive (false);
			colorshape.pickup (1);
		}
		if (other.gameObject.tag == "Pen_pickup") {
			powerup_3.Play ();
			other.gameObject.SetActive (false);
			colorshape.pickup (2);
		}
	}









}
