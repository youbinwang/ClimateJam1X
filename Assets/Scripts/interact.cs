using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interact : MonoBehaviour
{
    public int starCount;

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
                dialogue(other);
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
                dialogue(other);
            }

            if (other.gameObject.CompareTag("Star"))
            {
                collectStar();
                other.gameObject.SetActive(false);
            }
        }
    }

    // get the dialogueScript of the object that player collided 
    private void dialogue(Collider other)
    {
        Debug.Log("Dialogue triggered.");
        Character dialogueScript = other.gameObject.GetComponent<Character>();
        dialogueScript.showDialogue();
    }

    void collectStar()
    {
        starCount++;
        Debug.Log("Star Count: " + starCount);
        FindObjectOfType<StarInventory>().getStarCount(); // gets star count to the inventory 
    }
}
