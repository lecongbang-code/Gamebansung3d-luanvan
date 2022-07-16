using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public AdvancedCamRecoil advancedCamRecoil;

    public WeaponPlayerM24 weaponPlayer;

    public PlayerAnimation playerAnimation;

    public PlayerController playerController;

    PlayerAiming playerAiming;

    public AudioSource audioSource;

    public Weapon weapon;

    public CameraLook cameraLook;

    public HitmarkerM24 hitmarker;
    
    public AudioManager audioManager;

    public GameObject itemSilencer;

    public float timeShoot = 0f;
    float timeShow = 0f;
    float reloadTime = 0f;
    float timeAiming = 0f;
    float timeStart = 0f ;
    float timeChangeGun = 0f;
    int totalBullet = 0;
    int bulletMax = 0;
    int bullet = 0;
    int a;
    
    public bool isChangeGun = false;

    public float timeStartShoot = 1.2f;

    public float timeRefreshShoot = 1.7f;

    public float timeRefreshReload = 3.8f;

    public float timeRefreshChangeGun = 1.2f;

    void Awake() 
    {
        playerAiming = GetComponent<PlayerAiming>();
        bulletMax = weapon.bulletMax;
        bullet = bulletMax;
        totalBullet = weapon.totalBullet;
        hitmarker.SelectTextBullet(bulletMax,totalBullet);
    }

    void Update()
    {
        InputAiming();

        Reload();

        SelectGun();

        SelectSilencer();
    }

    public void InputAiming()
    {
        if(timeStart < 3f)
        {
            timeStart += Time.deltaTime;
        }

        if(timeStart >= timeStartShoot)
        {
            if(timeShoot < 3f)
            {
                timeShoot += Time.deltaTime;
            }

            if(playerController.aiming)
            {
                timeAiming += Time.deltaTime;
            }
            else
            {
                timeAiming = 0f;
            }

            if(Input.GetMouseButton(0) && !playerController.reload && playerController.aiming && !playerController.run && !isChangeGun)
            {
                if(bulletMax > 0)
                {
                    if(timeShoot > timeRefreshShoot && timeAiming > 0.15f)
                    {
                        advancedCamRecoil.Fire(playerController.aiming);
                        bulletMax = bulletMax - 1;
                        weaponPlayer.Shoot();
                        cameraLook.Advan();
                        playerAnimation.animator.SetBool("Shoot", true);
                        timeShoot = 0.0f;
                        playerController.aiming = false;
                        playerAiming.OnUnScoped();
                        hitmarker.SelectTextBullet(bulletMax, totalBullet);
                    }
                }
                else
                {
                    if(timeShoot > 0.2f)
                    {
                        audioManager.Play("OverBullet");
                        playerController.aiming = false;
                        playerAiming.OnUnScoped();
                        timeShoot = 0.0f;
                    }
                }
            }
            else
            {
                playerAnimation.animator.SetBool("Shoot", false);
            }

            if(Input.GetMouseButton(0) && !playerController.reload && !playerController.aiming && !playerController.run && !isChangeGun)
            {
                if(bulletMax > 0)
                {
                    if(timeShoot > timeRefreshShoot)
                    {
                        advancedCamRecoil.Fire(!playerController.aiming);
                        bulletMax = bulletMax - 1;
                        weaponPlayer.Shoot();
                        cameraLook.Advan();
                        playerAnimation.animator.SetBool("Shoot", true);
                        timeShoot = 0.0f;
                        playerController.aiming = false;
                        playerAiming.OnUnScoped();
                        hitmarker.SelectTextBullet(bulletMax, totalBullet);
                    }
                }
                else
                {
                    if(timeShoot > 0.2f)
                    {
                        audioManager.Play("OverBullet");
                        timeShoot = 0.0f;
                    }
                }
            }
            else
            {
                playerAnimation.animator.SetBool("Shoot", false);
            }
        }
    }

    public void Reload()
    {
        if(Input.GetKeyDown(KeyCode.R) && timeShoot > timeRefreshShoot && playerController.reload == false && !playerController.run && bulletMax != bullet && totalBullet > 0 && !isChangeGun)
        {
            reloadTime = 0f;
            playerController.aiming = false;
            playerAiming.OnUnScoped();
            playerController.reload = true;
            audioSource.Play();
            playerAnimation.animator.SetBool("Reload", true);
        }

        if(bulletMax == 0 && timeShoot > timeRefreshShoot && playerController.reload == false && !playerController.run && bulletMax != bullet && totalBullet > 0 && !isChangeGun)
        {
            reloadTime = 0f;
            playerController.aiming = false;
            playerAiming.OnUnScoped();
            playerController.reload = true;
            audioSource.Play();
            playerAnimation.animator.SetBool("Reload", true);
        }

        if(playerController.reload)
        {
            reloadTime += Time.deltaTime; 
            timeShow = reloadTime;
            timeShow = (float)System.Math.Round(timeShow, 1);
            hitmarker.textReload.text = ""+timeShow;
            hitmarker.TimeReload.SetActive(true);
            hitmarker.SelectSliderReload(true);
        }

        if(reloadTime > timeRefreshReload)
        {
            hitmarker.TimeReload.SetActive(false);
            playerAnimation.animator.SetBool("Reload", false);
            hitmarker.SelectTextBullet(bulletMax, totalBullet);
        }
        if(reloadTime < timeRefreshReload && playerController.run)
        {
            audioSource.Stop();
            playerController.reload = false;
            hitmarker.TimeReload.SetActive(false);
            hitmarker.SelectSliderReload(false);
        }
        if(reloadTime > timeRefreshReload + 0.1f)
        {
            audioSource.Stop();
            playerController.reload = false;
            hitmarker.SelectSliderReload(false);

            if(totalBullet >= bullet)
            {
                totalBullet = totalBullet - bullet + bulletMax;
                bulletMax = bullet;
                weapon.totalBullet = totalBullet;
            }
            else
            {
                a = bullet - bulletMax;

                if(a > totalBullet)
                {
                    bulletMax = bulletMax + totalBullet;
                    totalBullet = 0;
                    weapon.totalBullet = totalBullet;
                }
                if(a <= totalBullet)
                {
                    bulletMax = bulletMax + a;

                    totalBullet = totalBullet - a;
                    
                    weapon.totalBullet = totalBullet;
                }
            }
            
            hitmarker.SelectTextBullet(bulletMax, totalBullet);
            reloadTime = 0f;
        }
    }

    public void SelectGun()
    {
        if(isChangeGun)
        {
            timeChangeGun += Time.deltaTime;
        }
        
        if(Input.GetKeyDown(KeyCode.Q) && !playerController.aiming && !playerController.reload && !playerController.run && !isChangeGun && timeShoot > timeRefreshChangeGun - 0.2f)
        {
            audioManager.Play("OffScope");
            playerAnimation.animator.SetTrigger("Select");
            audioManager.AudioOnGun();
            isChangeGun = true;
        }
        if(timeChangeGun > timeRefreshChangeGun)
        {
            isChangeGun = false;
            timeChangeGun = 0f;
        }
    }

    public void SelectSilencer()
    {
        if(Input.GetKeyDown(KeyCode.O) && !playerController.aiming && !playerController.reload && !playerController.run && !isChangeGun && weapon.silencer)
        {
            if(!weaponPlayer.silencerActive)
            {
                weaponPlayer.silencerActive = true;
                audioManager.Play("OnSilencer");
                itemSilencer.SetActive(true);
            }
            else
            {
                weaponPlayer.silencerActive = false;
                audioManager.Play("OnSilencer");
                itemSilencer.SetActive(false);
            }
        }
    }
}
