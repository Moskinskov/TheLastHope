using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private SLSystem sl;

    [SerializeField] public PathController path;
    [SerializeField] public UiController ui;

    [SerializeField] public Color roadClr;
    [SerializeField] public Color pointClr;
    [SerializeField] public Color currentClr;
    public void PointEnter(PointController point)
    {
        path.clearRoad(pointClr, currentClr);
        if (point != path.startPoint)
        {
            if (point.IsKeyPoint)
            {
                path.searchRoad(path.startPoint.num, point.num);
                if (ui.OpenCloseUi(point, path.GetDistance(point.num), path.GetNextCity()))
                {
                    path.drawRoad(roadClr);
                }
            }
            else
            {
                path.searchRoad(path.startPoint.num, point.num);
                if (!path.road.Contains(path.nextKeyPoint.num))
                {
                    if (point.IsOpenPoint)
                    {
                        path.searchRoad(path.startPoint.num, point.num);
                        if (ui.OpenCloseUi(point, path.GetDistance(point.num), path.GetNextCity()))
                        {
                            path.drawRoad(roadClr);
                        }
                    }
                    else
                    {
                        if (path.searchRoadKey(path.startPoint.num, path.nextKeyPoint.num, point.num))
                        {
                            if (ui.OpenCloseUi(point, path.GetDistance(point.num), path.GetNextCity()))
                            {
                                path.drawRoad(roadClr);
                            }
                        }
                    }
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        sl = new SLSystem();
        sl.SaveMapFile("KeyWest_0");
        sl.LoadMapFile();
        path.clearRoad(pointClr, currentClr);
    }

}
