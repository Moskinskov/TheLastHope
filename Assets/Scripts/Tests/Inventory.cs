using System;
using System.Collections.Generic;
using UnityEngine;

namespace MosMos
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<InventoryElement> _inventory;
        
        private void Awake()
        {
            if (PropInventory == null)
            {
                PropInventory = new List<InventoryElement>();
            }

            foreach (var element in PropInventory)
            {
                element.PropGO.SetActive(false);
                element.transform.SetParent(transform);
            }
        }

        public List<InventoryElement> PropInventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }

        public void AddToInventary(InventoryElement o)
        {
            if (_inventory == null)
                _inventory = new List<InventoryElement>();
            if (!PropInventory.Contains(o))
                PropInventory.Add(o);
            
            o.PropGO.SetActive(false);
        }
        public void RemoveFromInventory(InventoryElement o)
        {
            if (_inventory == null)
                return;
            if (!PropInventory.Contains(o))
                return;

            PropInventory.Remove(o);
            o.PropGO.SetActive(true);
        }
    }
}