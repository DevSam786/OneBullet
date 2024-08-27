using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public Transform shotPoint;
    [Header("Bullets")]
    public GameObject bulletPrefab;
    public float maxBulletForce;
    public string enemyTag = "Enemy";
    [HideInInspector]public float bullet_Force;
    [Header("Knockback Settings")]
    public float maxHoldTime = 2f;
    public float maxKnockbackForce = 10f;
    private float holdTime = 0f;
    private bool isHolding = false;
    public Rigidbody playerRb;
    Rigidbody bullet_rb;
    //Bullets Mechanics
    public bool isBulletAvailable = true;
    GameObject bullet;
    //Time Things to Activate Gravity
    bool bulletsFlying;
    float timeAfterBulletShot;
    public float maxTimeAfterBulletShot;
    // Start is called before the first frame update
    void Start()
    {
        bullet = GameObject.FindGameObjectWithTag("Bullet");
        isBulletAvailable = true;
        bulletsFlying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(bulletsFlying == true)
        {
            timeAfterBulletShot += Time.deltaTime;
            if(timeAfterBulletShot > maxTimeAfterBulletShot )
            {
                bullet_rb.useGravity = true;
                timeAfterBulletShot = 0f;
                bulletsFlying=false;
            }
        }
       
        if (Input.GetMouseButtonDown(0))
        {
            isHolding = true;
            holdTime = 0f;
        }

        // Track how long the button is held
        if (isHolding)
        {
            holdTime += Time.deltaTime;
        }

        // Apply knockback when the button is released
        if (Input.GetMouseButtonUp(0) && isHolding && isBulletAvailable)
        {
            isHolding = false;
            CalculateAndApplyKnockback();
            
        }
    }
    void CalculateAndApplyKnockback()
    {
        float clampedHoldTime = Mathf.Clamp(holdTime, 0, maxHoldTime);

        //Force Calculation
        float knockbackForce = (clampedHoldTime / maxHoldTime) * maxKnockbackForce;
        bullet_Force = (clampedHoldTime/ maxHoldTime) * maxBulletForce;

        //Bullets Instantiate
        GameObject bullets = Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation);
        bullet_rb = bullets.GetComponent<Rigidbody>();
        bullet_rb.AddForce(shotPoint.forward * bullet_Force, ForceMode.Impulse);
        
        isBulletAvailable = false;
        bulletsFlying = true;

        // Reverse the direction the player is facing
        Vector3 knockbackDirection = -transform.forward;
        Debug.Log("Bullet Force is: " + bullet_Force + " && " + " Player BackWard Force is: " + knockbackForce);
        ApplyKnockback(knockbackDirection, knockbackForce);
    }

    void ApplyKnockback(Vector3 direction, float force)
    {
        playerRb.AddForce(direction * force, ForceMode.Impulse);
    }
}
