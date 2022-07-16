using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    public GameObject scopeOverlay;
    public GameObject weaponeCamera;
    public Camera mainCamera;
    public PlayerController playerController;
    PlayerInput playerInput;

    public AudioManager audioManager;

    float scopedFOV = 15f;
    float normalFOV = 60f;

    PlayerAnimation playerAnimation;

    bool aiming = false;

    void Start()
    {
        playerController.aiming = aiming;
        playerAnimation = GetComponentInParent<PlayerAnimation>();
        playerInput = GetComponentInParent<PlayerInput>();
    }

    void Update()
    {
        Aiming();

        scopeOverlay.SetActive(playerController.aiming);
    }

    void Aiming()
    {
        if(Input.GetButtonDown("Fire2") && playerInput.timeShoot > playerInput.timeRefreshShoot && !Input.GetButtonDown("Fire1") && !playerController.reload && !playerController.run && !playerInput.isChangeGun)
        {
            if(!playerController.aiming)
            {
                playerController.aiming = true;
                StartCoroutine(OnScoped());
            }
            else
            {
                playerController.aiming = false;
                OnUnScoped();
            }
        }

        playerAnimation.animator.SetBool("Aiming", playerController.aiming);
    }

    public void OnUnScoped()
    {
        audioManager.Play("OffScope");

        scopeOverlay.SetActive(false);

        weaponeCamera.SetActive(true);

        mainCamera.fieldOfView = normalFOV;
    }

    IEnumerator OnScoped()
    {
        audioManager.Play("OnScope");

        yield return new WaitForSeconds(0.08f);

        scopeOverlay.SetActive(true);

        weaponeCamera.SetActive(false);

        mainCamera.fieldOfView = scopedFOV;
    }
}
