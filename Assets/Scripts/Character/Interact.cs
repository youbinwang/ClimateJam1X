using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public int starCount;
    public GameObject ePopup; // ui popup when you can press E to interact 
    public GameObject constellationUI; // constellation UI 
    public GameObject closeButton; // close button
    public GameObject panel; //star panel
    public TextMeshProUGUI starCountUIText; // UI text 

    private CharacterMovement characterMovement;
    private CharacterAnimation characterAnimation;

    private Vector3 savePoint;
    private bool checkpointStored;
    
    void Start()
    {
        characterMovement = this.gameObject.GetComponent<CharacterMovement>();
        characterAnimation = FindObjectOfType<CharacterAnimation>();
        
        //FindObjectOfType<StarInventory>().invstarCount = starCount;
        //FindObjectOfType<StarInventory>().GetStarCount();
        ePopup.SetActive(false);
        starCount = 0;
        checkpointStored = false;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.R))
        {
            if(checkpointStored)
            {
                gameObject.transform.position = savePoint;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC") || other.gameObject.CompareTag("Star") || other.gameObject.CompareTag("Checkpoint") || other.gameObject.CompareTag("Book") || other.gameObject.CompareTag("Telescope"))
        {
            ePopup.SetActive(true);
        }

        if (other.gameObject.CompareTag("NPC"))
        {
            if (starCount >= other.GetComponent<Character>().starsReqd)
            {
                Debug.Log("Updating Quest");
                other.GetComponent<Character>().QuestDone(); //so the dialogue updates
            }
        }

        if (other.gameObject.CompareTag("IntroPiece"))
        {
            characterMovement.canMove = false;

            IntroScene dialogueScript = this.gameObject.GetComponent<IntroScene>();
            dialogueScript.ShowDialogue();

            ePopup.SetActive(false);
            other.gameObject.SetActive(false);
        }
        // if (Input.GetKey(KeyCode.E))
        // {
        //     if (other.gameObject.CompareTag("NPC"))
        //     {
        //         // Dialogue(other);
        //         StartCoroutine(HandleDialogue(other));
        //     }
        //
        //     if (other.gameObject.CompareTag("Star"))
        //     {
        //         // CollectStar();
        //         StartCoroutine(HandleCollectStar(other));
        //         other.gameObject.SetActive(false);
        //     }
        //     ePopup.SetActive(false);
        //  }
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (Input.GetKey(KeyCode.E))
        {
            if (other.gameObject.CompareTag("NPC"))
            {
                // Dialogue(other);
                StartCoroutine(HandleDialogue(other));
                other.GetComponent<Character>().UpdateJournal();
            }

            if (other.gameObject.CompareTag("Star"))
            {
                // CollectStar();
                StartCoroutine(HandleCollectStar(other));
                other.gameObject.SetActive(false);
            }

            if (other.gameObject.CompareTag("Checkpoint"))
            {
                savePoint = gameObject.GetComponent<Transform>().position;
                checkpointStored = true;
            }

            if (other.gameObject.CompareTag("Book") || other.gameObject.CompareTag("Telescope"))
            {
                StartCoroutine(HandleDialogue(other));
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
    // private void Dialogue(Collider other)
    // {
    //     characterMovement.canMove = false;
    //     characterMovement.InteractStopMove();
    //     characterAnimation.Interact();
    //     
    //     Character dialogueScript = other.gameObject.GetComponent<Character>();
    //     dialogueScript.ShowDialogue();
    // }

    public IEnumerator HandleDialogue(Collider other)
    {
        characterMovement.canMove = false;
        characterMovement.InteractStopMove();
        characterAnimation.Interact();
        
        yield return new WaitForSeconds(1.25f);

        characterAnimation.SetIdleState();

        if (other.CompareTag("NPC"))
        {
            Character dialogueScript = other.gameObject.GetComponent<Character>();
            dialogueScript.ShowDialogue();
        }

        if (other.CompareTag("Book") || other.CompareTag("Telescope"))
        {
            IntroScene dialogueScript = this.gameObject.GetComponent<IntroScene>();
            dialogueScript.ShowDialogue();
        }
    }

    private IEnumerator HandleCollectStar(Collider other)
    {
        characterMovement.canMove = false;
        characterMovement.InteractStopMove();
        characterAnimation.Interact();
        
        yield return new WaitForSeconds(1.25f);
        CollectStar();
        // EndDialogue();
        if (!other.gameObject.CompareTag("NPC"))
        {
            EndDialogue();
        }
    }
    
    void CollectStar()
    {
        starCount++;
        Debug.Log("Star Count: " + starCount);
        FindObjectOfType<AudioManager>().Play("Star");
        // FindObjectOfType<StarInventory>().GetStarCount(); // gets star count to the inventory 
        InventoryStars();
    }

    public void InventoryStars()
    {
        starCountUIText.text = starCount.ToString();
    }
    
    public void CloseConstellationUI()
    {
        constellationUI.SetActive(false);    
        panel.SetActive(false);
        closeButton.SetActive(false);
    }
    
    public void EndDialogue()
    {
        characterMovement.canMove = true;
        characterAnimation.EndInteraction();
    }
}
