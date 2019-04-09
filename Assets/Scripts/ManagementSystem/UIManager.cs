/// Limerence Games
/// The Last Hope
/// Curator: Dmitri Kuzmin
/// to be commented

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.UI;

namespace TheLastHope.Management
{

	public class UIManager : MonoBehaviour
	{
		[SerializeField]
		private Image _tempProgressBar;
		private float _tempWholePath;
		private float _tempCurrentPos;
		private UIOverlayController[] controllers;

		// Start is called before the first frame update
		public void Init(SceneData sceneData)
		{
			_tempWholePath = sceneData.LineLength;
			controllers = FindObjectsOfType<UIOverlayController>();
			foreach (UIOverlayController controller in controllers)
			{
				sceneData.UiOverlayControllers.Add(controller);
				controller.Init();
			}
		}

		public void UIUpdate(SceneData sceneData, PathLengthCounter pathCounter)
		{      
			foreach (UIOverlayController controller in sceneData.UiOverlayControllers)
			{

                controller.OverlayUpdate(sceneData);
				_tempProgressBar.fillAmount = (float)pathCounter.CurrentLine / (float)sceneData.LinesOverall;
			}
		}
	}
}