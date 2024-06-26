using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interact : MonoBehaviour
{
    int starCount;

    // Start is called before the first frame update
    void Start()
    {
        starCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (other.gameObject.CompareTag("NPC"))
            {
                dialogue();
            }

            if (other.gameObject.CompareTag("Star"))
            {
                collectStar();
                other.gameObject.SetActive(false);
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (other.gameObject.CompareTag("NPC"))
            {
                dialogue();
            }

            if (other.gameObject.CompareTag("Star"))
            {
                collectStar();
                other.gameObject.SetActive(false);
            }
        }
    }

    private void dialogue()
    {
        Debug.Log("Dialogue triggered.");
    }

    void collectStar()
    {
        starCount++;
        Debug.Log("Star Count: " + starCount);
    }
}
