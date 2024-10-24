using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField] string destroyObject;
    [SerializeField] int damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(destroyObject))
        {
            EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
