using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroller : MonoBehaviour
{

	public float ScrollX = 0.0f;
	public float ScrollY = 0.0f;
	[SerializeField] private float speed;
	private float _oldSpeed;

	private void Start()
	{
		_oldSpeed = speed;
	}

	// Update is called once per frame
	void Update()
    {
		float OffsetX = Time.time * ScrollX*speed;
		float OffsetY = Time.time * ScrollY*speed;
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
