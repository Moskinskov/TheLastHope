using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Управляет всеми компонентами на карте. Без него
/// ни одна мыш не пошевельнется.
/// </summary>
public class MapManager : MonoBehaviour
{

    #region Public variables
    /// <summary>
    /// Классы , с которыми работает MapManager
    /// path - класс , через который можно общаться с графом
    /// </summary>
    [SerializeField] public PathController path;
    /// <summary>
    /// ui - панелька с интерфейсом, который можно вызывать и убирать
    /// </summary>
    [SerializeField] public UiController ui;
    /// <summary>
    /// Переменные для закраски разных типов вершин графа. 
    /// roadClr - цвет вершины, которая входит в 
    /// построенный маршрут
    /// </summary>
    [SerializeField] public Color roadClr;
    /// <summary>
    /// pointClr - обычный цвет вершины, когда она не входит в маршрут
    /// и не является стартовой
    /// </summary>
    [SerializeField] public Color pointClr;
    /// <summary>
    /// currentClr - цвет стартовой точки
    /// </summary>
    [SerializeField] public Color currentClr;
    #endregion

    #region Public Methods
    /// <summary>
    /// Метод обрабатывает нажатие точки
    /// </summary>
    /// <param name="point"> Точка , которую нажал игрок</param>
    public void PointEnter(PointController point)
    {
        path.clearRoad(pointClr, currentClr);
        if (point != path.startPoint)
        {
            if (point.IsKeyPoint)
            {
                // point - ключевая точка
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
                        //point - открытая точка для перемещения
                        path.searchRoad(path.startPoint.num, point.num);
                        if (ui.OpenCloseUi(point, path.GetDistance(point.num), path.GetNextCity()))
                        {
                            path.drawRoad(roadClr);
                        }
                    }
                    else
                    {
                        //point закрытая точка для перемещния
                        //строим дорогу от стартовой до ключевой
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
    #endregion

    #region Unity methods
    // Start is called before the first frame update
    void Start()
    {
        path.Init();
        path.clearRoad(pointClr, currentClr);
    }
    #endregion

}
