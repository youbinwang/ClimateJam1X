using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] private GameObject blackScreen;
    private Animator fadeAnims;

    [SerializeField] private Animator credits;

    // Start is called before the first frame update
    void Start()
    {
        fadeAnims = blackScreen.GetComponent<Animator>();
        fadeAnims.Play("fade-from-black");

        Invoke("fadeGone", 3.9f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void fadeGone()
    {
        blackScreen.SetActive(false);

        Invoke("rollCredits", 1f);
    }

    private void rollCredits()
    {
        credits.Play("credits");

        Invoke("fadeOut", 115f);
    }

    private void fadeOut()
    {
        blackScreen.SetActive(true);
        fadeAnims.Play("fade-to-black");

        Invoke("mainMenu", 4f);
    }

    private void mainMenu()
    {
        SceneManager.LoadScene("Title");
    }
}
