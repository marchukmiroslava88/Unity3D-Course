using UnityEngine;

namespace KnightInBorderlands.Scripts
{
    public class Parallax : MonoBehaviour
    {
        private float _startPosition;
        private GameObject _cam;
        [SerializeField] private float _parallaxEffect;
    
        private void Start()
        {
            _cam = GameObject.Find("CM vcam1");
            _startPosition = transform.position.x;
        }
    
        private void Update()
        {
            var distance = (_cam.transform.position.x * _parallaxEffect);
            transform.position = new Vector2(_startPosition + distance, transform.position.y);
        }
    }
}
