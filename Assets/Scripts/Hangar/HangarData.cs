/// Limerence Games
/// The Last Hope
/// Curator: Danny Kotov
/// to be commented

using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;

namespace TheLastHope.Hangar
{
    public class HangarData : MonoBehaviour
    {
        public PositionController positionController;
        public Inventory inventory;
        [HideInInspector] public Container currentCarriage;

        public static HangarData instance;

        void Awake()
        {
            //currentCarriage = train[0];
        }
        public HangarData()
        {

        }

        public void Init(SceneData sceneData)
        {
            instance = this;
            print($" CAR {sceneData.TrainCars[2].name}");
            currentCarriage = sceneData.TrainCars[2].GetComponentInChildren<Container>();
            positionController.Init();
            inventory.Init();
        }

        public void SetInactive()
        {
            inventory.gameObject.SetActive(false);
            positionController.gameObject.SetActive(false);
        }
    }
}
