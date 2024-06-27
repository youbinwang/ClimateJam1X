using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{

    public string name;

    public bool loop;

    public AudioClip clip;

    [Range(0f, 3f)]
    public float volume;
    [Range(0f, 7f)]
    public float pitch;


    [HideInInspector]
    public AudioSource source;

}
