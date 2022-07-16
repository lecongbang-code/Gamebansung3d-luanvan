using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitmarkerM24 : MonoBehaviour
{
    public float timeLive;
    public float maxTimeLive;
    public GameObject hitmarkerObj;
    public AudioSource audioSource;
    public AudioClip audioClip;

    public GameObject TimeReload;
    public GameObject reloadSlider;
    public Text textReload;
    public Text textBullet;
    public Slider timeReloadSlider;
    
    bool reload;

    void Update()
    {
        SliderReload();

        if(timeLive == maxTimeLive)
        {
            hitmarkerObj.SetActive(true);
            audioSource.PlayOneShot(audioClip);
        }
        else if(timeLive == 0)
        {
            hitmarkerObj.SetActive(false);
        }
        timeLive -= Time.deltaTime;
        timeLive = Mathf.Clamp(timeLive, 0, maxTimeLive);
    }

    public void SelectTextBullet(int bullet,int totalBullet)
    {
        textBullet.text = bullet + " / " + totalBullet;
    }

    public void SliderReload()
    {
        if(reload)
        {
            timeReloadSlider.value += Time.deltaTime;
        }
        else
        {
            timeReloadSlider.value = 0;
        }
    }

    public void SelectSliderReload(bool getReload)
    {
        reload = getReload;
        reloadSlider.SetActive(reload);
    }
}
