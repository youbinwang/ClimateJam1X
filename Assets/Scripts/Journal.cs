using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Journal : MonoBehaviour
{
    public GameObject[] journalPages;
    public bool[] pageAvailable;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < pageAvailable.Length; i++)
        {
            pageAvailable[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (pageAvailable[5])
        // {
        //     pageAvailable[6] = true;
        //     pageAvailable[7] = true;
        //     pageAvailable[8] = true;
        // }
        if (pageAvailable.Length > 5 && pageAvailable[5])
        {
            if (pageAvailable.Length > 6)
            {
                pageAvailable[6] = true;
            }
            if (pageAvailable.Length > 7)
            {
                pageAvailable[7] = true;
            }
            if (pageAvailable.Length > 8)
            {
                pageAvailable[8] = true;
            }
        }
    }

    public void pageRight()
    {
        for(int i = 0;i < journalPages.Length;i++)
        {
            if(i %2 == 1) // checking if the index is odd so we don't get overlapping objects
            {
                if (journalPages[i].activeInHierarchy)
                {
                    journalPages[i].SetActive(false);

                    if (journalPages[i + 1] != null)
                    {
                        journalPages[i + 1].SetActive(false);
                    }

                    if (journalPages[i + 2] != null && pageAvailable[i + 2])
                    {
                        journalPages[i + 2].SetActive(true);
                    }

                    if (journalPages[i + 3] != null && pageAvailable[i + 3])
                    {
                        journalPages[i + 3].SetActive(true);
                    }

                }
            }
        }
    }
    
    public void pageLeft()
    {
        for( int i = 0; i < journalPages.Length; i++)
        {
            if(i%2 == 0) //checking if the index is even
            {
                if (journalPages[i].activeInHierarchy)
                {
                    journalPages[i].SetActive(false);
                    journalPages[i - 1].SetActive(false);

                    journalPages[i - 2].SetActive(true);
                    journalPages[i - 3].SetActive(true);
                }
            }
        }
        //if (journalPages[3].activeInHierarchy)
        //{
        //    journalPages[2].SetActive(false);
        //    journalPages[3].SetActive(false);

        //    journalPages[0].SetActive(true);
        //    journalPages[1].SetActive(true);
        //}
    }
}
