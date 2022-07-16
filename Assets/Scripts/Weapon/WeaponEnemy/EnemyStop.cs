using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStop : MonoBehaviour
{
    public float lookRadius;

    Transform target;

    public ZombieController zombieController;

    public NavMeshAgent agent;

    float timeStop = 2f;

    float timeStart = 0f;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position); 

        if(distance <= lookRadius)
        {
            if(timeStop < 3)
            {
                timeStop += Time.deltaTime;
            }

            zombieController.agent.speed = 0f;

            if(timeStop > 2.35f)
            {
                zombieController.animator.SetTrigger("StopTrigger");
                timeStop = 0f;
            }
        }
        else
        {
            if(timeStart < 3)
            {
                timeStart += Time.deltaTime;
            } 
            if(timeStart > 2f)
            {
                zombieController.agent.speed = zombieController.moveSpeed;
                timeStart = 0f;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
