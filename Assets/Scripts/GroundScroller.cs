using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroller : MonoBehaviour
{

	[SerializeField] float scrollX = 0.0f;
	[SerializeField] float scrollY = 0.0f;
	[SerializeField] private float speed;
	private float _oldSpeed;

    public float ScrollX { get { return scrollX; } set { scrollX = value; } }
    public float ScrollY { get { return scrollY; } set { scrollY = value; } }


    private void Start()
	{
		_oldSpeed = speed;
	}

	// Update is called once per frame
	void Update()
    {
		float OffsetX = Time.time * scrollX*speed;
		float OffsetY = Time.time * scrollY*speed;
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2(OffsetX, OffsetY);

		if (speed != _oldSpeed) 
		{
			ChangeSpeed(_oldSpeed, speed);
		}

    }

	void ChangeSpeed(float _os, float _sp)
	{
		speed = Mathf.Lerp(_oldSpeed, speed, Time.deltaTime*5.0f);
		_oldSpeed = speed;
	}
}
