using UnityEngine;

namespace KnightInBorderlands.Scripts.Components
{
    public class ExitTriggerComponent : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer = ~0;
        [SerializeField] private EnterEvent _action;

        private void OnTriggerExit2D(Collider2D other)
        {
            if (((1 << other.gameObject.layer) & _layer) != 0)
            {
                _action?.Invoke(other.gameObject);
            }
        }
    }
}