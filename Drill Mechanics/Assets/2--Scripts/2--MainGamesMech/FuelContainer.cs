using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelContainer : MonoBehaviour
{
    [SerializeField] string playerTag;
    private void OnTriggerEnter(Collider collison)
    {
        if (collison.CompareTag(playerTag))
        {
            PlayerFuelCollection player = collison.GetComponent<PlayerFuelCollection>();
            if (player.isBatteryInHand == false)
            {
                player.isBatteryInHand = true;
                Destroy(gameObject);
            }
        }
    }
}
