using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

	public ColorShape player;
	private float time_consumed;
	private bool is_enabled;
	private BoxCollider2D box;
	private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		time_consumed = 0.0f;
		is_enabled = true;
		box = GetComponent<BoxCollider2D>();
		sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Disable(){
		is_enabled = false;
		time_consumed = Time.fixedTime;
		sprite.color = new Color(1,1,1,0);
		box.enabled = false;
	}

	void OnBecameVisible(){
		if (!is_enabled && player.change_time > time_consumed)
		{
			is_enabled = true;
			box.enabled = true;
			sprite.color = new Color(1,1,1,1);
		}
	}
}
