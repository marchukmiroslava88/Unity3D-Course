using System.Collections;
using TMPro;
using UnityEngine;

namespace KnightInBorderlands.Scripts
{
    public class TooltipManager : MonoBehaviour
    {
        public static TooltipManager Instance;
        [SerializeField] private TextMeshProUGUI _textComponent;
    
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            StartCoroutine(HideDescription());
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
        
        private IEnumerator HideDescription()
        {
            yield return new WaitForSeconds(3f);
            gameObject.SetActive(false);
        }
    }
}
