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

		[SerializeField]
		private AnimationCurve curve = AnimationCurve.Linear(0.0f, 0.0f, 0.3f, 1.0f);
		[SerializeField]
		private float t = 1.0f;        // don't know what is that


		[SerializeField] Transform camTransform;
		[SerializeField] float shakeDuration = 0f; // How long the object should shake for.
		[SerializeField] float shakeAmount = 0.7f; // Amplitude of the shake. A larger value shakes the camera harder.
		[SerializeField] float decreaseFactor = 1.0f;

		//public AnimationCurve Curve { get { return curve; } set { curve = value; } }
		//public float T { get { return t; } set { t = value; } }
		//public Transform CamTransform { get { return camTransform; } set { camTransform = value; } }
		//public float ShakeDuration { get { return shakeDuration; } set { shakeDuration = value; } }
		//public float ShakeAmount { get { return shakeAmount; } set { shakeAmount = value; } }
		//public float ВecreaseFactor { get { return decreaseFactor; } set { decreaseFactor = value; } }

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

			//if (Input.mousePosition.y > Camera.main.pixelHeight * yBoard && cameraPos.z < zRange)
			//{
			//	cameraPos.z = cameraPos.z + steps;
			//}
			//else if (Input.mousePosition.y < Camera.main.pixelHeight * (1 - yBoard) && cameraPos.z > -zRange)
			//{
			//	cameraPos.z = cameraPos.z - steps;
			//}

			//IMPLEMENT MOUSE LOOK

			Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraPos, Time.deltaTime*0.5f);
		}

		public void CameraShake()
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
			var originalPos = camTransform.localPosition;

			if (shakeDuration > 0)
			{
				camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
				shakeDuration -= Time.deltaTime * decreaseFactor;
			}
			else
			{
				shakeDuration = 0f;
				camTransform.localPosition = originalPos;
			}
		}

		// Debug 
		// DONT'T FORGET TO MOVE UPDATE TO RENDER MANAGER

	}
}