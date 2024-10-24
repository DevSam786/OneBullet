using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCheck : MonoBehaviour
{
    public int damage;
    public string playerTag;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }
}
