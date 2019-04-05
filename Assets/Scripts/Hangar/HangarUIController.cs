﻿using UnityEngine;
using UnityEngine.UI;

namespace TheLastHope.Hangar
{
    /// <summary>
    /// User interface controller
    /// </summary>
    class HangarUIController : MonoBehaviour
    {
        /// <summary>
        /// Open 'carriage position' window 
        /// </summary>
        [SerializeField] private Button carriageButton;
        /// <summary>
        /// Open 'shop' window
        /// </summary>
        [SerializeField] private Button shopButton;
        /// <summary>
        /// Switch to previous carriage
        /// </summary>
        [SerializeField] private Button previousCarBtn;
        /// <summary>
        /// Switch to next carriage
        /// </summary>
        [SerializeField] private Button nextCarBtn;

        public void Init()
        {
            carriageButton.onClick.AddListener(CarriageBtnOnClick);
            carriageButton.interactable = false;

            HangarData.instance.shop.gameObject.SetActive(false);
            shopButton.onClick.AddListener(ShopBtnOnClick);

            previousCarBtn.onClick.AddListener(PreviousCarriage);
            previousCarBtn.interactable = false;
            nextCarBtn.onClick.AddListener(NextCarriage);
        }

        void CarriageBtnOnClick()
        {
            carriageButton.interactable = false;
            shopButton.interactable = true;
            HangarData.instance.currentWindow = CurrentWindow.Carriage;
            HangarData.instance.positionController.gameObject.SetActive(true);
            HangarData.instance.shop.gameObject.SetActive(false);

            previousCarBtn.gameObject.SetActive(true);
            nextCarBtn.gameObject.SetActive(true);
        }

        void ShopBtnOnClick()
        {
            shopButton.interactable = false;
            carriageButton.interactable = true;
            HangarData.instance.currentWindow = CurrentWindow.Shop;
            HangarData.instance.shop.gameObject.SetActive(true);
            HangarData.instance.positionController.gameObject.SetActive(false);

            previousCarBtn.gameObject.SetActive(false);
            nextCarBtn.gameObject.SetActive(false);
        }

        void PreviousCarriage()
        {
            if (HangarData.instance.GetPreviousCarriage())
            {
                if (HangarData.instance.numOfCar == 0)
                    previousCarBtn.interactable = false;
                HangarData.instance.positionController.UpdateSlots();
                nextCarBtn.interactable = true;
                //camera move -->
            }
        }
        void NextCarriage()
        {
            if (HangarData.instance.GetNextCarriage())
            {
                if (HangarData.instance.numOfCar == HangarData.instance.train.Length - 1)
                    nextCarBtn.interactable = false;
                HangarData.instance.positionController.UpdateSlots();
                previousCarBtn.interactable = true;
                //camera move <--
            }
        }


    }
}
