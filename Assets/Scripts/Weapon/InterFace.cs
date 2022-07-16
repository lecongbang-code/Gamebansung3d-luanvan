using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InterFace : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progrssText;

    public float health = 100;
    public float mana = 100;
    bool manaRun;

    public float colRed = 0;
    public float colGre = 0;

    public GameObject objUseMedkit;

    public TextMeshProUGUI txtUseMedkit;

    public Slider sliderUseMedkit;

    public Slider sliderHealth;

    public Slider sliderMana;

    public Image imageHealth;

    public Image imageMana;

    public GameObject youDead;

    public GameObject miniMap;

    public TextMeshProUGUI txtTime;

    public TextMeshProUGUI txtHp;

    public float timesSeconds;

    public int timeMinutes;

    public GameObject tamban;

    public GameObject caidat;

    public Point point;

    float timeUseMedkit;

    bool useMedkit;

    public TextMeshProUGUI textMedkit;

    void Start()
    {
        txtTime.text = "" + timeMinutes + " : " + timesSeconds;
        textMedkit.text = "" + playerController.medKit;
    }

    void Update()
    {
        Health();

        Mana();

        UseMedkit();
        
        txtTime.text = "" + timeMinutes + " : " + Mathf.Round(timesSeconds).ToString();
    }

    public void UseMedkit()
    {
        if(Input.GetKeyDown(KeyCode.L) && playerController.medKit > 0 && health != 100)
        {
            useMedkit = true;
        }

        if(useMedkit)
        {
            objUseMedkit.SetActive(true);

            timeUseMedkit += Time.deltaTime;

            sliderUseMedkit.value = timeUseMedkit;

            txtUseMedkit.text = (float)System.Math.Round(timeUseMedkit, 1) + " s";
            
            if(timeUseMedkit >= 3f)
            {
                PlusHealth();
            }
        }
    }

    public void PlusHealth()
    {
        health += 25;

        if(health > 100)
        {
            health = 100;
        }
        playerController.medKit -= 1;
        textMedkit.text = "" + playerController.medKit;
        useMedkit = false;
        objUseMedkit.SetActive(false);
        timeUseMedkit = 0f;
    }

    public void Mana()
    {
        if(playerController.run)
        {
            mana -= Time.deltaTime * 6f;
            manaRun = true;
        }
        if(mana <= 0)
        {
            manaRun = false;
        }

        if(!manaRun && health <= 100 || !playerController.run && health <= 100)
        {
            playerController.run = false;
            mana += Time.deltaTime * 2f;
        }

        colGre = 255f /100f + mana;
        colRed = 255f / 100f * (100f - mana);

        imageMana.color = new Color((colRed / 255f),(colGre /255f), 0f, 255f);
        sliderMana.value = mana;
        mana = Mathf.Clamp(mana, 0, 100);
    }

    public void Health()
    {
        colGre = 255f /100f + health;
        colRed = 255f / 100f * (100f - health);

        imageHealth.color = new Color((colRed / 255f),(colGre /255f), 0f, 255f);
        sliderHealth.value = health;
        txtHp.text = "Hp : " + health;

        health = Mathf.Clamp(health, 0, 100);

        if(health <= 0f)
        {
            Dead();
        }

        timesSeconds -= Time.deltaTime;

        if(timesSeconds <= 0 && timeMinutes > 0 )
        {
            timesSeconds = 60f;
            timeMinutes -= 1;
        }
        if(timeMinutes <= 0 && timesSeconds <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        Time.timeScale = 0;
        point.CountZombieDead();
        tamban.SetActive(false);
        caidat.SetActive(false);
        youDead.SetActive(true);
        miniMap.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
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

    public void PlayAgain(int sceneBack)
    {
        SceneManager.LoadScene(sceneBack);
    }
    
}
