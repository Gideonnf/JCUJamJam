using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);


        // loop thru the array of sounds and assign them into diff audioSources
        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }


    }

    private void Update()
    {
       

    }
    /// <summary>
    /// Just call AudioManager.Instance.Play("The name you wrote on the inspector")
    /// </summary>
    /// <param name="name"></param>
    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound file: " + name + " not found");
            return;
        }

        s.source.Play();

    }

    public void PlayOneShot(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound file: " + name + " not found");
            return;
        }

        s.source.PlayOneShot(s.source.clip);

    }



    public void PlayLoop(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound file: " + name + " not found");
            return;
        }

        s.source.loop = true;
        s.source.Play();
    }

   

    public void PlayPitch(string name, float pitch)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound file: " + name + " not found");
            return;
        }
        s.source.pitch = pitch;

        s.source.Play();
    }

    public void Stop(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound file: " + name + " not found");
            return;
        }

        s.source.Stop();
    }

  
}
