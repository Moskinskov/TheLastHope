using UnityEngine;
using UnityEngine.EventSystems;

namespace TheLastHope.Hangar
{
    public class Slot : MonoBehaviour, IDropHandler
    {
        public bool isInventory = true;
        public bool isVacant = true;
        public int number;
        public GameObject item
        {
            get
            {
                if (transform.childCount > 0)
                    return transform.GetChild(0).gameObject;
                return null;
            }
            set
            {
                item = value;
                item.transform.SetParent(transform);
            }
        }
        public void OnDrop(PointerEventData eventData)
        {
            if (!item)
            {
                if (!Item.itemBeingDragged.transform.parent.GetComponent<Slot>().isInventory)
                {
                    HangarData.instance.currentCarrage.RemoveHardware(Item.itemBeingDragged.transform.parent.GetComponent<Slot>().number);
                    HangarData.instance.currentCarrage.AddNewHardware(Item.itemBeingDragged.GetComponent<Item>().hw, number);
                }
                if (!isInventory && Item.itemBeingDragged.transform.parent.GetComponent<Slot>().isInventory)
                {
                    HangarData.instance.positionController.itemsOnCarrage.Add(item);
                    HangarData.instance.currentCarrage.AddNewHardware(Item.itemBeingDragged.GetComponent<Item>().hw, number);
                }
                Item.itemBeingDragged.transform.parent.GetComponent<Slot>().isVacant = true;
                Item.itemBeingDragged.transform.SetParent(transform);

                isVacant = false;
            }
        }
    }
}
