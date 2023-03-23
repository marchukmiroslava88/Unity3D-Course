using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KnightInBorderlands.Scripts
{
    public class Dialogue : MonoBehaviour
    {
        public static Dialogue Instance;
        [SerializeField] private TextMeshProUGUI _textComponent;
        public string[] _lines;
        public float _textSpeed;
        
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
            _textComponent.text = string.Empty;
            gameObject.SetActive(false);
        }

        public void StartDialogue()
        {
            gameObject.SetActive(true);
            StartCoroutine(TypeLine());
        }

        public void HideDialogue()
        {
            gameObject.SetActive(false);
            _textComponent.text = "";
        }
        
        private IEnumerator TypeLine()
        {
            foreach (string text in _lines)
            {
                _textComponent.text += text;
                yield return new WaitForSeconds(_textSpeed);
            }
        }
    }
}