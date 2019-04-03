/// Limerence Games
/// The Last Hope
/// Curator: Danny Kotov
/// to be commented

using UnityEngine;
using UnityEngine.EventSystems;

namespace TheLastHope.Hangar
{
    public class Slot : MonoBehaviour, IDropHandler
    {
        public bool isInventory = true;             //Slots of inventory or not
        public bool isVacant = true;    
        public int number;                          //Number of a slot
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
                    HangarData.instance.positionController.itemsOnCarriage.Add(item);
                    HangarData.instance.currentCarriage.AddNewHardware(Item.itemBeingDragged.GetComponent<Item>().hw, number);
                }


                Item.itemBeingDragged.transform.parent.GetComponent<Slot>().isVacant = true;
                Item.itemBeingDragged.transform.SetParent(transform);

                isVacant = false;
            }
        }
    }
}
