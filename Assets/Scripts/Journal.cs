using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Journal : MonoBehaviour
{
    public GameObject[] journalPages;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pageRight()
    {
        if (journalPages[0].activeInHierarchy)
        {
            journalPages[0].SetActive(false);
            journalPages[1].SetActive(false);

            journalPages[2].SetActive(true);
            journalPages[3].SetActive(true);
        }
    }
    
    public void pageLeft()
    {
        if (journalPages[3].activeInHierarchy)
        {
            journalPages[2].SetActive(false);
            journalPages[3].SetActive(false);

            journalPages[0].SetActive(true);
            journalPages[1].SetActive(true);
        }
    }
}
