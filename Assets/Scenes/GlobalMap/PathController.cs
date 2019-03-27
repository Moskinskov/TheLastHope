using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PathController : MonoBehaviour
{
    [SerializeField] public PointController startPoint;// Текущая и стартовая позиция
    [SerializeField] public PointController nextKeyPoint; //Следующая ключевая цель
    [SerializeField] public int countPoints; // Число вершин
    [SerializeField] private int[,] matrix; //Матрица дистанций от i до j
    [SerializeField] private int[,] matrix2;
    [SerializeField] private PointController[] mapsObj;
    [SerializeField] public List<int> road;

	private Color _initColor;

    #region Math-Calculate function
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
    //Заполняет матрицу путей
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

    public bool searchRoad(int numStart, int numEnd)
    {
        road.Clear();
        road.Add(numStart);
        int cur = numStart;
        while(matrix2[cur, numEnd] >= 0)
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
    #endregion

    //Очишает карту от закраски маршрута. Main - цвет основной для точки
    // start - цвет стартовой точки
    public void clearRoad(Color main, Color start)
    {
		_initColor = new Color(17, 86, 33);
		for (int i = 1; i < road.Count; i++)
        {
<<<<<<< HEAD
            mapsObj[road[i]].setColor(_initColor);
=======
            mapsObj[road[i]].setColor(main);
>>>>>>> MakeMap
        }
        mapsObj[startPoint.num].setColor(start);
    }
    //Красит маршрут в цвет clr
    public void drawRoad(Color clr)
    {
        foreach (int p in road)
        {
            mapsObj[p].setColor(clr);
        }
    }
    void PrintMatrix(int[,] mat)
    {
        for (int i = 0; i < countPoints; i++)
        {
            string str = "";
            for (int j = 0; j < countPoints; j++)
            {
                str += mat[i, j] + " ";
            }
            print(str);
        }
    }
    //Возвращает дичтанцию из startPoint в город под номером end
    public int GetDistance(int end)
    {
        return matrix[startPoint.num, end];
    }
    //Возвращает следующий город на пути
    public PointController GetNextCity()
    {
        return mapsObj[road[1]];
    }
    public void StartLevel()
    {
        //startPoint.name Имя стартового города
        //mapsObj[road[1]].name Имя следующего города

    }
<<<<<<< HEAD
    void Start()
=======
    public void Init()
>>>>>>> MakeMap
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
<<<<<<< HEAD
        startPoint.setColor(Color.green);
    }

=======
    }
>>>>>>> MakeMap
}
