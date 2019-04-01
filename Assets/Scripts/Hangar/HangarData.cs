using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Hangar
{
    public class HangarData : MonoBehaviour
    {
        public PositionController positionController;
        public Inventory inventiry;
        [SerializeField] private Container[] train;
        [HideInInspector] public Container currentCarriage;

        public static HangarData instance;

        void Awake()
        {
            currentCarriage = train[0];
        }
        public HangarData()
        {
            instance = this;
        }

        public void SetInactive()
        {
            inventiry.gameObject.SetActive(false);
            positionController.gameObject.SetActive(false);
        }
    }
}
