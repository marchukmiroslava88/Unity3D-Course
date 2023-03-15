using UnityEngine;

namespace KnightInBorderlands.Scripts
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] private GameObject _interactWithObject;
        public string message;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == _interactWithObject.name)
            {
                TooltipManager._instance.SetAndShowTooltip(message);  
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.name == _interactWithObject.name)
            {
                TooltipManager._instance.HideToolTip();
            }
        }
    }
}
