using System;
using UnityEngine;
using UnityEngine.Events;

namespace KnightInBorderlands.Scripts.Components
{
    public class ExitTriggerComponent : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer = ~0;
        [SerializeField] private ExitEvent _action;

        private void OnTriggerExit2D(Collider2D other)
        {
            if (((1 << other.gameObject.layer) & _layer) != 0)
            {
                _action?.Invoke(other.gameObject);
            }
        }
    }
    
    [Serializable]
    public class ExitEvent : UnityEvent<GameObject>
    {
    }
}