using UnityEngine;
using UnityEngine.EventSystems;

namespace TheLastHope.Hangar
{
    public class Slot : MonoBehaviour, IDropHandler
    {
        /// <summary>
        /// Slots of inventory or not
        /// </summary>
        public bool isInventory = true;
        /// <summary>
        /// Slot is vacant or not
        /// </summary>
        public bool isVacant = true;
        /// <summary>
        /// Number of a slot
        /// </summary>
        public int number;
        /// <summary>
        /// Item in this slot
        /// </summary>
        public GameObject item
        {
            get
            {
                if (transform.childCount > 0)
                    return transform.GetChild(0).gameObject;
                return null;
            }
        }
        public void OnDrop(PointerEventData eventData)
        {
            if (!item)
            {
                switch (HangarData.instance.currentWindow)
                {
                    case CurrentWindow.Carriage:
                        {
                            if (isInventory && !Item.itemBeingDragged.transform.parent.GetComponent<Slot>().isInventory)
                            {
                                HangarData.instance.currentCarriage.RemoveHardware(Item.itemBeingDragged.transform.parent.GetComponent<Slot>().number);
                            }
                            else if (!Item.itemBeingDragged.transform.parent.GetComponent<Slot>().isInventory)
                            {
                                HangarData.instance.currentCarriage.RemoveHardware(Item.itemBeingDragged.transform.parent.GetComponent<Slot>().number);
                                HangarData.instance.currentCarriage.AddNewHardware(Item.itemBeingDragged.GetComponent<Item>().hw, number);
                                Item.itemBeingDragged.transform.SetParent(transform);
                            }
                            if (!isInventory && Item.itemBeingDragged.transform.parent.GetComponent<Slot>().isInventory)
                            {
                                HangarData.instance.positionController.itemsOnCarriage.Add(Item.itemBeingDragged);
                                HangarData.instance.currentCarriage.AddNewHardware(Item.itemBeingDragged.GetComponent<Item>().hw, number);
                            }

                            Item.itemBeingDragged.transform.parent.GetComponent<Slot>().isVacant = true;
                            Item.itemBeingDragged.transform.SetParent(transform);
                            break;
                        }
                    case CurrentWindow.Shop:
                        {
                            if (!Item.itemBeingDragged.transform.parent.GetComponent<Slot>().isInventory)
                            {
                                if (isInventory)
                                {
                                    HangarData.instance.player.Credit -= Item.itemBeingDragged.GetComponent<Item>().price;
                                    //HangarData.instance.Credit -= Item.itemBeingDragged.GetComponent<Item>().price;
                                    HangarData.instance.shop.CreditUpdate();
                                }

                                Item.itemBeingDragged.transform.parent.GetComponent<Slot>().isVacant = true;
                                Item.itemBeingDragged.transform.SetParent(transform);
                            }
                            else
                            {
                                if (!isInventory)
                                {
                                    HangarData.instance.player.Credit += Item.itemBeingDragged.GetComponent<Item>().price;
                                    //HangarData.instance.Credit += Item.itemBeingDragged.GetComponent<Item>().price;
                                    HangarData.instance.shop.CreditUpdate();
                                }
                                Item.itemBeingDragged.transform.parent.GetComponent<Slot>().isVacant = true;
                                Item.itemBeingDragged.transform.SetParent(transform);
                            }
                            break;
                        }
                }

                isVacant = false;
            }
        }
    }
}
