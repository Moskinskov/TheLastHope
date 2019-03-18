using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.UI;

namespace TheLastHope.Management
{

	public class UIManager : MonoBehaviour
	{
		// Start is called before the first frame update
		public void Init()
		{
			UIOverlayController[] controllers;
			controllers = FindObjectsOfType<UIOverlayController>();
			foreach (UIOverlayController controller in controllers)
			{
				controller.Init();
			}
		}
	}
}