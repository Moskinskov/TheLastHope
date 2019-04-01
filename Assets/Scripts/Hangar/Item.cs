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

        void Awwake()
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
                for (int i = 0; i < HangarData.instance.inventory.transform.childCount; i++)
                {
                    if (HangarData.instance.inventory.transform.GetComponentsInChildren<Slot>()[i].isVacant)
                    {
                        HangarData.instance.currentCarriage.GetComponent<Container>().RemoveHardware(transform.parent.GetComponent<Slot>().number);
                        transform.parent = HangarData.instance.inventory.transform.GetChild(i);
                        HangarData.instance.inventory.transform.GetChild(i).GetComponent<Slot>().isVacant = false;
                        controller.itemsOnCarriage.Remove(gameObject);
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