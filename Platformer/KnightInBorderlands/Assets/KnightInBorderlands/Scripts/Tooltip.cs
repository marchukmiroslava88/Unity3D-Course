using UnityEngine;

namespace KnightInBorderlands.Scripts
{
    public class Tooltip : MonoBehaviour
    {
        public string message;

        private void OnTriggerEnter2D(Collider2D other)
        {
            TooltipManager._instance.SetAndShowTooltip(message);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            TooltipManager._instance.HideToolTip();
        }
    }
}
