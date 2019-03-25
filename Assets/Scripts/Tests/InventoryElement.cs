using UnityEngine;

namespace MosMos
{
    public class InventoryElement : MonoBehaviour
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private GameObject _realGO;

        private CanvasInventoryElement _canvasInventoryElement;

        public GameObject PropGO
        {
            get { return _realGO; }
            set { _realGO = value; }
        }

        public Sprite Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        public CanvasInventoryElement PropCanvasElement
        {
            get { return _canvasInventoryElement; }
            set { _canvasInventoryElement = value; }
        }
    }
}