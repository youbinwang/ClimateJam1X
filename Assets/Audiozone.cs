using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiozone : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public string[] audioStartName;
    [SerializeField] public string[] audioEndName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void zonePassThru()
    {
        Debug.Log("Audio Zone Passed through");

        for (int i=0 ; i<audioEndName.Length; i++)
        {
            FindObjectOfType<AudioManager>().StopFade(audioEndName[i]);
        }

        for (int i = 0; i < audioStartName.Length; i++) {
            FindObjectOfType<AudioManager>().PlayFade(audioStartName[i]);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        zonePassThru();
    }
}
