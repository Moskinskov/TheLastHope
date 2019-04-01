using System.Collections.Generic;
using TheLastHope.Management.AbstractLayer;
using UnityEngine;
using UnityEngine.UI;

namespace TheLastHope.Hangar
{
    public class PositionController : MonoBehaviour
    {
        [HideInInspector]public List<GameObject> itemsOnCarrage;
        [HideInInspector]public List<GameObject> squareSlots;
        void Start()
        {
            itemsOnCarrage = new List<GameObject>();
            squareSlots = new List<GameObject>();

            InitSlots();
        }
        void Update()
        {
        }

        /// <summary>
        /// Creating UI slots
        /// </summary>
        private void InitSlots()
        {
            for (int i = 0; i < HangarData.instance.currentCarrage.squareTypeCount; i++)
            {
                GameObject slot = new GameObject() { name = $"Slot_{i}" };
                slot.AddComponent<RectTransform>();
                slot.AddComponent<Slot>();
                slot.GetComponent<Slot>().isInventory = false;
                slot.GetComponent<Slot>().number = i;
                slot.AddComponent<GridLayoutGroup>();
                slot.GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
                slot.GetComponent<GridLayoutGroup>().cellSize = new Vector2(60f, 60f);              //Item's size
                slot.AddComponent<Image>();
                slot.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);                        //white color with 50% alpha channel
                slot.GetComponent<Image>().enabled = false;
                slot.transform.parent = transform;
                squareSlots.Add(slot);
            }
        }
        
        /// <summary>
        /// Show vacant slots when item dragging is begin
        /// </summary>
        public void BeginItemDragging()
        {
            for (int i = 0; i < squareSlots.Count; i++)
            {
                squareSlots[i].GetComponent<Image>().enabled = true;
            }
        }
        
        /// <summary>
        /// Hide unfilled slots when item dragging is end
        /// </summary>
        public void EndItemDragging()
        {
            for (int i = 0; i < squareSlots.Count; i++)
            {
                if (squareSlots[i].transform.childCount == 0)
                {
                    squareSlots[i].GetComponent<Image>().enabled = false;
                }
            }
        }
    }
}
