using UnityEngine;

public class KnifeAttack : MonoBehaviour
{
    public float speedBullet;
    public int damage;

    public HitmarkerAKM hitmarker;

    Vector3 lastPos;

    void Awake()
    {
        lastPos = transform.position;
        try
        {
            hitmarker = GameObject.Find("CrosshairManager").GetComponent<HitmarkerAKM>();
        }
        catch
        {
            // --
        }    
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speedBullet * Time.deltaTime);

        RaycastHit hit;

        Debug.DrawLine(lastPos, transform.position);
        
        if(Physics.Linecast(lastPos, transform.position, out hit))
        {
            if(hit.collider.sharedMaterial != null)
            {
                string materialName = hit.collider.sharedMaterial.name;
                switch(materialName)
                {
                    case "Meat":
                    Meat(hit);
                    hitmarker.timeLive = hitmarker.maxTimeLive;
                    print ("Attack");
                    break;
                }
            }
            Destroy(gameObject);
        }
        lastPos = transform.position;
    }

    void Meat(RaycastHit hit)
    {
        if(hit.transform.GetComponent<Hitposition>() != null)
        {
            hit.transform.GetComponent<Hitposition>().Damage(damage);
        }
    }
}

