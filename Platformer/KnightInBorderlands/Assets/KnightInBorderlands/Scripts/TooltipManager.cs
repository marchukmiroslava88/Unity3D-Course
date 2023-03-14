using TMPro;
using UnityEngine;

namespace KnightInBorderlands.Scripts
{
    public class TooltipManager : MonoBehaviour
    {
        public static TooltipManager _instance;
        public TextMeshProUGUI textComponent;
    
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }
    
        private void Update()
        {
        
        }

        public void SetAndShowTooltip(string message)
        {
            textComponent.text = message;
            gameObject.SetActive(true);
        }

        public void HideToolTip()
        {
            textComponent.text = string.Empty;
            gameObject.SetActive(false);
        }
    }
}
