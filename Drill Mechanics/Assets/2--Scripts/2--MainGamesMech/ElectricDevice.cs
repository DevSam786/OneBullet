using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricDevice : MonoBehaviour
{
    BatteryMachine batteryMachine;
    public float powerConsumption;
    [HideInInspector]public string deviceName;
    public bool isBatteryCapacityEnds;
    private void Start()
    {
        batteryMachine = GameObject.FindGameObjectWithTag("BatteryHolder").GetComponent<BatteryMachine>();
        deviceName = this.gameObject.name;
        batteryMachine.ConnectDevice(this);
    }
    
    private void OnDisable()
    {
        batteryMachine.DisconnectDevice(this);
    }

}
