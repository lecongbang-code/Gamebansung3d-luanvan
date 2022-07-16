using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Option : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progrssText;

    public SetUpCammera setUpCammerAr;
    public SetUpCammera setUpCammerSr;

    public Slider speedmoveAr;
    public Slider speedmoveArAim;
    public Slider speedmoveSr;
    public Slider speedmoveSrAim;

    public TextMeshProUGUI txtValueAr;
    public TextMeshProUGUI txtValueArAim;
    public TextMeshProUGUI txtValueSr;
    public TextMeshProUGUI txtValueSrAim;

    public Main_controller main_Controller;
    public PlayerController playerController;
    public MissionControl missionControl;

    public Weapon weaponAkm;
    public Weapon weaponM4;
    public Weapon weaponM24;
    public Weapon weaponSmg9;
    public Weapon weaponImg; 

    public void RestartGame()
    {
        playerController.money = 9999999;
        playerController.gold = 10000;
        playerController.key = 50;
        playerController.medKit = 5;

        playerController.akm = true;
        playerController.m4 = true;
        playerController.m24 = true;
        playerController.smg9 = false;
        playerController.img = true;

        playerController.priceUpdataAkm = 5500;
        playerController.priceUpdataM4 = 5500;
        playerController.priceUpdataM24 = 5500;
        playerController.priceUpdataSmg9 = 5500;
        playerController.priceUpdataImg = 5500;

        playerController.selectAkm = false;
        playerController.selectM4 = false;
        playerController.selectM24 = false;
        playerController.selectSmg9 = true;
        playerController.selectImg = false;

        for(int i = 0; i < missionControl.zombie.Length; i++)
        {
            missionControl.zombie[i] = 0;
        }

        for(int i = 0; i < missionControl.zombieKill.Length; i++)
        {
            missionControl.zombieKill[i] = 15;
        }

        for(int i = 1; i < playerController.scene.Length; i++)
        {
            playerController.scene[i] = false;
        }

        weaponAkm.silencer = false;
        weaponAkm.bulletMax = 30;
        weaponAkm.damage = 25;
        weaponAkm.speedBullet = 200;
        weaponAkm.recoil = 0.75f;
        weaponAkm.totalBullet = 0;

        weaponM4.silencer = false;
        weaponM4.bulletMax = 30;
        weaponM4.damage = 25;
        weaponM4.speedBullet = 200;
        weaponM4.recoil = 0.65f;
        weaponM4.totalBullet = 0;

        weaponSmg9.silencer = false;
        weaponSmg9.bulletMax = 20;
        weaponSmg9.damage = 20;
        weaponSmg9.speedBullet = 220;
        weaponSmg9.recoil = 0.4f;
        weaponSmg9.totalBullet = 999;

        weaponM24.silencer = false;
        weaponM24.bulletMax = 7;
        weaponM24.damage = 200;
        weaponM24.speedBullet = 400;
        weaponM24.recoil = 3f;
        weaponM24.totalBullet = 0;

        weaponImg.silencer = false;
        weaponImg.bulletMax = 75;
        weaponImg.damage = 20;
        weaponImg.speedBullet = 210;
        weaponImg.recoil = 1.5f;
        weaponImg.totalBullet = 0;

        speedmoveAr.value = 120;
        speedmoveArAim.value = 30;
        speedmoveSr.value = 120;
        speedmoveSrAim.value = 20;
        
        main_Controller.LoadLevel(0);
    }

    void Start()
    {
        speedmoveAr.value = setUpCammerAr.speedNoAiming;
        txtValueAr.text = "" + setUpCammerAr.speedNoAiming;

        speedmoveArAim.value = setUpCammerAr.speedAiming;
        txtValueArAim.text = "" + setUpCammerAr.speedAiming;

        speedmoveSr.value = setUpCammerSr.speedNoAiming;
        txtValueSr.text = "" + setUpCammerSr.speedNoAiming;

        speedmoveSrAim.value = setUpCammerSr.speedAiming;
        txtValueSrAim.text = "" + setUpCammerSr.speedAiming;
    }

    void Update()
    {
        setUpCammerAr.speedNoAiming = speedmoveAr.value;
        txtValueAr.text = "" + setUpCammerAr.speedNoAiming;

        setUpCammerAr.speedAiming = speedmoveArAim.value;
        txtValueArAim.text = "" + setUpCammerAr.speedAiming;

        setUpCammerSr.speedNoAiming = speedmoveSr.value;
        txtValueSr.text = "" + setUpCammerSr.speedNoAiming;

        setUpCammerSr.speedAiming = speedmoveSrAim.value; 
        txtValueSrAim.text = "" + setUpCammerSr.speedAiming;
    }

    public void Play()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    public void LoadLevel (int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progrssText.text = progress * 100f + "%";

            yield return null;
        }
    }
}
