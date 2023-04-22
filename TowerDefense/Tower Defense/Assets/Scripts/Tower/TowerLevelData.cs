using System;
using UnityEngine;

namespace Tower
{
   [Serializable]
   public class TowerLevelData
   {
      public float FireRate = 1f;
      public GameObject BulletPrefab;
      public int Bullets = 1;
      public float TimeBetweenShots = 0.25f;
      public GameObject TowerHead;
   }
}