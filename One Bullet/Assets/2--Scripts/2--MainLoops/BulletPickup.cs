using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour
{
    public string enemyTag = "Enemy";
    WeaponSystem weaponSystem;

    private void Start()
    {
          weaponSystem = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponSystem>();    
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            WeaponSystem weapon = collision.gameObject.GetComponentInChildren<WeaponSystem>();
            if (weapon != null)
            {
                weapon.isBulletAvailable = true;
            }
        }else if (collision.gameObject.CompareTag(enemyTag))
        {
            if(weaponSystem != null)
            {
                if(weaponSystem.bullet_Force >= 2)
                {
                    weaponSystem.bullet_Force -= 2f;
                }
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(enemyTag))
        {
            if (weaponSystem != null)
            {
                if (weaponSystem.bullet_Force >= 2)
                {
                    Destroy(collision.gameObject,0.4f);
                }
            }
        }
    }
}
