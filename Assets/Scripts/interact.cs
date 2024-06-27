using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
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
                Dialogue(other);
            }

            if (other.gameObject.CompareTag("Star"))
            {
                CollectStar();
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
                Dialogue(other);
            }

            if (other.gameObject.CompareTag("Star"))
            {
                CollectStar();
                other.gameObject.SetActive(false);
            }
        }
    }

    // get the dialogueScript of the object that player collided 
    private void Dialogue(Collider other)
    {
        Debug.Log("Dialogue triggered.");
        Character dialogueScript = other.gameObject.GetComponent<Character>();
        dialogueScript.ShowDialogue();
    }

    void CollectStar()
    {
        starCount++;
        Debug.Log("Star Count: " + starCount);
        FindObjectOfType<StarInventory>().GetStarCount(); // gets star count to the inventory 
    }
}
