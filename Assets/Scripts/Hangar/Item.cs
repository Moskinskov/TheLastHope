/// Limerence Games
/// The Last Hope
/// Curator: Danny Kotov
/// to be commented

using TheLastHope.Management.AbstractLayer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TheLastHope.Hangar
{
    /// <summary>
    /// Item with drag and drop system
    /// </summary>
    public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        /// <summary>
        /// The item which being dragged now (this gameObject)
        /// </summary>
        public static GameObject itemBeingDragged;
        /// <summary>
        /// Position of item before drag
        /// </summary>
        private Vector3 startPosition;
        /// <summary>
        /// Parent of game object before drag
        /// </summary>
        private Transform startParent;
        /// <summary>
        /// Price of item
        /// </summary>
        public int price;
        /// <summary>
        /// If mouse over the object value is true;
        /// </summary>
        private bool isOver;
        /// <summary>
        /// Description of item
        /// </summary>
        [SerializeField, TextArea(5,10)]private string description;
        /// <summary>
        /// Hardware which banded with item (serialized)
        /// </summary>
        public GameObject hw;         
        
        /// <summary>
        /// (serialized)
        /// </summary>
        [HideInInspector]public PositionController controller;

        public void Init()
        {
            //price = 10;
            gameObject.GetComponent<Image>().sprite = hw.GetComponent<AHardware>().sprite;
        }

        #region IBeginDragHandler implement
        public void OnBeginDrag(PointerEventData eventData)
        {
            switch (HangarData.instance.currentWindow)
            {
                case CurrentWindow.Carriage:
                    {
                        itemBeingDragged = gameObject;
                        startPosition = transform.position;
                        startParent = transform.parent;
                        GetComponent<CanvasGroup>().blocksRaycasts = false;

                        controller.BeginItemDragging(hw.GetComponent<AHardware>());
                        break;
                    }
                case CurrentWindow.Shop:
                    {
                        //if (price <= HangarData.instance.Credit)
                        
                        itemBeingDragged = gameObject;
                        startPosition = transform.position;
                        startParent = transform.parent;
                        GetComponent<CanvasGroup>().blocksRaycasts = false;
                        break;
                    }
            }
        
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

            switch (HangarData.instance.currentWindow)
            {
                case CurrentWindow.Carriage:
                    {

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
                        break;
                    }
                case CurrentWindow.Shop:
                    {
                        if (transform.parent == startParent)
                        {
                            transform.position = startPosition;
                        }
                        break;
                    }
            }
        }
        #endregion

        public void OnPointerEnter(PointerEventData eventData)
        {
            HangarData.instance.description.SetActive(true);
            
            HangarData.instance.description.transform.position = transform.position + new Vector3(0, - gameObject.GetComponent<RectTransform>().sizeDelta.y / 2, 0);
            HangarData.instance.description.GetComponentInChildren<Text>().text = description;

            if (Input.GetKey(KeyCode.Mouse0))
            {
                HangarData.instance.description.SetActive(false);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HangarData.instance.description.SetActive(false);
        }


    }
}