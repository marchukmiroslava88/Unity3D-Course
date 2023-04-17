using System.Collections.Generic;
using UnityEngine;

namespace Tower
{
    [CreateAssetMenu(fileName = "SpawnData", menuName = "TowerScriptableObject", order = 1)]
    public class TowerScriptableObject : ScriptableObject
    {
        public List<TowerModifyLevel> TowerModifyLevels = new();
    }
}