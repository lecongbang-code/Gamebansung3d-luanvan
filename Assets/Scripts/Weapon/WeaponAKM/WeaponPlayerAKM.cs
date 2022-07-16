using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlayerAKM : MonoBehaviour
{
    public Transform shotPoint;
    public Transform targetLook;

    public GameObject CameraMain;
    public GameObject bullet;
    public GameObject silencer;

    public ParticleSystem muzzleFlash;
    public ParticleSystem cartridgeEjection;

    public AudioManager audioManager;

    public HitmarkerAKM hitmarker;
    public GunS guns;
    public Weapon weapon;

    int damage;
    string nameSilencer;

    public bool silencerActive;

    void Start()
    {
        damage = weapon.damage;
        hitmarker = GameObject.Find("CrosshairManager").GetComponent<HitmarkerAKM>();
    }

    void Update()
    {
        shotPoint.LookAt(targetLook);
        Vector3 origin = shotPoint.position;
        Vector3 dir = targetLook.position;
        SetActiveSilencer();
    }

    public void SetActiveSilencer()
    {
        if(silencerActive)
        {
            silencer.SetActive(true);
            nameSilencer = "AKMsilencer";
        }
        else
        {
            silencer.SetActive(false);
            nameSilencer = "AKMnosilencer";
        }
    }

    public void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        newBullet.GetComponent<BulletAKM>().hitmarker = hitmarker;
        newBullet.GetComponent<BulletAKM>().damage = damage;
        muzzleFlash.Play();
        cartridgeEjection.Play();
        guns.Fire();
        audioManager.Play(nameSilencer);
    }
}
