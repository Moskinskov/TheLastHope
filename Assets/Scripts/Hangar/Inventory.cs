using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TheLastHope.Hangar
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private GameObject[] objPool;    //Temporary array
        private List<GameObject> inventory;
        void Filling()
        {
            inventory = new List<GameObject>();
            for (int i = 0; i < objPool.Length; i++)
            {
                inventory.Add(Instantiate(objPool[i], transform.GetChild(i).transform));
                inventory[i].GetComponent<Item>().controller = HangarData.instance.positionController;
            }
        } 

        void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).childCount > 0)
                {
                    transform.GetComponentsInChildren<Slot>()[i].isVacant = false;
                }
            }
            Filling();
        }
    }
}
