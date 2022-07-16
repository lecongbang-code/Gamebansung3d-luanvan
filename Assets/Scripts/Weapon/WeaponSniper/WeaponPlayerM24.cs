using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlayerM24 : MonoBehaviour
{
    public Transform shotPoint;
    public Transform targetLook;

    public GameObject CameraMain;
    // public GameObject decal;
    
    public GameObject bullet;

    public GameObject silencer;

    public ParticleSystem muzzleFlash;
    public ParticleSystem cartridgeEjection;

    public AudioManager audioManager;
    public HitmarkerM24 hitmarker;
    
    public GunS gunS;
    public Weapon weapon;

    int damage;
    string nameSilencer;

    public bool silencerActive;

    void Start()
    {
        damage = weapon.damage;
        hitmarker = GameObject.Find("CrosshairManager").GetComponent<HitmarkerM24>();
    }

    void Update()
    {
        shotPoint.LookAt(targetLook);
        Vector3 origin = shotPoint.position;
        Vector3 dir = targetLook.position;

        //RaycastHit hit;
        
        Debug.DrawLine(origin,dir,Color.white);
        Debug.DrawLine(CameraMain.transform.position,dir,Color.blue); 

        // if(Physics.Linecast(origin, dir, out hit))
        // {
        //     decal.transform.position = hit.point + hit.normal * 0.01f;
        //     decal.transform.rotation = Quaternion.LookRotation(-hit.normal);
        // }

        SetActiveSilencer();
    }

    public void SetActiveSilencer()
    {
        if(silencerActive)
        {
            silencer.SetActive(true);
            nameSilencer = "M24silencer";
        }
        else
        {
            silencer.SetActive(false);
            nameSilencer = "M24nosilencer";
        }
    }

    public void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, shotPoint.position, shotPoint.rotation);

        newBullet.GetComponent<Bullet>().hitmarker = hitmarker;
        newBullet.GetComponent<Bullet>().damage = damage;
        muzzleFlash.Play();
        cartridgeEjection.Play();
        gunS.Fire();
        audioManager.Play(nameSilencer);
    }
}
