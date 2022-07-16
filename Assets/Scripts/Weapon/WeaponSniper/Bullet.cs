using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speedBullet;
    public int damage;

    Vector3 lastPos;

    public GameObject decal;
    public GameObject metalHiteffect;
    public GameObject sandHiteffect;
    public GameObject stoneHiteffect;
    public GameObject waterLeakEffect;
    public GameObject waterLeakExtinguishEffect;
    public GameObject[] meatHiteffect;
    public GameObject woodHiteffect;

    public ParticleSystem muzzleFlash;
	public ParticleSystem cartridgeEjection;

    public Weapon weapon;

    public HitmarkerM24 hitmarker;
    
    void Start()
    {
        lastPos = transform.position;
        Destroy(gameObject, 5);
        speedBullet = weapon.speedBullet;
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
                    case "Metal":
                    SpawnDecal(hit, metalHiteffect);
                    break;
                    case "Sand":
                    SpawnDecal(hit, sandHiteffect);
                    break;
                    case "Stone":
                    SpawnDecal(hit, stoneHiteffect);
                    break;
                    case "WaterFilled":
					SpawnDecal(hit, waterLeakEffect);
					SpawnDecal(hit, metalHiteffect);
					break;
                    case "Wood":
                    SpawnDecal(hit, woodHiteffect);
                    break;
                    case "Meat":
                    Meat(hit);
                    SpawnDecal(hit, meatHiteffect[Random.Range(0, meatHiteffect.Length)]);
                    hitmarker.timeLive = hitmarker.maxTimeLive;
                    break;
                    case "WaterFilledExtinguish":
                    SpawnDecal(hit, waterLeakExtinguishEffect);
                    SpawnDecal(hit, metalHiteffect);
                    break;
                }
            }
            Destroy(gameObject);
        }
        lastPos = transform.position;
    }

    public void Meat(RaycastHit hit)
    {
        if(hit.transform.GetComponent<Hitposition>() != null)
        {
            hit.transform.GetComponent<Hitposition>().Damage(damage);
        }
    }

    void SpawnDecal(RaycastHit hit, GameObject prefab)
    {
        GameObject spawnDecal = GameObject.Instantiate(prefab, hit.point, Quaternion.LookRotation(hit.normal));
        spawnDecal.transform.SetParent(hit.collider.transform);
        Destroy(spawnDecal.gameObject, 5);
    }
}
