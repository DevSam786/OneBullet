using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TurretBaseClass : MonoBehaviour
{
    #region Datamembers

    #region Editor Settings

    [Header("Transform")]
    public bool isTurretOn;
    ElectricDevice device;
    [SerializeField] string enemyTag;    
    [SerializeField] float turretRange;
    [SerializeField] Transform partToRotate;
    [SerializeField] float turnSpeed;
    BuildingTurrets turretBuild;
    Transform target;

    [Header("Aim")]
    [SerializeField] private bool aim;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool ignoreHeight;
    [SerializeField] private Transform aimedTransform;    

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;    
    [SerializeField] float startTimeBtwShooting;
    float timeBtwShooting;
    

    #endregion
    #region Private Fields

    private Camera mainCamera;

    #endregion

    #endregion


    #region Methods

    #region Unity Callbacks

    private void Start()
    {
        mainCamera = Camera.main;
        turretBuild = GetComponentInParent<BuildingTurrets>();
        timeBtwShooting = startTimeBtwShooting;
        device = GetComponent<ElectricDevice>();
    }

    private void Update()
    {
        if(device.isBatteryCapacityEnds == true) 
            return;
        UpdateTarget();
        Aim();    
        if(timeBtwShooting <= 0 && target != null)
        {
            Shoot();
            timeBtwShooting = startTimeBtwShooting;
        }
        else
        {
            timeBtwShooting-= Time.deltaTime;
        }
    }   

    #endregion
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
            if(nearestEnemy != null && shortestDistance <= turretRange) 
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target = null;
            }
        }
    }
    private void Aim()
    {
        if (aim == false || target == null)
        {
            return;
        }            
        var direction = target.position - aimedTransform.position;           
        aimedTransform.forward = direction;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation,lookRotation,turnSpeed * Time.deltaTime).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0, rotation.y, 0);
    }
    /*
    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            return (success: true, position: hitInfo.point);
        }
        else
        {
            return (success: false, position: Vector3.zero);
        }
    }
    */
    private void Shoot()
    {
        GameObject projectile = (GameObject)Instantiate(projectilePrefab, aimedTransform.position, aimedTransform.rotation);
        TurretBulletBase turretBullet = projectile.GetComponent<TurretBulletBase>();
        if(projectile != null)
        {
            turretBullet.Seek(target);
        }
    }
   

   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,turretRange);
    }

    #endregion
}
