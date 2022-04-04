using UnityEngine.Audio;
using System;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    
    [Range(-.05f, 1)]
    public float globalVolume;

    public Sound[] sounds;


    void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;


        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = globalVolume < 0 ? s.volume : globalVolume;
            s.source.pitch = s.pitch;
        }
    }

    public static void Play (string name)
    {
        Sound s = Array.Find(instance.sounds, sound => sound.name == name);
        if (s != null)
            s.source.Play();
        else 
            Debug.LogWarning($"Could not find {name} in sounds");
    }

}