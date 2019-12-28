using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Sound[] sounds;

    private void Awake()
    {
        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
        }
    }

    public void Play(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if(name == sounds[i].name)
            {
                Sound sound = sounds[i];
                sound.source.Play();
                break;
            }
        }
    }
}
