using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PathController : MonoBehaviour
{
    [SerializeField] public PointController startPoint;// Текущая и стартовая позиция
    [SerializeField] public int countPoints; // Число вершин
    [SerializeField] private int[,] matrix; //Матрица дистанций от i до j
    [SerializeField] private int[,] matrix2;
    [SerializeField] private PointController[] mapsObj;
    [SerializeField] List<int> road;

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

    public void searchRoad(int numEnd)
    {
        road.Clear();
        road.Add(startPoint.num);
        int cur = startPoint.num;
        while(matrix2[cur, numEnd] >= 0)
        {
            cur = matrix2[cur, numEnd];
            road.Add(cur);
        }
        if (matrix2[cur, numEnd] == -2)
        {
            road.Add(numEnd);
        } 
    }
    #endregion


    public void clearRoad()
    {
        for (int i = 1; i < road.Count; i++)
        {
            mapsObj[road[i]].setColor(Color.white);
        }
        mapsObj[0].setColor(Color.green);
    }

    public void drawRoad(Color clr, int numEnd)
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
    public int GetDistance(int end)
    {
        return matrix[startPoint.num, end];
    }
    public PointController GetNextCity()
    {
        return mapsObj[road[1]];
    }
    public void StartLevel()
    {
        //startPoint.name Имя стартового города
        //mapsObj[road[0]].name Имя следующего города

    }
    // Start is called before the first frame update
    void Start()
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
        startPoint.setColor(Color.green);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    
}
