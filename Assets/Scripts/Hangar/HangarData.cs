using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Hangar
{
    public class HangarData : MonoBehaviour
    {
        public PositionController positionController;
        public Inventory inventiry;
        [SerializeField] private Container[] train;
        [HideInInspector] public Container currentCarrage;

        public static HangarData instance;

        void Awake()
        {
            currentCarrage = train[0];
        }
        public HangarData()
        {
            instance = this;
        }
    }
}
