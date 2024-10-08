using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    public bool isAttackAvailble;
    [SerializeField] int health;
    [HideInInspector]public NavMeshAgent agent;
    [HideInInspector] public Animator  anim;
    [HideInInspector]public Transform player;
    [SerializeField] string playerTag;
    [SerializeField] string attackTag;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(player != null)
        {
            player = GameObject.FindGameObjectWithTag(playerTag).transform;
            if (Vector3.Distance(transform.position, player.position) < agent.stoppingDistance)
            {
                //agent.SetDestination(transform.position);
                agent.enabled = false;
                anim.SetBool(attackTag,true);
                isAttackAvailble = true;
            }
            else
            {
                agent.enabled = true;
                agent.SetDestination(player.position);
                anim.SetBool(attackTag,false);
                isAttackAvailble = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
