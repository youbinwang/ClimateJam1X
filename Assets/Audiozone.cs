using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Audiozone : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public string audioStartName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void zonePassThru()
    {
        FindObjectOfType<AudioManager>().Stop(FindObjectOfType<AudioManager>().currentAudio); // stops current audio
        FindObjectOfType<AudioManager>().Play(audioStartName);
        FindObjectOfType<AudioManager>().currentAudio = audioStartName;
    }
}
