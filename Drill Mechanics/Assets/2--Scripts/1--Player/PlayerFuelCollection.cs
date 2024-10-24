using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFuelCollection : MonoBehaviour
{
    PlayerMovement player;    
    [SerializeField] GameObject fuelContainer;
    [SerializeField] GameObject weapon;
    [SerializeField] Transform postionToSpawn;
    BuildingTurrets buildingTurrets;
    public bool isBatteryInHand;
    private void Start()
    {
        player = GetComponent<PlayerMovement>();
        buildingTurrets = GameObject.FindGameObjectWithTag("Turrets").GetComponent<BuildingTurrets>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isBatteryInHand)
        {
            GameObject fuelContainerInHand = Instantiate(fuelContainer, postionToSpawn.position,Quaternion.identity,postionToSpawn);
            weapon.SetActive(false);
        }
        else
        {
            weapon.SetActive(true);
        }

    }

}
