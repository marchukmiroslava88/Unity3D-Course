using Tower;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
    }
    
    public TowerType GetTowerToBuild { get; private set; }
    public void SetTowerToBuild(TowerType tower) => GetTowerToBuild = tower;
}
