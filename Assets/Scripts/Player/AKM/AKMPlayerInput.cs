using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKMPlayerInput : MonoBehaviour
{
    public Reticle reticle;

    public AdvancedCamRecoil advancedCamRecoil;

    public WeaponPlayerAKM weaponPlayer;

    public AKMPlayerAnimation playerAnimation;

    public PlayerController playerController;

    AKMPlayerAiming playerAiming;

    public AudioSource audioSource;

    public Weapon weapon;

    public CameraLook cameraLook;

    public HitmarkerAKM hitmarker;

    public AudioManager audioManager;

    public GameObject ItemSilencer;

    public float timeShow;
    public float shootTime = 0f;
    float reloadTime = 0f;
    float timeAiming = 0f;
    float timeStart = 0f ;
    float timeView;
    float timeChangeGun = 0f;
    int totalBullet;
    int bulletMax;
    int bullet;
    int a; 

    public bool isChangeGun = false;
    
    public float timeStartShoot = 1f;

    public float timeRefreshShoot = 0.1065f;

    public float timeRefreshReload = 2.2f;

    public float timeRefreshChangeGun = 1.5f;

    void Start() 
    {
        playerAiming = GetComponent<AKMPlayerAiming>();
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
        if(timeStart < 2)
        {
            timeStart += Time.deltaTime;
        }

        if(timeStart >= timeStartShoot)
        {
            if(shootTime < 3f)
            {
                shootTime += Time.deltaTime;
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
                    if(shootTime >= timeRefreshShoot && timeAiming >= 0.1f)
                    {
                        advancedCamRecoil.Fire(playerController.aiming);
                        bulletMax = bulletMax - 1;
                        weaponPlayer.Shoot();
                        cameraLook.Advan();
                        playerAnimation.animator.SetTrigger("AimingShootTrigger");
                        playerAnimation.animator.SetBool("Shoot", true);
                        shootTime = 0.0f;
                        hitmarker.SelectTextBullet(bulletMax, totalBullet);
                    }
                }
                else
                {
                    if(shootTime > 0.2f)
                    {
                        audioManager.Play("OverBullet");
                        playerController.aiming = false;
                        playerAiming.OnUnScoped();
                        shootTime = 0.0f;
                    }
                }
            }
            else
            {
                playerAnimation.animator.SetBool("Shoot", false);
            }

            if(bulletMax == 0)
            {
                if(shootTime >= timeRefreshShoot && playerController.reload == false && !playerController.run && bulletMax != bullet && totalBullet > 0 && !isChangeGun)
                {
                    reloadTime = 0f;
                    playerController.aiming = false;
                    playerAiming.OnUnScoped();
                    playerController.reload = true;
                    audioSource.Play();
                    playerAnimation.animator.SetBool("Reload", true);
                }
            }

            if(Input.GetMouseButton(0) && !playerController.reload && !playerController.aiming && !playerController.run && !isChangeGun)
            {
                if(bulletMax > 0)
                {
                    if(shootTime >= timeRefreshShoot)
                    {
                        advancedCamRecoil.Fire(!playerController.aiming);
                        bulletMax = bulletMax - 1;
                        weaponPlayer.Shoot();
                        cameraLook.Advan();
                        // playerAnimation.animator.SetTrigger("ShootTrigger");
                        playerAnimation.animator.SetBool("Shoot", true);
                        shootTime = 0.0f;
                        playerController.aiming = false;
                        playerAiming.OnUnScoped();
                        hitmarker.SelectTextBullet(bulletMax, totalBullet);
                        reticle.isReticle(true);
                    }
                }
                else
                {
                    if(shootTime >= 0.2f)
                    {
                        audioManager.Play("OverBullet");
                        shootTime = 0.0f;
                    }
                }
            }
            else
            {
                playerAnimation.animator.SetBool("Shoot", false);
                reticle.isReticle(false);
            }
            if(bulletMax == 0)
            {
                if(shootTime >= timeRefreshShoot && playerController.reload == false && !playerController.run && bulletMax != bullet && totalBullet > 0 && !isChangeGun)
                {
                    reloadTime = 0f;
                    playerController.aiming = false;
                    playerAiming.OnUnScoped();
                    playerController.reload = true;
                    audioSource.Play();
                    playerAnimation.animator.SetBool("Reload", true);
                }
            }
        }

    }

    public void Reload()
    {
        if(Input.GetKeyDown(KeyCode.R) && shootTime >= timeRefreshShoot && playerController.reload == false && !playerController.run && bulletMax != bullet && totalBullet > 0 && !isChangeGun)
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
        }
        if(reloadTime < timeRefreshReload && playerController.run)
        {
            audioSource.Stop();
            hitmarker.TimeReload.SetActive(false);
            hitmarker.SelectSliderReload(false);
        }
        if(reloadTime > timeRefreshReload + 0.1f)
        {
            playerController.reload = false;
            hitmarker.SelectSliderReload(false);

            // nếu tổng đạn của vũ khí >= số đạn cần thay
            if(totalBullet >= bullet)
            {
                // tổng đạn của vũ khí = tổng đạn của vũ khí - số đạn cần thay + số đạn còn dư
                totalBullet = totalBullet - bullet + bulletMax;
                // cập nhật lại số đạn dư (số đạn sử dụng trong khi bắn)
                bulletMax = bullet;
                // cập nhật lại tổng đạn của vũ khí
                weapon.totalBullet = totalBullet;
            }
            else
            {
                // tạo biến a ngoài cái hàm để lưu giá trị
                // a = số đạn cần thay - số đạn còn dư
                a = bullet - bulletMax;

                // nếu a > tổng đạn của vũ khí
                if(a > totalBullet)
                {
                    // số đạn còn dư = số đạn còn dư + tổng đạn của vũ khí
                    bulletMax = bulletMax + totalBullet;
                    // tổng đạn của vũ khí = 0
                    totalBullet = 0;
                    // cập nhật lại tổng đạn của vũ khí
                    weapon.totalBullet = totalBullet;
                }
                // nếu a <= tổng đạn của vũ khí
                if(a <= totalBullet)
                {
                    // số đạn còn dư = số đạn còn dư + a
                    bulletMax = bulletMax + a;
                    // tổng đạn của vũ khí = tổng đạn của vũ khí - a
                    totalBullet = totalBullet - a;
                    // cập nhật lại tổng đạn của vũ khí
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
        
        if(Input.GetKeyDown(KeyCode.Q) && !playerController.aiming && !playerController.reload && !playerController.run && !isChangeGun && shootTime > timeRefreshShoot)
        {
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
                ItemSilencer.SetActive(true);
            }
            else
            {
                weaponPlayer.silencerActive = false;
                audioManager.Play("OnSilencer");
                ItemSilencer.SetActive(false);
            }
        }
    }
}
