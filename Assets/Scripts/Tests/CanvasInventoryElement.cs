using UnityEngine;
using UnityEngine.EventSystems;

namespace MosMos
{
    public class CanvasInventoryElement : MonoBehaviour, IDragHandler
    {
        private InventoryElement _inventoryElement;
        private Vector3 _mousePosition;

        public InventoryElement Element
        {
            get { return _inventoryElement; }
            set { _inventoryElement = value; }
        }


        public void OnDrag(PointerEventData eventData)
        {
            transform.SetParent(default);
            _mousePosition = Input.mousePosition;

            var ray = Camera.main.ScreenPointToRay(_mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log(hit.transform.name);

                Element.PropGO.SetActive(true);
                Element.PropGO.transform.localPosition = hit.point;
                Element.PropGO.transform.position.Set(Element.PropGO.transform.localPosition.x, 0.5f, Element.PropGO.transform.localPosition.z);
            }


        }
    }
}