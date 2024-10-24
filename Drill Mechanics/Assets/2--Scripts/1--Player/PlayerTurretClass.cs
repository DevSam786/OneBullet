using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurretClass : MonoBehaviour
{
    [SerializeField] GameObject[] buildingTurret;
    [SerializeField] GameObject closestTarget;
    [SerializeField] bool turretInRange;
    [SerializeField] string turretTag;
    [SerializeField] float closestDistance;  // Start with a very large number

    // Start is called before the first frame update
    void Start()
    {
        buildingTurret = GameObject.FindGameObjectsWithTag(turretTag);
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        DebugDrawSphere();
        ActivateClosestTarget();
    }
    void FindClosestTarget()
    {
        
        closestTarget = null;  // Reset closest target

        Vector3 playerPosition = transform.position;

        // Loop through each target to find the closest one
        foreach (GameObject target in buildingTurret)
        {
            float distanceToTarget = Vector3.Distance(playerPosition, target.transform.position);

            if (distanceToTarget < closestDistance)
            {
                closestTarget = target;
            }
        }
    }
   
    void DebugDrawSphere()
    {
        if(closestTarget == null)
        {
            return;
        }
        Debug.DrawLine(transform.position, closestTarget.transform.position, Color.green); // Draws a line between the object and the player
       
    }
    void ActivateClosestTarget()
    {
        // You can implement any activation logic here, for example:
        if (Input.GetKeyDown(KeyCode.E) && closestTarget != null)
        {
            closestTarget.GetComponent<BuildingTurrets>().TurretActive();
        }
        /*
        // Optionally, deactivate all other targets
        foreach (GameObject target in buildingTurret)
        {
            if (target != closestTarget)
            {
                target.SetActive(false);  // Deactivate other targets, if needed
            }
        }*/
    }
}
