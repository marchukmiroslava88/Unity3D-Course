using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }

        Instance = this;
    }
    
    public GameObject GetTowerToBuild { get; private set; }
    
    public void SetTowerToBuild(GameObject tower) => GetTowerToBuild = tower;
}
