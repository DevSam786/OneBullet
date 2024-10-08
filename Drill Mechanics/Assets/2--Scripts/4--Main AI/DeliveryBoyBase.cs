using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeliveryBoyBase : MonoBehaviour
{
    [SerializeField] int health;
    [HideInInspector] NavMeshAgent agent;
    [HideInInspector] Animator anim;
    [SerializeField] Transform desination;
    [SerializeField] string destinationTag;    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        desination = GameObject.FindGameObjectWithTag(destinationTag).transform;

    }

    // Update is called once per frame
    void Update()
    {
        desination = GameObject.FindGameObjectWithTag(destinationTag).transform;
        agent.SetDestination(desination.position);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
