using TheLastHope.Management.AbstractLayer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TheLastHope.Hangar
{
    public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static GameObject itemBeingDragged;
        private Vector3 startPosition;
        private Transform startParent;

        public GameObject hw;
        [HideInInspector]public PositionController controller;

        public void Init()
        {
            gameObject.GetComponent<Image>().sprite = hw.GetComponent<AHardware>().sprite;
        }

        #region IBeginDragHandler implement
        public void OnBeginDrag(PointerEventData eventData)
        {
            itemBeingDragged = gameObject;
            startPosition = transform.position;
            startParent = transform.parent;
            GetComponent<CanvasGroup>().blocksRaycasts = false;

            controller.BeginItemDragging();
        }
        #endregion

        #region IDragHandler implement
        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }
        #endregion

        #region IEndDragHandler implement
        public void OnEndDrag(PointerEventData eventData)
        {
            itemBeingDragged = null;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            if (transform.parent == startParent && transform.parent.GetComponent<Slot>().isInventory)
            {
                transform.position = startPosition;
            }
            else if (transform.parent == startParent)
            {
                for (int i = 0; i < HangarData.instance.inventiry.transform.childCount; i++)
                {
                    if (HangarData.instance.inventiry.transform.GetComponentsInChildren<Slot>()[i].isVacant)
                    {
                        HangarData.instance.currentCarrage.GetComponent<Container>().RemoveHardware(transform.parent.GetComponent<Slot>().number);
                        transform.parent = HangarData.instance.inventiry.transform.GetChild(i);
                        HangarData.instance.inventiry.transform.GetChild(i).GetComponent<Slot>().isVacant = false;
                        controller.itemsOnCarrage.Remove(gameObject);
                        controller.EndItemDragging();
                        return;
                    }
                }
            }
            controller.EndItemDragging();
        }
        #endregion
    }
}