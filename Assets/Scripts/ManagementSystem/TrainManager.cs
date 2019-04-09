/// Limerence Games
/// The Last Hope
/// Curator: Sergey Aydarov
/// to be commented

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;

public class TrainManager : MonoBehaviour
{
    public void Init(SceneData sceneData)
    {
        ACarriage[] carriages = GameObject.FindObjectsOfType<ACarriage>();
        foreach(var car in carriages)
        {
            sceneData.TrainCars.Add(car.gameObject);
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
