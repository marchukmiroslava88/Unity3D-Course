using UnityEngine;
namespace KnightInBorderlands.Scripts
{
    public enum CollectableType
    {
        Coins,
        Keys,
        HealthPoison,
    }
    public class Collectables : MonoBehaviour
    {
        [SerializeField] private CollectableType _type;
        public CollectableType Type => _type;
    }
}