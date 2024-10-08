using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    [Header("Guns Variable")]
    public GameObject bulletPrefab;
    //bullet force
    [SerializeField]float shootForce, upwardForce, bulletLifeTime;
    public bool inDrillRange;
    [SerializeField] float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    [SerializeField] int magazingSize, bulletsPerTap;
    public bool allowButtonHold;

    int bulletLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;
    [Header("Reference")]
    public Transform shotPoint;
    bool allowToInvoke = true;
    private void Awake()
    {
        bulletLeft = magazingSize;
        readyToShoot = true;
        allowToInvoke = true;
    }
    void Update()
    {
        MyInput();
    }
    void MyInput()
    {
        if (Input.GetKeyDown(KeyCode.R) && bulletLeft < magazingSize && !reloading)
        {
            Reload();
        }
        if(readyToShoot && shooting && bulletLeft <= 0 && !reloading)
        {
            Reload();
        }

        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if(readyToShoot && shooting && !reloading && bulletLeft > 0)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        readyToShoot = false;
        Vector3 directionWithoutSpread = shotPoint.forward;
        float x =  Random.Range(spread,-spread );
        float y = Random.Range(spread, -spread);
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);
        GameObject currentBullet = Instantiate(bulletPrefab, shotPoint.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce,ForceMode.Impulse);
        Destroy(currentBullet,bulletLifeTime);
        if (!inDrillRange)
        {
            bulletLeft--;
        }
        bulletsShot++;
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
        Invoke("ReloadingFinished", reloadTime);
    }
    void ReloadingFinished()
    {
        bulletLeft = magazingSize;
        reloading = false;
    }
}
