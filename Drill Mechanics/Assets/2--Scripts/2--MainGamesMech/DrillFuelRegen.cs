using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillFuelRegen : MonoBehaviour
{
    [SerializeField] float fuelRegenAmount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Drill"))
        {
            DrillBase drillBase = other.GetComponent<DrillBase>();
            if (drillBase != null )
            {
                drillBase.DrillAddingFuel(fuelRegenAmount);
                Destroy(gameObject);
            }
        }
    }
}
