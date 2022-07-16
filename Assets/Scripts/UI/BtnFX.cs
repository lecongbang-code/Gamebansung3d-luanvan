using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hoverFx;
    public AudioClip clickFx;

    public void HoverSound()
    {
        audioSource.PlayOneShot(hoverFx);
    }

    public void ClickSound()
    {
        audioSource.PlayOneShot(clickFx);
    }
}
