using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingWeapon : MonoBehaviour
{
    [Header("Guns Variable")]
    public GameObject bulletPrefab;
    public string reloadTag;
    public AnimationClip animationClip;
    //bullet force
    [SerializeField] float shootForce, upwardForce, bulletLifeTime;
    [SerializeField] float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    [SerializeField] int magazingSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletLeft, bulletsShot;
    bool shooting, readyToShoot, reloading;
    [Header("Reference")]
    public Transform shotPoint;
    public Animator anim;
    bool allowToInvoke = true;
    public float startTimeBtwSpawn;
    float timeBtwSpawn;
    private void Awake()
    {
        bulletLeft = magazingSize;
        readyToShoot = true;
        allowToInvoke = true;
        timeBtwSpawn = startTimeBtwSpawn;
    }
    void Update()
    {
        if (readyToShoot && shooting && bulletLeft <= 0 && !reloading)
        {
            Reload();
        }
    }
    
    void Shoot()
    {
        readyToShoot = false;
        Vector3 directionWithoutSpread = shotPoint.forward;
        float x = Random.Range(spread, -spread);
        float y = Random.Range(spread, -spread);
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, 0, y);
        GameObject currentBullet = Instantiate(bulletPrefab, shotPoint.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        Destroy(currentBullet, bulletLifeTime);        
        bulletsShot++;
        bulletLeft--;
        if (allowToInvoke)
        {
            Invoke("ResetShooting", timeBetweenShooting);
            allowToInvoke = false;
        }
    }
    void ResetShooting()
    {
        readyToShoot = true;
        allowToInvoke = true;
    }
    void Reload()
    {
        reloading = true;
        anim.SetTrigger(reloadTag);
        Invoke("ReloadingFinished", reloadTime);
    }
    void ReloadingFinished()
    {
        bulletLeft = magazingSize;
        reloading = false;
    }
    public void TimerFunction()
    {
        if(timeBtwSpawn <= 0)
        {
            shooting = true;

            if (readyToShoot && shooting && !reloading && bulletLeft > 0)
            {
                Shoot();
            }                          
            Debug.Log(bulletLeft);
            timeBtwSpawn = startTimeBtwSpawn;
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
}
