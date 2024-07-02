using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public int starCount;
    public GameObject ePopup; // ui popup when you can press E to interact 
    [SerializeField] public GameObject constellationUI; // constellation UI 
    [SerializeField] public GameObject closeButton; // close button
    [SerializeField] public GameObject panel; //star panel
    
    
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<StarInventory>().invstarCount = starCount;
        FindObjectOfType<StarInventory>().GetStarCount();
        ePopup.SetActive(false);
        starCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        ePopup.SetActive(true);
        if (other.gameObject.CompareTag("NPC"))
        {
            if (starCount == 2)
            {
                Debug.Log("updating quest");
                other.GetComponent<Character>().QuestDone(); //so the dialogue updates
            }
        }
        if (Input.GetKey(KeyCode.E))
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
            ePopup.SetActive(false);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        
        if (Input.GetKey(KeyCode.E))
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
            ePopup.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ePopup.SetActive(false);
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

    public void CloseConstellationUI()
    {
        constellationUI.gameObject.SetActive(false);    
        closeButton.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);

    }
}
