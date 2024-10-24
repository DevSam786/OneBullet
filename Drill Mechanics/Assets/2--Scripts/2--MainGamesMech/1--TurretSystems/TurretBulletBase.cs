using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletBase : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed;
    [SerializeField] int damage;
    public void Seek(Transform _target)
    {
        Transform targetPos;
        targetPos = _target;
        //target.position = targetPos.position + new Vector3(0, 0.5f, 0);
        target = targetPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distancePerFrame = speed * Time.deltaTime;
        if(dir.magnitude <= distancePerFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distancePerFrame, Space.World);
    }
    void HitTarget()
    {
        Destroy(gameObject);
        target.GetComponent<EnemyBase>().TakeDamage(damage);
        Debug.Log("We Hit Something!");
    }
}
