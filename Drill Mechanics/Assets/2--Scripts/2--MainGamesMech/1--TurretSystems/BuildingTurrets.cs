using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTurrets : MonoBehaviour
{
    public PlayerFuelCollection player;
    public bool isTurretActive;
    [SerializeField] GameObject turretGameobject;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFuelCollection>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if(isTurretActive)
            {
                TurretActive();
            }
        }
    }
    void TurretActive()
    {
        turretGameobject.SetActive(true);
    }
}
