using UnityEngine;

public class KnifeInput : MonoBehaviour
{
    public int damageLeft;
    public int damageRight;

    public GameObject knife;

    public Transform shotPoint;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("AttackLeft" ,true);
        }
        else
        {
            animator.SetBool("AttackLeft" ,false);
        }

        if(Input.GetMouseButtonDown(1))
        {
            animator.SetBool("AttackRight" ,true);
        }
        else
        {
            animator.SetBool("AttackRight" ,false);
        }
    }

    void KnifeAtLeft()
    {
        GameObject newKnife = Instantiate(knife, shotPoint.position, shotPoint.rotation);
        newKnife.SetActive(true);
        newKnife.GetComponent<KnifeAttack>().damage = damageLeft;
        Destroy(newKnife,0.15f);
    }

    void KnifeAtRight()
    {
        GameObject newKnife = Instantiate(knife, shotPoint.position, shotPoint.rotation);
        newKnife.SetActive(true);
        newKnife.GetComponent<KnifeAttack>().damage = damageRight;
        Destroy(newKnife,0.2f);
    }
}
