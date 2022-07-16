using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioMoveMent : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClip = default;

    [SerializeField] AudioClip[] RunaudioClip = default;

    public AudioSource audioSource;

    public PlayerController playerController;

    public void Step()
    {
        if(playerController.run)
        {
            AudioClip clip = RunGetRandomClip();
            audioSource.PlayOneShot(clip);
        }
        else
        {
            AudioClip clip = WalkGetRandomClip();
            audioSource.PlayOneShot(clip);
        } 
    }

    public void StepZombieRun()
    {
        AudioClip clip = RunGetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    public void StepZombieIdel()
    {
        AudioClip clip = WalkGetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    AudioClip WalkGetRandomClip()
    {
        int index = Random.Range(0, audioClip.Length -1);
        return audioClip[index];
    }

    AudioClip RunGetRandomClip()
    {
        int index = Random.Range(0, RunaudioClip.Length -1);
        return audioClip[index];
    }
}
