using UnityEngine;

namespace KnightInBorderlands.Scripts.LevelManager
{
    public class CheckPoint : MonoBehaviour
    {
        public static CheckPoint _instance;
        public Vector2 _checkPointPosition;
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject); 
            }
        }

        public void Check(Vector2 position)
        {
            _checkPointPosition = position;
            Debug.Log($"New _checkPointPosition {_checkPointPosition}");
        }
        
        public void SpawnHero()
        {
            
        }
    }
}
