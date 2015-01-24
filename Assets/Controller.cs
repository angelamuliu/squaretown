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
	public Transform groundCheck;
	float groundradius = 0.2f;
	public LayerMask whatIsGround;


	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundradius, whatIsGround);
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
