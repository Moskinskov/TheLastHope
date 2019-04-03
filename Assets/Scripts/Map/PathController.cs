using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PathController - обеспечивет работу с графом
/// Основной ф-онал:
/// 1) Построение маршрута до определенной точки из стартовой
/// 2) Построение маршрута до определенной точки через еще одну
/// 3) Закраска маршрута
/// 4) Закраска и привод графа к первоначальному цвету
/// </summary>
public class PathController : MonoBehaviour
{
    #region Public variables
    /// <summary>
    /// road - список хранящий маршрут , где road[i] - номер i-го города
    /// на маршруте из точки старта.
    /// </summary>
    public List<int> road;
    /// <summary>
    /// Стартовая/текущаяя точка на графе, где сейчас находится игрок
    /// </summary>
    [SerializeField] public PointController startPoint;
    /// <summary>
    /// Следующая ключевая точка.
    /// Между стартовой и ключевой точкой должно быть ограниченное кол-во
    /// точек. Если игрок доходит до ключевой точки, то все точки между первой
    /// и ключевой становятся открытыми. После чего ключевая точка меняется.
    /// </summary>
    [SerializeField] public PointController nextKeyPoint; 
    /// <summary>
    /// Число вершин в графе
    /// </summary>
    [SerializeField] public int countPoints;
    #endregion

    #region Private variables
    /// <summary>
    /// marix[i,j] - расточния между i-ой вершиной и j-ой
    /// </summary>
    [SerializeField] private int[,] matrix; 
    /// <summary>
    /// matrix2[i,j] - номер первой точки на маршруте из iой вершины в j-ую
    /// </summary>
    [SerializeField] private int[,] matrix2;
    /// <summary>
    /// Список всех точек в графе.
    /// </summary>
    [SerializeField] private PointController[] mapsObj;
    #endregion

    #region Math-Calculate function
    /// <summary>
    /// Заполняет matrix2 и matrix
    /// Алгоритм -
    /// </summary>
    private void generateMatrix()
    {
        for (int k = 0; k < countPoints; ++k)
        {
            for (int j = 0; j < countPoints; ++j)
            {
                for (int i = 0; i < countPoints; ++i)
                {
                    if (matrix[i, j] > matrix[i, k] + matrix[k, j])
                    {
                        matrix[i, j] = matrix[i, k] + matrix[k, j];
                        matrix2[i, j] = matrix2[i, k] >= 0 ? matrix2[i, k] : k;
                    }
                }
            }
        }
    }
    /// <summary>
    /// Заполняем матрицу путей
    /// </summary>
    void BFS ()
    {
        Stack<PointController> que = new Stack<PointController>();
        //Карта вершин
        bool[] map = new bool[countPoints];
        //Пишем , что пока мы не были не в одной вершине
        for(int i = 0; i < countPoints; i++)
        {
            map[i] = false;
        }
       
        que.Push(startPoint);
        while (que.Count > 0)
        {
            PointController top = que.Pop();
            //Если в этой вершине мы еще не были
            if (!map[top.num])
            {
                //Ставим что мы в ней были
                map[top.num] = true;
                mapsObj[top.num] = top;
                //Заполняем всех  соседей и заполняем матрицу путей
                foreach (Points p in top.Neightboring)
                {
                    que.Push(p.point);
                    matrix[top.num, p.point.num] = p.dist;
                }
            }
        }
    }


    #endregion

    #region Public methods
    /// <summary>
    /// Метод строит маршрут из точки старт в точку энд
    /// </summary>
    /// <param name="numStart">Номер стартовой точки</param>
    /// <param name="numEnd">Номер конечной точки</param>
    /// <returns></returns>
    public bool searchRoad(int numStart, int numEnd)
    {
        road.Clear();
        road.Add(numStart);
        int cur = numStart;
        while (matrix2[cur, numEnd] >= 0)
        {
            cur = matrix2[cur, numEnd];
            road.Add(cur);
        }
        if (matrix2[cur, numEnd] == -2)
        {
            road.Add(numEnd);
        }
        return true;
    }
    /// <summary>
    /// Строит маршрут из стартовй точки в конечную через промежуточную
    /// </summary>
    /// <param name="numStart">Номер стартовой точки</param>
    /// <param name="numEnd">Номер конечной точки</param>
    /// <param name="numAcross">Номер промежуточной точки</param>
    /// <returns></returns>
    public bool searchRoadKey(int numStart, int numEnd, int numAcross)
    {
        road.Clear();
        List<int> road2 = new List<int>();
        searchRoad(numStart, numAcross);
        foreach (int i in road)
        {
            road2.Add(i);
        }
        searchRoad(numAcross, numEnd);
        foreach (int i in road)
        {
            road2.Add(i);
        }
        road = road2;
        int temp = 0;
        foreach (int i in road)
        {
            if (i == numStart)
            {
                temp++;
            }
        }
        if (temp > 1)
        {
            return false;
        }
        return true;
    }
    /// <summary>
    /// Приводит граф к первоначальной закраске
    /// </summary>
    /// <param name="main">Основной цвет вершины</param>
    /// <param name="start">Цвет стартовой/текущей точки</param>
    public void clearRoad(Color main, Color start)
    {
        for (int i = 0; i < countPoints; i++)
        {
            mapsObj[i].setColor(main);
        }
        mapsObj[startPoint.num].setColor(start);
    }
    /// <summary>
    /// Красит построенный маршрут
    /// </summary>
    /// <param name="clr">Цвет закраски маршрута</param>
    public void drawRoad(Color clr)
    {
        foreach (int p in road)
        {
            print(p);
            mapsObj[p].setColor(clr);
        }
    }
    /// <summary>
    /// Возвращает дичтанцию из startPoint в город под номером end
    /// </summary>
    /// <param name="end">Номер города , до которого считается дистанция маршрута</param>
    /// <returns></returns>
    public int GetDistance(int end)
    {
        return matrix[startPoint.num, end];
    }
    /// <summary>
    /// Возвращает следующий город на пути
    /// </summary>
    /// <returns> Следующий город из StartPoint по маршруту road</returns>
    public PointController GetNextCity()
    {
        return mapsObj[road[1]];
    }

    /// <summary>
    /// Заглушка для кнопки старт. С помощью этой ф-ции предпологается 
    /// переходить на новую сцену
    /// </summary>
    public void StartLevel()
    {
        //startPoint.name Имя стартового города
        //mapsObj[road[1]].name Имя следующего города

    }
    /// <summary>
    /// Инициализирует компоненты PathController и
    /// заполняет их нужными значениями
    /// </summary>
    public void Init()
    {
        road = new List<int>();
        mapsObj = new PointController[countPoints];
        matrix = new int[countPoints, countPoints];
        matrix2 = new int[countPoints, countPoints];
        for (int i = 0; i < countPoints; i++)
        {
            for (int j = 0; j < countPoints; j++)
            {
                if (i == j)
                {
                    matrix[i, j] = 0;
                    matrix2[i, j] = -1;
                }
                else
                {
                    matrix[i, j] = 1000000000;
                    matrix2[i, j] = -2;
                }
            }
        }
        BFS();
        generateMatrix();
    }
    #endregion
}
