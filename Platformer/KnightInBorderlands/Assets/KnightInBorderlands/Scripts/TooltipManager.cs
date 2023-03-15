using TMPro;
using UnityEngine;

namespace KnightInBorderlands.Scripts
{
    public class TooltipManager : MonoBehaviour
    {
        public static TooltipManager _instance;
        [SerializeField] private TextMeshProUGUI _textComponent;
    
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

        public void SetAndShowTooltip(string message)
        {
            _textComponent.text = message;
            gameObject.SetActive(true);
        }

        public void HideToolTip()
        {
            _textComponent.text = string.Empty;
            gameObject.SetActive(false);
        }
    }
}
