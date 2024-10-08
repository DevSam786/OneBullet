using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField] string enemyTag;
    [SerializeField] int damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
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
