using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public LayerMask RaycastLayers;
    public int RayDistance;
    public float FireRate = 1f;
    private float FireCountdown;

    private void Update()
    {
        var ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out var hitResult, RayDistance, RaycastLayers)) {
            // Debug.Log($"Raycast hit: {hitResult.collider.name}");
 
            if (FireCountdown <= 0f)
            {
                Shoot();
                FireCountdown = 1f / FireRate; 
            }

            FireCountdown -= Time.deltaTime;
        }
    }
 
    private void Shoot()
    {
        Debug.Log("SHOOT!");
    }
}
