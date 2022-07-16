using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActiveAttack : MonoBehaviour
{
    public float lookRadius;

    public EnemyStat enemyStat;

    Transform target;

    public NavMeshAgent agent;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent.stoppingDistance = 5f;
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position); 

        if(distance <= agent.stoppingDistance)
        {
            enemyStat.Attack();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
