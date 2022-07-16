using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public HitPlayer hitPlayer;
    public Point pointScript;
    Animator anim;
    public Rigidbody[] rigid;
    public int health;
    public int damage;
    public int pointMin;
    public int pointMax;
    public ZombieController zombieController;
    Collider m_Collider;
    int point;

    public AudioManager audioManager;

    public GameObject keyGold;

    void Start()
    {
        anim = GetComponent<Animator>();
        m_Collider = GetComponent<Collider>();
    }
    
    public void TakeAwayHealth(int TakeAway)
    {
        if(health > 0)
        {
            health -= TakeAway;
            zombieController.lookRadius = 100f;
            zombieController.animator.SetTrigger("SelectHit");

            if(health <=0)
            {
                zombieController.lookRadius = 0f;
                Dead();
                m_Collider.enabled = !m_Collider.enabled;
                point = Random.Range(pointMin, pointMax);
                pointScript.SelectPoint(point);
                pointScript.SelectShootKill();
            }
        }
    }

    public void Dead()
    {
        audioManager.Play("zombie_dead");
        anim.SetBool("Dead",true);
        if(Random.Range(1, 10) == 5)
        {
            CreateKey();
        }
        Destroy(gameObject, 2f);
    }

    public void Attack()
    {
        if(health > 0 && hitPlayer.timeActive > 0.05f)
        {
            hitPlayer.ActiveHitPlayer(damage);
        }
    }

    public void CreateKey()
    {
        Instantiate(keyGold, transform.position ,Quaternion.identity);
    }
}
