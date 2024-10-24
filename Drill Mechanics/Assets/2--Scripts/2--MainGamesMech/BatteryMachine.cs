using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryMachine : MonoBehaviour
{
    [SerializeField] private float maxBatteryCapacity = 100f; // Max battery charge
    [SerializeField] private float currentBattery; // Current battery level
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] GameObject batteryPrefab;
    [SerializeField] Transform batterySpawnPos;

    private bool isBatteryPlaced = false; // Check if the battery has been placed
    private List<ElectricDevice> connectedDevices = new List<ElectricDevice>(); // List of all devices connected to the battery

    void Start()
    {
        PlaceBattery();
    }

    void Update()
    {
        if (isBatteryPlaced && connectedDevices.Count > 0) // Only drain if battery is placed and devices are connected
        {
            DrawElectricity();
        }
        if(currentBattery <=0)
        {
            foreach (ElectricDevice device in connectedDevices)
            {
                device.isBatteryCapacityEnds = true;
            }
        }
        else
        {
            foreach (ElectricDevice device in connectedDevices)
            {
                device.isBatteryCapacityEnds = false;
            }
        }
    }

    // This function is called when the player places a battery into the machine
    public void PlaceBattery()
    {
        isBatteryPlaced = true;
        currentBattery = maxBatteryCapacity;
        GameObject battery = Instantiate(batteryPrefab, batterySpawnPos.position, Quaternion.identity, this.transform);
        Debug.Log("Battery placed, power supply started.");
    }

    // Function to add an electric device to the connected list
    public void ConnectDevice(ElectricDevice device)
    {
        connectedDevices.Add(device);
        Debug.Log("Device connected: " + device.deviceName);
    }

    // Function to remove device when turned off or disconnected
    public void DisconnectDevice(ElectricDevice device)
    {
        connectedDevices.Remove(device);
        Debug.Log("Device disconnected: " + device.deviceName);
    }

    private void DrawElectricity()
    {
        // Calculate total drain based on consumption of each device
        float totalConsumption = 0f;
        foreach (ElectricDevice device in connectedDevices)
        {
            totalConsumption += device.powerConsumption;
        }

        // Drain battery based on the total consumption
        float drainAmount = totalConsumption * Time.deltaTime;
        currentBattery -= drainAmount;

        Debug.Log($"Battery Level: {currentBattery} | Consumption: {totalConsumption}");

        if (currentBattery <= 0)
        {
            //ExplodeBattery();
            Debug.Log("Battery has reached 0! It will now explode!");
            isBatteryPlaced =false;
        }
    }

    private void ExplodeBattery()
    {
        Debug.Log("Battery has reached 0! It will now explode!");
        Instantiate(explosionEffect, transform.position, transform.rotation); // Play explosion effect
        Destroy(gameObject); // Destroy the machine (or battery) after explosion
    }
}
