using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	private ColorShape colorshape;

	public KeyCode up;
	public KeyCode down;
	public KeyCode right;
	public KeyCode left;

	public float jumpForce = 1;
	public float movespeed = 1;

	// Keeping track of player contact with ground
	bool grounded = false; // Start false b/c player starts out falling
	private Vector2 delta;
	public Transform side1Check;
	public Transform side2Check;
	public Transform side3Check;
	float groundradius = 0.2f;
	public LayerMask whatIsGround;

	private bool gamestart = false;

	AudioSource jump_sound; AudioSource powerup_1; AudioSource powerup_2; AudioSource powerup_3;

	void Start() {
		colorshape = GetComponent <ColorShape> ();
		delta = Vector2.zero;
		AudioSource[] audios = GetComponents<AudioSource>();
		jump_sound = audios [0];
		powerup_1 = audios [1];
		powerup_2 = audios [2];
		powerup_3 = audios [3];
	}

	void FixedUpdate () {
		rigidbody2D.velocity = new Vector2( delta.x, rigidbody2D.velocity.y + delta.y );

		// wall jumping if you are a square!
		if (colorshape.color == 1)
		{
			grounded = Physics2D.OverlapCircle (side1Check.position, groundradius, whatIsGround)
					|| Physics2D.OverlapCircle (side2Check.position, groundradius, whatIsGround)
					|| Physics2D.OverlapCircle (side3Check.position, groundradius, whatIsGround);
		}
		else
		{
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
	}

	void Update() {

		if (!gamestart) {
			if (Input.anyKey) {
				gamestart = true;
				rigidbody2D.isKinematic = false;
			}
			
		} else {
			float x = 0.0f, y = 0.0f;
			
			// Horizontal movement
			if (Input.GetKey (right)) 
			{
				x = movespeed;
			} 
			else if (Input.GetKey (left)) {
				x = -movespeed;
			}
			else {
				x = 0;
			}
			if (colorshape.color == 2)
				x *= 2;
			
			// Vertical movement
			if (grounded && Input.GetKey (up)) {
				y = jumpForce;
				jump_sound.Play ();
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, jumpForce);
			} else {
				y = 0;
			}
			if (colorshape.color == 0)
				y *= 2;
			
			delta = new Vector2(x, y);
		}
	}


	// Deactivate objects player collides with if they are pickups
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Tri_pickup" && colorshape.color != 0) {
			if (other.gameObject.GetComponent<Pickup>().is_enabled)
			{
				powerup_1.Play ();
				other.gameObject.GetComponent<Pickup>().Disable();
				colorshape.pickup (0);
			}	
		}
		if (other.gameObject.tag == "Sqr_pickup" && colorshape.color != 1) {
			if (other.gameObject.GetComponent<Pickup>().is_enabled)
			{
				powerup_2.Play ();
				other.gameObject.GetComponent<Pickup>().Disable();
				colorshape.pickup (1);
			}
		}
		if (other.gameObject.tag == "Pen_pickup" && colorshape.color != 2) {
			if (other.gameObject.GetComponent<Pickup>().is_enabled)
			{
				powerup_3.Play ();
				other.gameObject.GetComponent<Pickup>().Disable();
				colorshape.pickup (2);
			}
		}
	}









}
