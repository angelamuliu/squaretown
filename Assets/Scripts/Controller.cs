using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	private ColorShape colorshape;

	public KeyCode up;
	public KeyCode down;
	public KeyCode right;
	public KeyCode left;

	public float pushForce = 1; // pushing away from walls
	public float jumpForce = 1; // jumping vertically
	public float movespeed = 1; // running
	public float wall_lockout_time = 0.5f;

	// Keeping track of player contact with surfaces
	private Collider2D ground = null;
	private Collider2D wall = null;
	private Collider2D platform = null;

	// Keep momentum after wall jumps
	private float wall_jump_time = 0.0f;

	// desired velocity 
	private Vector2 desiredVelocity = new Vector2(0, 0);

	// edge collision detectors
	public Transform side1Check;
	public Transform side2Check;
	public Transform side3Check;
	float checkradius = 0.2f;

	public LayerMask whatIsGround;
	public LayerMask whatIsWall;
	public LayerMask movingPlatforms;

	AudioSource jump_sound; AudioSource powerup_1; AudioSource powerup_2; AudioSource powerup_3;
	private bool jumpsound = false;

	void Start() {
		colorshape = GetComponent <ColorShape> ();
		desiredVelocity = Vector2.zero;
		AudioSource[] audios = GetComponents<AudioSource>();
		jump_sound = audios [0];
		powerup_1 = audios [1];
		powerup_2 = audios [2];
		powerup_3 = audios [3];
	}

	private void CollisionDetection()
	{
		// am i touching the ground or a moving platform?
		if (side1Check.position.y < side2Check.position.y) {
			if (side1Check.position.y < side3Check.position.y) {
				ground = Physics2D.OverlapCircle (side1Check.position, checkradius, whatIsGround);
				platform = Physics2D.OverlapCircle (side1Check.position, checkradius, movingPlatforms);
			} else { // side1 is greater than side 3 but less than side 2
				ground = Physics2D.OverlapCircle (side3Check.position, checkradius, whatIsGround);
				platform = Physics2D.OverlapCircle (side3Check.position, checkradius, movingPlatforms);
			}
		} else { // side 1 is greather than side 2
			if (side2Check.position.y < side3Check.position.y) {
				ground = Physics2D.OverlapCircle (side2Check.position, checkradius, whatIsGround);
				platform = Physics2D.OverlapCircle (side2Check.position, checkradius, movingPlatforms);
			} else { // side 2 is less than side 1 but greater than side 3
				ground = Physics2D.OverlapCircle (side3Check.position, checkradius, whatIsGround);
				platform = Physics2D.OverlapCircle (side3Check.position, checkradius, movingPlatforms);
			}
		}

		// if wall-jumping enabled: am i touching a wall?
		if ( colorshape.is_orange() )
		{
			wall = Physics2D.OverlapCircle(side1Check.position, checkradius, whatIsWall);
			if ( wall == null )
				wall = Physics2D.OverlapCircle(side2Check.position, checkradius, whatIsWall);
			if ( wall == null )
				wall = Physics2D.OverlapCircle(side3Check.position, checkradius, whatIsWall);
		}
		else
		{
			wall = null;
		}
	}

	private float horizontal_input()
	{
		if ( Input.GetKey(right) )
		{
			return movespeed;
		}
		else if ( Input.GetKey(left) )
		{
			return -movespeed;
		}
		else
		{
			return 0.0f;
		}
	}

	private void ComputeVelocity()
	{
		float x = rigidbody2D.velocity.x;
		float y = rigidbody2D.velocity.y;

		if ( ground )
		{
			x = horizontal_input();

			// simple jump
			if ( Input.GetKey(up) )
			{
				y = jumpForce;
				jumpsound = true;
			}
		}
		else if ( platform )
		{
			x = platform.gameObject.GetComponent<MovingPlatform>().x_speed + horizontal_input();

			// simple jump
			if ( Input.GetKey(up) )
			{
				y = jumpForce;
				jumpsound = true;
			}
		}
		else if ( wall )
		{
			float my_x = transform.position.x;
			float wall_x = wall.gameObject.transform.position.x;

			// -1 if the wall is to the RIGHT of me
			float dir = wall_x > my_x ? -1.0f : 1.0f;

			// wall jump
			if ( Input.GetKey(up) )
			{
				x = dir * pushForce;
				y = jumpForce;
				jumpsound = true;
				wall_jump_time = Time.fixedTime;
			}
			else
			{
				x = horizontal_input();
			}
		}
		else // freefall
		{
			// after a walljump, lock in horizontal momentum for a while
			if ( wall_jump_time > 0.0f )
			{
				if ( Time.fixedTime - wall_jump_time > wall_lockout_time )
					wall_jump_time = 0.0f;
			}
			else
			{
				x = horizontal_input();
			}		
		}

		desiredVelocity = new Vector2(x, y);
	}

	void FixedUpdate () {
		CollisionDetection();
		rigidbody2D.velocity = desiredVelocity;
		if ( jumpsound )
		{
			jumpsound = false;
			jump_sound.Play();
		}
	}

	void Update() {
		ComputeVelocity();
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
