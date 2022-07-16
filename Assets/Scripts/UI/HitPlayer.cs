using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    public GameObject healthRed;
    public GameObject healthone;
    public GameObject healthtow;
    public GameObject healthree;

    public InterFace interFace;
    public AudioSource audioSource;
    public AudioClip hitPlayer;

    bool isActive;
    public float timeActive = 1f;
    int objActive;

    public void Update()
    {
        if(timeActive < 1f)
        {
            timeActive += Time.deltaTime;
        }

        if(timeActive < 0.25f)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }

        healthRed.SetActive(isActive);

        if(objActive == 1)
        {
            healthone.SetActive(isActive);
        }
        if(objActive == 2)
        {
            healthtow.SetActive(isActive);
        }
         if(objActive == 3)
        {
            healthree.SetActive(isActive);
        }
    }

    public void ActiveHitPlayer(int getdamage)
    {
        timeActive = 0f;
        objActive = Random.Range(1, 4);
        interFace.health -= getdamage;
        audioSource.PlayOneShot(hitPlayer);
    }
}
