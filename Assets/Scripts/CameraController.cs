using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Render
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] [Range(1, 20)] private int cameraSpeed = 1;            //delay of camera moves; 1 - minimum
		[SerializeField] [Range(5, 10)] private float zRange = 5;
		[SerializeField] [Range(10, 20)] private float xRange = 10;
		[SerializeField] [Range(0.0f, 1.0f)] private float xBoard = 0.8f;       //when camera has to move horizontal; default: camera moves right when cursor reaches 80% of display height (and left - 20%)
		[SerializeField] [Range(0.0f, 1.0f)] private float yBoard = 0.5f;       //when camera has to move vertical
		[SerializeField] private int steps = 4;                                 //how fast camera coordinates changes

		[SerializeField] [Range(60, 1000)] float cameraSpeedinFrames = 60.0f;

		private Vector3 cameraPos;

		public AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
		public float t = 0.1f;        // don't know what is that
		public void CameraUpdate()
		{
			cameraPos = Camera.main.transform.position;


			if (Input.mousePosition.x > Camera.main.pixelWidth * xBoard && cameraPos.x < xRange)
			{
				cameraPos.x = cameraPos.x + steps;
			}
			else if (Input.mousePosition.x < Camera.main.pixelWidth * (1 - xBoard) && cameraPos.x > -xRange)
			{
				cameraPos.x = cameraPos.x - steps;
			}

			if (Input.mousePosition.y > Camera.main.pixelHeight * yBoard && cameraPos.z < zRange)
			{
				cameraPos.z = cameraPos.z + steps;
			}
			else if (Input.mousePosition.y < Camera.main.pixelHeight * (1 - yBoard) && cameraPos.z > -zRange)
			{
				cameraPos.z = cameraPos.z - steps;
			}

			Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraPos, curve.Evaluate(t));
		}

		// Debug 
		// DONT'T FORGET TO MOVE UPDATE TO RENDER MANAGER
		void Update()
		{
			CameraUpdate();
		}

	}
}