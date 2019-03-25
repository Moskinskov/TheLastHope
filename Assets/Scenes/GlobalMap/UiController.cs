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
    [SerializeField] public Text sityName;
    public void OpenCloseUi(PointController kek)
    {
        if (uiActive)
        {
            canUi.sortingOrder = 0;
            canMap.sortingOrder = 1;
            uiActive = false;
            turnPoint.setColor(Color.white);
            sityName.text = "";
        }
        else
        {
            canUi.sortingOrder = 1;
            canMap.sortingOrder = 0;
            turnPoint = kek;
            uiActive = true;
            sityName.text = "  Sity:  " + turnPoint.name;
            turnPoint.setColor(Color.yellow);
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
