using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Guns/Types of Gun")]
public class GunSO : ScriptableObject
{
    [Header("Guns Variable")]
    public GameObject bulletPrefab;
    //bullet force
    public float shootForce;
    public float upwardForce;
    public float bulletLifeTime;
  
    public float timeBetweenShooting;
    public float spread;
    public float reloadTime;
    public float timeBetweenShots;
    public int magazingSize;
    public int bulletsPerTap;
    public bool allowButtonHold;
    
}
