using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    GameObject[] spawnPoint;

    public float lookRadius;

    public float stoppingDistance;

    public float moveSpeed;

    public GameObject activeAttack;

    public Animator animator;

    Transform target;

    public NavMeshAgent agent;

    public AudioManager audioManager;

    public AudioMoveMent audioMoveMent;

    public AudioMoveMent audioZombieIdel;

    bool isAttack;
    float timeAttack = 0f;
    float timeMovemen = 0f;
    public float timeZombieAttack = 0f;
    public float timeZombieAudio = 0f;

    float nextState;
    GameObject pointMove;

    void Start()
    {
        nextState = Random.Range(2,15);
        spawnPoint = GameObject.FindGameObjectsWithTag("SpawnEnemy");
        timeZombieAudio = Time.time;
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;
        agent.speed = moveSpeed;
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position); 

        nextState -= Time.deltaTime;

       if(distance <= lookRadius)
       {
            if(timeMovemen < 3f)
            {
                timeMovemen += Time.deltaTime * 1f;
            }
            if(timeMovemen > 2f)
            {
                timeMovemen = 2f;
            }

            animator.SetFloat("Vertical", timeMovemen);

            if(distance <= agent.stoppingDistance)
            {
                FaceTarget();

                isAttack = true;

                if(timeMovemen >=0 )
                {
                    timeMovemen -= Time.deltaTime * 1f;
                }
                agent.speed = 0f;

                animator.SetFloat("Vertical", timeMovemen);

                animator.SetBool("Attack", true);
                
            }

            if(isAttack)
            {
                timeAttack += Time.deltaTime * 1f;
            }

            if(timeAttack > timeZombieAttack)
            {
                animator.SetBool("Attack", false);

                agent.speed = moveSpeed;
            }
            
            if(timeAttack > timeZombieAttack + 0.1f)
            {
                isAttack = false;

                timeAttack = 0f;
            }

            if(distance >= agent.stoppingDistance && !isAttack && timeMovemen > 0.5f)
            {
                agent.SetDestination(target.position);

                animator.SetFloat("Vertical", timeMovemen);
            }
        }
        else
        {
            if(Time.time >= timeZombieAudio + Random.Range(8,20))
            {
                audioZombieIdel.StepZombieIdel();

                timeZombieAudio = Time.time;
            }

            timeMovemen = 0f;

            if(nextState <= 0)
            {
                int point = Random.Range(0, spawnPoint.Length);

                pointMove = spawnPoint[point];

                agent.SetDestination(pointMove.transform.position);

                nextState = Random.Range(5,15);
            }

            transform.position += (agent.desiredVelocity * Time.deltaTime)/1000f;

            if(agent.desiredVelocity.magnitude > 0.2)
            {
                timeMovemen = 1.5f;
            }

            animator.SetFloat("Vertical", timeMovemen);
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }

    void PlayAudioAttack()
    {
        audioManager.Play("zombie_attack");
    }

    void PlayAudioShout()
    {
        audioManager.Play("zombie_shout");
    }

    void PlayAudioMove()
    {
        audioMoveMent.StepZombieRun();
    }

    void OnActiveAttack()
    {
        activeAttack.SetActive(true);
    }

    void OffActiveAttack()
    {
        activeAttack.SetActive(false);
    }

    void OnHitAnim()
    {
        animator.SetBool("Attack", false);
        agent.speed = 0f;
        isAttack = true;
        timeAttack = 0f;
    }

    void OffHitAnim()
    {
        agent.speed = moveSpeed;
        isAttack = false;
    }

    void DeadAnim()
    {
        agent.velocity = Vector3.zero;
        agent.angularSpeed = 0;
        agent.acceleration = 0;
        agent.speed = 0f;
        agent.stoppingDistance = 0;
    }
}
