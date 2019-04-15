/// Limerence Games
/// The Last Hope
/// Curator: Danny Kotov
/// to be commented

using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management;

namespace TheLastHope.Hangar
{
    /// <summary>
    /// Data of the hangar
    /// </summary>
    public class HangarData : MonoBehaviour
    {
        #region Serialized objects - will be changed
        /// <summary>
        /// (serialazed)
        /// </summary>
        public PositionController positionController;
        /// <summary>
        /// (serialazed)
        /// </summary>
        public Inventory inventory;                             
        /// <summary>
        /// (serialazed)
        /// </summary>
        public Shop shop;
        /// <summary>
        /// (serialazed)
        /// </summary>
        [SerializeField]private HangarUIController uiController;
        /// <summary>
        /// Description of item
        /// </summary>
        public GameObject description;
        #endregion

        public Container[] train;

        #region Public variables
        /// <summary>
        /// Carriage in editing
        /// </summary>
        [HideInInspector] public Container currentCarriage;
        /// <summary>
        /// Current window in UI
        /// </summary>
        public CurrentWindow currentWindow;
        /// <summary>
        /// Number of current carriage of Train[]
        /// </summary>
        public int numOfCar=0;

        //public int Credit = 100;
        public Management.Player player;
        public static HangarData instance;
        #endregion
        void Awake()
        {
            Init();
        }
        public HangarData()
        {

        }

        //public void Init(SceneData sceneData)
        public void Init()
        {
            currentWindow = CurrentWindow.Carriage;
            player = new Management.Player(100);
            instance = this;
            //print($" CAR {sceneData.TrainCars[2].name}");
            //currentCarriage = sceneData.TrainCars[2].GetComponentInChildren<Container>();
            currentCarriage = train[numOfCar];

            positionController.Init();
            inventory.Init();
            uiController.Init();
            shop.Init();
            description.SetActive(false);
        }
        
        public bool GetNextCarriage()
        {
            if (numOfCar + 1 < train.Length)
            {
                numOfCar++;
                currentCarriage = train[numOfCar];
                return true;
            }
            return false;
        }

        public bool GetPreviousCarriage()
        {
            if (numOfCar - 1 >= 0)
            {
                numOfCar--;
                currentCarriage = train[numOfCar];
                return true;
            }
            return false;
        }

        public void SetInactive()
        {
            inventory.transform.parent.gameObject.SetActive(false);
            //positionController.gameObject.SetActive(false);
        }
    }

    public enum CurrentWindow
    {
        Carriage,
        Shop
    }
}
