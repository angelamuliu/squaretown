using UnityEngine;
using System.Collections;

public class ColorShape : MonoBehaviour {

	public Sprite r3;
	public Sprite r2o1;
	public Sprite r1o2;
	public Sprite o3;
	public Sprite r2y1;
	public Sprite r1y2;
	public Sprite y3;
	public Sprite o2r1;
	public Sprite o1r2;
	public Sprite o2y1;
	public Sprite o1y2;
	public Sprite y2r1;
	public Sprite y1r2;
	public Sprite y2o1;
	public Sprite y1o2;

	public SpriteRenderer sprite;
	public int color;
	private int nextColor;
	private int count;

	// Use this for initialization
	void Start () {
		color = 0;
		nextColor = 0;
		count = 0;
		sprite.sprite = r3;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setColor(int newcolor)
	{
		color = newcolor;
		nextColor = color;

		switch (color){
			case 0: sprite.sprite = r3;
					break;
			case 1: sprite.sprite = o3;
					break;
			case 2: sprite.sprite = y3;
					break;
		}
	}

	public void pickup (int type){
		// same as my current nextColor
		if (type == color)
			return;

		// second or third pickup of same nextColor
		if (type == nextColor)
		{
			count++;
			if (count >= 3)
			{
				color = nextColor;
				if (nextColor == 0)
					sprite.sprite = r3;
				else if (nextColor == 1)
					sprite.sprite = o3;
				else if (nextColor == 2)
					sprite.sprite = y3;
			}
			else if (count == 2)
			{
				if (color == 0)
				{
					if (nextColor == 1)
					{
						sprite.sprite = r1o2;
					}
					else if (nextColor == 2)
					{
						sprite.sprite = r1y2;
					}
				}
				else if (color == 1)
				{
					if (nextColor == 0)
					{
						sprite.sprite = o1r2;
					}
					else if (nextColor == 2)
					{
						sprite.sprite = o1y2;
					}
				}
				else if (color == 2)
				{
					if (nextColor == 0)
					{
						sprite.sprite = y1r2;
					}
					else if (nextColor == 1)
					{
						sprite.sprite = y1o2;
					}
				}
			}
		}
		// first pickup, or switching to a new nextColor
		else
		{
			nextColor = type;
			switch (color) {
				// red
				case 0: if (nextColor == 1) sprite.sprite = r2o1;
						else sprite.sprite = r2y1;
						break;

				// orange
				case 1: if (nextColor == 0) sprite.sprite = o2r1;
						else sprite.sprite = o2y1;
						break;

				// yellow
				case 2: if (nextColor == 0) sprite.sprite = y2r1;
						else sprite.sprite = y2o1;
						break;
			}
		}
	}
}
