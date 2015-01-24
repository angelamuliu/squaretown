using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {


	public KeyCode up;
	public KeyCode down;
	public KeyCode right;
	public KeyCode left;
	public float speed = 0;
	
	void FixedUpdate () {
		if (Input.GetKey(up)) {
			transform.Translate(new Vector2(0.0f, speed+0.1f));
		}
		
		if (Input.GetKey(down)) {
			transform.Translate(new Vector2(0.0f, (-1)*(speed+0.1f)));
		}
		
		if (Input.GetKey(right)) {
			transform.Translate(new Vector2(speed+0.1f, 0.0f));
		}
		if (Input.GetKey(left)){
			transform.Translate(new Vector2((-1)*(speed+0.1f), 0.0f));
		}
	}
}
