using System;
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
    [SerializeField] public MapManager mapMan;
    [SerializeField] public bool IsKeyPoint = false;
    [SerializeField] public bool IsOpenPoint = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        mapMan.PointEnter(this);
    }

    // Start is called before the first frame update

    public void setColor(Color clr)
    {
        GetComponent<Image>().color = clr;
    }
    void Start()
    {
        //GetComponent<Image>().color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
