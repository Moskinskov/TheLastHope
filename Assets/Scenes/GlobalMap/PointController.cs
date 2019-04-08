﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public struct Points
{
    public PointController point;
    public int dist;
}

public class PointController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public int num;
    [SerializeField] public Points[] Neightboring;
    [SerializeField] public PathController path;
    [SerializeField] public UiController ui;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (this != path.startPoint)
        {
            //path.clearRoad();
            //path.searchRoad(this.num);
            //if (ui.OpenCloseUi(this, path.GetDistance(this.num), path.GetNextCity()))
            //{
            //    path.drawRoad(Color.blue, this.num);
            //}
        }
    }

    public void setColor(Color clr)
    {
        GetComponent<Image>().color = clr;
    }

}
