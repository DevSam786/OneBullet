using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillBase : MonoBehaviour
{
    [Range(30f,100f)]
    [SerializeField] float rotationsPerMinute;
    [SerializeField] GameObject drillBlade;
    [SerializeField] float detectionRadius = 5f; 
    [HideInInspector] public Transform player;           
    private GunBase playerScript;  
    [Header("Drill Run")]
    public float maxFuelAmount;
    [SerializeField]float fuelAmount;
    [Range(0.1f, 1f)]
    [SerializeField]float fuelDraningSpeed;
    bool isDrillOn;
    bool playerInRange = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = player.GetComponentInChildren<GunBase>();
        fuelAmount = maxFuelAmount;
        
        isDrillOn = true;        

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        DebugDrawSphere();
        CheckPlayerInRange();
        if (isDrillOn)
        {
            FuelWithRotationSpeed();

            DrillConsumingFuel(fuelDraningSpeed * Time.fixedDeltaTime);
            drillBlade.transform.Rotate(0, 6.0f * rotationsPerMinute * Time.deltaTime, 0);           
        }
    }
    void CheckPlayerInRange()
    {
        if (player != null)
        {
            // Check distance between the player and this object
            float distance = Vector3.Distance(player.position, transform.position);

            // If the player is within the detection radius
            if (distance <= detectionRadius && isDrillOn)
            {
                if (!playerInRange) // Ensure this only triggers once when the player enters
                {
                    playerInRange = true;
                    playerScript.inDrillRange = true;
                }
            }
            else
            {
                if (playerInRange) // Ensure this only triggers once when the player leaves
                {
                    playerInRange = false;
                    playerScript.inDrillRange = false;
                }
            }
        }
    }
    void DebugDrawSphere()
    {
        Debug.DrawLine(transform.position, player.position, Color.red); // Draws a line between the object and the player
        Debug.DrawRay(transform.position, Vector3.up * detectionRadius, Color.red); // Draws a vertical ray to represent the radius
        // You can draw more visual elements to represent the sphere using Debug.DrawLine for a more complete visualization
    }
    void FuelWithRotationSpeed()
    {
        if(rotationsPerMinute <= 50 && rotationsPerMinute > 29)
        {
            fuelDraningSpeed = 0.5f;
        }
        else if(rotationsPerMinute <= 60 && rotationsPerMinute > 50)
        {
            fuelDraningSpeed = 0.6f;
        }
        else if (rotationsPerMinute <= 70 && rotationsPerMinute > 60)
        {
            fuelDraningSpeed = 0.7f;
        }
        else if (rotationsPerMinute <= 80 && rotationsPerMinute > 70)
        {
            fuelDraningSpeed = 0.8f;
        }
        else if (rotationsPerMinute <= 90 && rotationsPerMinute > 80)
        {
            fuelDraningSpeed = 0.9f;
        }
        else if (rotationsPerMinute <= 100 && rotationsPerMinute > 90)
        {
            fuelDraningSpeed = 1f;
        }
        else
        {
            Debug.LogError("Something is Wrong with Code");
        }
    }
    // Optional: Unity has a built-in method to visualize spheres in the Scene view.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Red sphere
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // Draws a wireframe sphere in Scene view
    }
    
    void DrillConsumingFuel(float fuel)
    {
        fuelAmount -= fuel;
    }
    public void DrillAddingFuel(float fuel)
    {
        fuelAmount += fuel;
        if(fuelAmount >= maxFuelAmount)
        {
            fuelAmount = maxFuelAmount;
        }
    }
}
