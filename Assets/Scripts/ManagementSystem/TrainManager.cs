using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;

public class TrainManager : MonoBehaviour
{

    [SerializeField] List<GameObject> cars =  new List<GameObject>();
    public void Init(SceneData sceneData)
    {
        foreach(var car in cars)
        {
            sceneData.TrainCars.Add(car);
        }
    }
    public void UpdateTrain(SceneData sceneData)
    {

        foreach(var car in sceneData.TrainCars)
        {
            car.GetComponent<ACarriage>().UpdateCarriage();
        }
    }
}
