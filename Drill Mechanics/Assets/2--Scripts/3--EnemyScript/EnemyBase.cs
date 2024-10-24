using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    public bool isAttackAvailble;
    [SerializeField]  int health;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator  anim;
    [HideInInspector] public Transform player;
    [SerializeField] public string playerTag;
    [SerializeField] public string attackAnimTag;
    [SerializeField] public string walkingAnimTag;
    //Bool to Control Animations
    public bool shouldEnemyMoveAnim;
    public bool shouldEnemyAttackAnim;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("isWalking", true);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if (player != null)
        {
            player = GameObject.FindGameObjectWithTag(playerTag).transform;
            if (Vector3.Distance(transform.position, player.position) < agent.stoppingDistance)
            {
                //agent.SetDestination(transform.position);
                agent.enabled = false;                
                isAttackAvailble = true;
                anim.SetBool(attackAnimTag, true);
                anim.SetBool(walkingAnimTag, false);

            }
            else
            {
                agent.enabled = true;                
                agent.SetDestination(player.position);
                anim.SetBool(attackAnimTag, false);
                anim.SetBool(walkingAnimTag, true);
                isAttackAvailble = false;
            }
        }
        else
        {
            anim.SetBool(attackAnimTag, false);
            anim.SetBool(walkingAnimTag, false);
        }
    }
    public Vector3 PlayerPos(Vector3 pos)
    {
        return pos;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;        
    }
}
