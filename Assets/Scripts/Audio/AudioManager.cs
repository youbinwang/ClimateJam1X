using UnityEngine.Audio;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Collections;

/* basically with this you've got to import sounds like you'd usually import sound but! you can add them to an array and then tell the audio manager 
 
when to play them. It goes with the sound script, which is a scriptable object i'll send below. */
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public String currentAudio;

    public static AudioManager instance;

    void Start()
    {
    
        Play("Harp");
        
        // what the above does is basically plays something all the way through. 
    }

    void Awake() // like start but it happens before. idk man
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }


    public void StartFadeOutAll()
    {
        foreach (Sound sound in sounds)
        {
            StopFade(sound.name);
        }
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            Debug.Log("Playing sound: " + s);
        }
        s.source.Play();
    }

    private IEnumerator FadeOut(String name)
    {
        Debug.Log("Fading Out");
        Sound s = Array.Find(sounds, sound => sound.name == name);
        float speed = 0.005f;
        while (s.volume > 0)
        {
            s.source.volume -= speed;
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("Done Fading Out");
    }

    private IEnumerator FadeIn(String name)
    {
        Debug.Log("Fading In");
        Sound s = Array.Find(sounds, sound => sound.name == name);
        float speed = 0.005f;
        while (s.volume < 1)
        {
            s.source.volume += speed;
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("Done Fading In");
    }


    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            Debug.Log("Pausing sound: " + s);
            s.source.Pause();
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            Debug.Log("Stopping sound: " + s);
            s.source.Stop();
        }
    }


    public void PlayFade(string name)
    {
        Play(name);
        StartCoroutine(FadeIn(name));
    }


    public void StopFade(string name)
    {
        Stop(name);
        StartCoroutine(FadeOut(name));
    }
}