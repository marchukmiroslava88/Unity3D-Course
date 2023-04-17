using UnityEngine;
using UnityEngine.UI;

public class ShopItems : MonoBehaviour
{
    private ToggleGroup shopItemsToggleGroup;
    public static ShopItems Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        shopItemsToggleGroup = GetComponent<ToggleGroup>();
    }

    public void SetAllShopItemsOff()
    {
        shopItemsToggleGroup.SetAllTogglesOff();
    }
}