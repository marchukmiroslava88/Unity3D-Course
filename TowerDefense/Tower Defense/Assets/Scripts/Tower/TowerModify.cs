using UnityEngine;

namespace Tower
{
    public class TowerModify: MonoBehaviour
    {
        [SerializeField] public RectTransform Button;
        [SerializeField] public RectTransform CanvasRectT;
        
        public static TowerModify Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                return;
            }

            Instance = this;
        }
    }
}