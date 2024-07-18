using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour
{
    [SerializeField] private DialogueObject[] Prologue; //Thalia's dialogue outside the observatory
    
    [SerializeField] private DialogueObject[] Entered; //Thalia's dialogue that plays right at the beginning prompting her to look for the book
    [SerializeField] private DialogueObject[] Looking; // Thalia's dialogue that plays while she looks at the book
    [SerializeField] private DialogueObject[] AfterLooking; // Thalia's dialogue after seeing the empty sky, discussing the book
    [SerializeField] private DialogueObject[] PromptTown; //Thalia's dialogue prompting the player to go to town

    public int dialogueIndex; // index for the dialouge node IN THE ARRAY. NOT the same thing as the tracking int. 
    public int trackingInt; // int that keeps track of game state - triggers certain dialogue when this int is reached. (tracking ints are basically the quest system) 0 is BEFORE QUEST and 1 is AFTER QUEST

    [SerializeField] private GameObject Dialogue; // SF for now in case we want to have custom textboxes but probably not lol 
    [SerializeField] private TextMeshProUGUI CharacterName;
    [SerializeField] private TextMeshProUGUI DialogueText;
    [SerializeField] private Image CharacterImage; // face image

    [SerializeField] private Sprite ThaliaFace;

    [SerializeField] public String TalkingSound;

    [SerializeField] private GameObject starUI;

    [SerializeField] private GameObject telescopeTrigger;
    [SerializeField] private Image bookUI;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera telescopeCamera;

    public bool dialogueActive;
    void Start()
    {
        dialogueIndex = 0; // instantiate's index
        HideDialogue();
        dialogueActive = false;
        trackingInt = 0;
        telescopeTrigger.SetActive(false);
        this.gameObject.GetComponent<CharacterMovement>().canMove = false;

        starUI.SetActive(false);

        ShowDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        NextState();
    }

    public void ShowDialogue() // called by farah's dialogue function. 
    {
        dialogueIndex = 0;
        PlayDialogue();
        Dialogue.gameObject.SetActive(true);
        dialogueActive = true;
    }

    public void PlayDialogue() // played once the DIALOGUE function is called
    {
        CharacterName.text = "Thalia"; // sets the name 
        CharacterImage.sprite = ThaliaFace;

        if (trackingInt == 0 && dialogueIndex >= Prologue.Length || trackingInt == 1 && dialogueIndex >= Entered.Length || trackingInt == 2 && dialogueIndex >= Looking.Length || trackingInt == 3 && dialogueIndex >= AfterLooking.Length || trackingInt == 4 && dialogueIndex >= PromptTown.Length)
        {
            HideDialogue();
        }
        else if (trackingInt == 0)
        {
            DialogueText.text = Prologue[dialogueIndex].GetNodeText();
        }
        else if (trackingInt == 1)
        {
            DialogueText.text = Entered[dialogueIndex].GetNodeText();
        }
        else if (trackingInt == 2)
        {
            DialogueText.text = Looking[dialogueIndex].GetNodeText(); // gets the text of the dialogue at the index
            if(dialogueIndex == 1)
            {
                bookUI.gameObject.SetActive(true);
            }
        }
        else if (trackingInt == 3)
        {
            DialogueText.text = AfterLooking[dialogueIndex].GetNodeText(); // gets the text of the dialogue at the index
            if(dialogueIndex == 1)
            {
                mainCamera.enabled = false;
                telescopeCamera.enabled = true;
            }
            if(dialogueIndex == 3)
            {
                bookUI.gameObject.SetActive(true);
            }
        }
        else if (trackingInt == 4)
        {
            DialogueText.text = PromptTown[dialogueIndex].GetNodeText();
        }
    }

    public void NextState()
    {
        if (dialogueActive)
        {
            if (dialogueActive && Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<AudioManager>().Play(TalkingSound); // plays the talking sound 
                Debug.Log("Played talking sound");
                if (trackingInt == 0 && dialogueIndex <= Prologue.Length || trackingInt == 1 && dialogueIndex <= Entered.Length || trackingInt == 2 && dialogueIndex <= Looking.Length || trackingInt == 3 && dialogueIndex <= AfterLooking.Length || trackingInt == 4 && dialogueIndex <= PromptTown.Length)
                {
                    dialogueIndex++; // increases the index 
                    PlayDialogue();

                }
                else
                {
                    dialogueIndex = 0; // resets dialogue to 0 

                    if(trackingInt == 2 || trackingInt == 3)
                    {
                        bookUI.gameObject.SetActive(false);
                    }
                    if(trackingInt == 4)
                    {
                        telescopeCamera.gameObject.SetActive(false);
                        mainCamera.gameObject.SetActive(true);

                        starUI.gameObject.SetActive(true);
                    }
                    HideDialogue();
                }
            }
        }
    }

    public void HideDialogue() // called once it reaches the end of a sequence 
    {
        Dialogue.gameObject.SetActive(false);
        dialogueIndex = 0;
        trackingInt++;

        this.gameObject.GetComponent<Interact>().EndDialogue();
    }
}
