using UnityEngine;

public class Node : MonoBehaviour
{
   public Color hoverColor;
   private Renderer rend;
   private Color startColor;
   private GameObject turret;
   private BuildManager buildManager;
   
   private void Start()
   {
      rend = GetComponent<Renderer>();
      startColor = rend.material.color;
      buildManager = BuildManager.Instance;
   }

   void OnMouseEnter()
   { 
      if (buildManager.GetTowerToBuild == null) return;
      rend.material.color = hoverColor;
   }
   
   void OnMouseDown()
   {
      if (buildManager.GetTowerToBuild == null) return;
      
      if (turret != null) return;

      var transformPos = transform.position;
      turret = Instantiate(
         buildManager.GetTowerToBuild,
         new Vector3(transformPos.x, 0.5f, transformPos.z),
         Quaternion.Euler(0,90,0)
         );

      ShopItems.Instance.SetAllShopItemsOff();
   }
   
   void OnMouseExit()
   {
      rend.material.color = startColor;
   }
}
