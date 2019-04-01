using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField] public Canvas canMap;
    [SerializeField] public Canvas canUi;
    [SerializeField] private bool uiActive;
    [SerializeField] private PointController turnPoint;
    [SerializeField] public Text targetName;
    [SerializeField] public Text distanceFromTarget;
    [SerializeField] public Text nextCity;

	private Color _initColor;

	public void LoadLevel()
	{
		Application.LoadLevel("MainScene");
	}

	public bool OpenCloseUi(PointController target, int dist, PointController nextCity)
    {
		_initColor = new Color(17, 86, 33);
		turnPoint = target;
        if (uiActive && target == turnPoint)
        {
            canUi.sortingOrder = 0;
            canMap.sortingOrder = 1;
            uiActive = false;
            target.setColor(_initColor);
            targetName.text = "";
            return false;
        }
        else
        {
            canUi.sortingOrder = 1;
            canMap.sortingOrder = 0;
            uiActive = true;
            targetName.text = "  Selected city:  " + target.name;
            distanceFromTarget.text = "  Distance to " + target.name + " :  " + dist;
            this.nextCity.text = "  Next city where are you going :  " + nextCity.name;
            target.setColor(Color.yellow);
            return true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        uiActive = false;
    }

}
