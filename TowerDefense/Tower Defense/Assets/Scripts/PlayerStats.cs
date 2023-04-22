using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public int Money;
    [SerializeField] private int _startMoney;
    [SerializeField] private TextMeshProUGUI _moneyText;
    
    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
    }
    private void Start()
    {
        Money = _startMoney;
        _moneyText.text = _startMoney.ToString();
    }

    public void AddMoney(int price)
    {
        Money += price;
        _moneyText.text = Money.ToString();
    }
    
    public void RemoveMoney(int price)
    {
        Money -= price;
        _moneyText.text = Money.ToString();
    }
}