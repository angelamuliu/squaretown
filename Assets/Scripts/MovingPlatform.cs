using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public float x_speed = 0.0f;
	public float y_speed = 0.0f;
	public float x_min = 0.0f;
	public float x_max = 0.0f;
	public float y_min = 0.0f;
	public float y_max = 0.0f;

	private float x_offset, y_offset;
	private Vector2 position;

	// Use this for initialization
	void Start () {
		x_offset = 0.0f;
		y_offset = 0.0f;
		position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2(position.x + x_offset, position.y + y_offset);
	}

	void FixedUpdate(){
		float x_old = x_offset;
		float y_old = y_offset;

		x_offset += x_speed;
		y_offset += y_speed;

		if (x_offset < x_min || x_offset > x_max)
		{
			x_speed = -x_speed;
			x_offset = x_old + x_speed;
		}
		if (y_offset < y_min || y_offset > y_max)
		{
			y_speed = -y_speed;
			y_offset = y_old + y_speed;
		}
	}
}
