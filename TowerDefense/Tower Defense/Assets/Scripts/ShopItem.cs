using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private Toggle _toggle;
    
    private void Start()
    {
        _toggle.GetComponentInChildren<Text>().text = _price.ToString();
    }
}