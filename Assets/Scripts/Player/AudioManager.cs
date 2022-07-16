using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    bool audioStart = true;
    float timeStart = 0f;
    public float timeLoadStart = 0f;

    void Awake() 
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch =s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }   
    }

    void Update() 
    {
        if(audioStart)
        {
            timeStart += Time.deltaTime;
        }
        
        if(timeStart > timeLoadStart)
        {
            Play("Changegun");
            audioStart = false;
            timeStart = 0f;
        }
    }

    public void AudioOnGun()
    {
        audioStart = true;
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
}
