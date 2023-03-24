using KnightInBorderlands.Scripts.Components;
using UnityEngine;

namespace KnightInBorderlands.Scripts.LevelManager
{
    public class CheckPoint : MonoBehaviour
    {
        public static CheckPoint Instance;
        [SerializeField] private Vector2 _checkPointPosition;
        [SerializeField] private GameObject _player;
        
        private void Awake() 
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject); 
            }
        }

        private void Start()
        {
            SaveSystem.SavePlayer();
        }

        public void Check(Vector2 position)
        {
            _checkPointPosition = position;
            HealthComponent.Instance.RestoreHealth();
            SaveSystem.SavePlayer();
        }
        
        public void SpawnHero()
        {
            var data = SaveSystem.LoadPlayer();
            _player.transform.position = JsonUtility.FromJson<Vector2>(data.Position);
        }

        public Vector2 CheckPointPosition => _checkPointPosition;
    }
}
