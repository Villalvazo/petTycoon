using UnityEngine.Audio;
using System;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public Sound[] sonidos;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sonidos)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }
    private void Start()
    {
        PlayLoop("Theme");
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sonidos,sound => sound.nombre == name);
        s.source.Play();
    }
    public void PlayLoop(string name)
    {
        Sound s = Array.Find(sonidos, sound => sound.nombre == name);
        s.source.Play();
        s.source.loop = true;
    }
}
