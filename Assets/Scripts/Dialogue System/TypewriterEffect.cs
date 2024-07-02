using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TypewriterEffect : MonoBehaviour // https://github.com/Rantabl-Resources/Unity-Typewriter-Effect <- taken from here. 
{
    [SerializeField] public TMP_Text textObject;
    [SerializeField] private float delayBeforeStart = 1f;
    [SerializeField] private float timeBtwChars = 0.1f;
    [SerializeField] private string leadingChar = "";
    [SerializeField] private bool leadingCharBeforeDelay = false;
    public bool typing = false;

    private string _writer;

    public void Awake()
    {
        //StartTyping();
    }

    public void StartTyping()
    {
        if (textObject != null)
        {
            _writer = textObject.text;
            textObject.text = leadingCharBeforeDelay ? leadingChar : "";
            Debug.Log("Calling typing coroutine");

            StartCoroutine(nameof(TMPTypeWriter));
        }
        else
            textObject = GetComponent<TextMeshProUGUI>();
    }

    private IEnumerator TMPTypeWriter()
    {
        Debug.Log("Beginning typing coroutine");
        //Added this so it won't start typing until the other is finished.
        while (typing)
        {
            yield return null;
        }
        typing = true;

        yield return new WaitForSeconds(delayBeforeStart);

        foreach (var c in _writer)
        {
            if (textObject.text.Length > 0)
                textObject.text = textObject.text[..^leadingChar.Length];

            textObject.text += c;
            textObject.text += leadingChar;
            if (c.ToString() != "," || c.ToString() != ".")
            {
                yield return new WaitForSeconds(timeBtwChars);
            }
            else
            {
                yield return new WaitForSeconds(2 * timeBtwChars);
            }
        }

        if (leadingChar != "")
            textObject.text = textObject.text[..^leadingChar.Length];

        typing = false;
        Debug.Log("Finished typing");
        FindObjectOfType<AudioManager>().Stop("SpeakingWaste");
        FindObjectOfType<AudioManager>().Stop("SpeakingObs");
    }
}