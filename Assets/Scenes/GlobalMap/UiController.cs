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
    public bool OpenCloseUi(PointController target, int dist, PointController nextCity)
    {
        turnPoint = target;
        if (uiActive && target == turnPoint)
        {
            canUi.sortingOrder = 0;
            canMap.sortingOrder = 1;
            uiActive = false;
            targetName.text = "";
            return false;
        }
        else
        {
            canUi.sortingOrder = 1;
            canMap.sortingOrder = 0;
            uiActive = true;
            targetName.text = "  City:  " + target.name;
            distanceFromTarget.text = "  Distance:  " + dist;
            this.nextCity.text = "  Next city where are you going :  " + nextCity.name;
            return true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        uiActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
