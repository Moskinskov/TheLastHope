using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MosMos
{
    public class DragPanel : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Transform _contentPanel;
        [SerializeField] private CanvasInventoryElement _exampleElement;

        [SerializeField] private Transform _targetTransform;
        
        private void Awake()
        {
            if (_inventory == null)
                return;

            foreach (var element in _inventory.PropInventory)
            {
                ApplyContent(element);
            }
        }

        public void ApplyContent(InventoryElement element)
        {
            if (_inventory == null)
                return;
            if (!_inventory.PropInventory.Contains(element))
                _inventory.AddToInventary(element);

            element.PropCanvasElement = Instantiate(_exampleElement, _contentPanel);
            element.PropCanvasElement.Element = element;
            element.PropCanvasElement.GetComponent<Image>().sprite = element.Icon;
        }

        public void RemoveContent(InventoryElement element)
        {
            if (_inventory == null)
                return;

            _inventory.RemoveFromInventory(element);
            Destroy(element.PropCanvasElement);
        }
    }
}


