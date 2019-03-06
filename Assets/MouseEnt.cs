using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEnt : MonoBehaviour
{
    public GameObject uiobj;
     void OnMouseEnter()
    {
        uiobj.SetActive(true);
    }
     void OnMouseExit()
    {
        uiobj.SetActive(false);
    }
}