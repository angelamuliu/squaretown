using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	
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
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, jumpForce);
		} else {
			// Maybe put triples jumps here one day...
		}

	}









}
