using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFuelCollection : MonoBehaviour
{
    PlayerMovement player;
    public bool hadPetrolContainer;
    [SerializeField] GameObject fuelContainer;
    [SerializeField] Transform postionToSpawn;
    BuildingTurrets buildingTurrets;
    private void Start()
    {
        hadPetrolContainer = false;
        player = GetComponent<PlayerMovement>();
        buildingTurrets = GameObject.FindGameObjectWithTag("Turrets").GetComponent<BuildingTurrets>();
    }
    // Update is called once per frame
    void Update()
    {
        if (hadPetrolContainer)
        {
            GameObject fuelContainerInHand = Instantiate(fuelContainer, postionToSpawn.position,Quaternion.identity,postionToSpawn);
            hadPetrolContainer = false;
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(buildingTurrets != null)
            {
                if(buildingTurrets.isTurretActive == false)
                {
                    buildingTurrets.isTurretActive = true;
                }
            }
        }
    }
}
