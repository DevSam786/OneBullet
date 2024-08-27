using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    Transform player;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent  = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if(Vector3.Distance(transform.position, player.position) > agent.stoppingDistance)
            {
                agent.enabled = true;
                agent.SetDestination(player.position);
            }
            else
            {
                agent.enabled = false;
            }
        }
    }
}
