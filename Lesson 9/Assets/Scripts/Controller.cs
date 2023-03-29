using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, _layer))
            {
                raycastHit.rigidbody.isKinematic = false;
            }
        }
    }
}
