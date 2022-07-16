using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKMPlayerAiming : MonoBehaviour
{
    public GameObject activeReticle;
    public GameObject scopeOverlay;
    public GameObject weaponeCamera;
    public Camera mainCamera;
    public PlayerController playerController;
    AKMPlayerInput playerInput;
    public Weapon weapon;

    public AudioManager audioManager;
    
    float scopedFOV = 60f;
    float normalFOV = 60f;

    AKMPlayerAnimation playerAnimation;

    bool aiming = false;
    float recoil;
    float recoilWeapon;

    void Start()
    {
        recoil = weapon.recoil;
        recoilWeapon = (weapon.recoil / 100) * 50;
        playerController.aiming = aiming;
        playerAnimation = GetComponentInParent<AKMPlayerAnimation>();
        playerInput = GetComponentInParent<AKMPlayerInput>();
    }

    void Update()
    {
        Aiming();
    }

    void Aiming()
    {
        if(Input.GetButtonDown("Fire2") && !playerController.reload && !playerController.run && !playerInput.isChangeGun)
        {
            audioManager.Play("Onscope");
        }

        if(Input.GetButtonUp("Fire2") && !playerController.reload && !playerController.run && !playerInput.isChangeGun)
        {
            audioManager.Play("Offscope");
        }

        if(Input.GetButton("Fire2") && !playerController.reload && !playerController.run && !playerInput.isChangeGun)
        {
            playerController.aiming = true;
            weapon.aim = true;
            weapon.recoil = recoilWeapon;
            StartCoroutine(OnScoped());
            activeReticle.SetActive(false);
        }
        else
        {
            playerController.aiming = false;
            weapon.aim = false;
            weapon.recoil = recoil;
            OnUnScoped();
            activeReticle.SetActive(true);
        }

        playerAnimation.animator.SetBool("Aiming", playerController.aiming);

        scopeOverlay.SetActive(playerController.aiming);
    }

    public void OnUnScoped()
    {
        scopeOverlay.SetActive(false);

        weaponeCamera.SetActive(true);

        mainCamera.fieldOfView = normalFOV;
    }

    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(0.1f);

        scopeOverlay.SetActive(true);

        weaponeCamera.SetActive(false);

        mainCamera.fieldOfView = scopedFOV;
    }
}
