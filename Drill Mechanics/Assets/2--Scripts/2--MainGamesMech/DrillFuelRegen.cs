using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillFuelRegen : MonoBehaviour
{
    [SerializeField] float fuelRegenAmount;
    [SerializeField] string machineTag;
    PlayerFuelCollection playerFuelCollection;

    private void Start()
    {
        playerFuelCollection = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFuelCollection>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(machineTag))
        {
            BatteryMachine batteryMachine = other.GetComponent<BatteryMachine>();
            if (batteryMachine != null )
            {
                batteryMachine.PlaceBattery();
                playerFuelCollection.isBatteryInHand = false;
                Destroy(gameObject);
            }
        }
    }
   
}
