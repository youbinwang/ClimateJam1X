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
    [SerializeField] private GameObject journalUI;

    [SerializeField] private GameObject telescopeTrigger;
    [SerializeField] private GameObject bookUI;

    [SerializeField] public Camera mainCamera;
    [SerializeField] public Camera telescopeCamera;
    [SerializeField] private GameObject telescopeView;
    [SerializeField] private GameObject moveableTelescope;

    [SerializeField] private Vector3 insidePosition;

    public GameObject introColliders;

    public bool dialogueIsActive;

    public bool introDone;

    [SerializeField] public TextMeshProUGUI telescopeInstructions;

    [SerializeField] private GameObject townImage;
    [SerializeField] private GameObject endSceneImage;
    void Start()
    {
        dialogueIndex = 0; // instantiate's index
        trackingInt = 0;

        telescopeTrigger.SetActive(false);

        starUI.SetActive(false);
        journalUI.SetActive(false);

        moveableTelescope.GetComponent<DragStar>().enabled = false;

        introColliders.SetActive(true);
        introDone = false;

        ShowDialogue();
        //StartCoroutine(this.gameObject.GetComponent<Interact>().HandleDialogue(telescopeTrigger.GetComponent<Collider>()));
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
        dialogueIsActive = true;
    }

    public void PlayDialogue() // played once the DIALOGUE function is called
    {
        CharacterName.text = "Thalia";
        CharacterImage.sprite = ThaliaFace;

        if (trackingInt == 0 && dialogueIndex >= Prologue.Length || trackingInt == 1 && dialogueIndex >= Entered.Length || trackingInt == 2 && dialogueIndex >= Looking.Length || trackingInt == 3 && dialogueIndex >= AfterLooking.Length || trackingInt == 4 && dialogueIndex >= PromptTown.Length)
        {
            if (trackingInt == 0)
            {
                this.gameObject.GetComponent<Transform>().position = insidePosition;
                introDone = true;
            }

            if (trackingInt == 2)
            {
                bookUI.SetActive(false);
                telescopeTrigger.SetActive(true);
            }

            if (trackingInt == 3)
            {
                bookUI.SetActive(false);
                moveableTelescope.GetComponent<DragStar>().enabled = true;
            }
            if (trackingInt == 4)
            {
                telescopeCamera.gameObject.SetActive(false);
                telescopeView.gameObject.SetActive(false);
                mainCamera.gameObject.SetActive(true);
            }
            dialogueIndex = 0;
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
                bookUI.SetActive(true);
            }
        }
        else if (trackingInt == 3)
        {
            DialogueText.text = AfterLooking[dialogueIndex].GetNodeText(); // gets the text of the dialogue at the index
            if(dialogueIndex == 1)
            {
                mainCamera.gameObject.SetActive(false);
                telescopeCamera.gameObject.SetActive(true);
                telescopeView.SetActive(true);
            }
            if(dialogueIndex == 3)
            {
                bookUI.SetActive(true);
            }
        }
        else if (trackingInt == 4)
        {
            telescopeInstructions.gameObject.SetActive(false);
            DialogueText.text = PromptTown[dialogueIndex].GetNodeText();
        }
    }

    public void NextState()
    {
        Debug.Log("NextState Called");
        if (dialogueIsActive)
        {
            Debug.Log("dialogue working");
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
            {
                // FindObjectOfType<AudioManager>().Play(TalkingSound); // plays the talking sound 
                Debug.Log("Played talking sound");
                if ((trackingInt == 0 && dialogueIndex <= Prologue.Length) || (trackingInt == 1 && dialogueIndex <= Entered.Length) || (trackingInt == 2 && dialogueIndex <= Looking.Length) || (trackingInt == 3 && dialogueIndex <= AfterLooking.Length) || (trackingInt == 4 && dialogueIndex <= PromptTown.Length))
                {
                    dialogueIndex++; // increases the index 
                    PlayDialogue();

                }
                else
                {
                    dialogueIndex = 0; // resets dialogue to 0 


                    //if(trackingInt == 0)
                    //{
                    //    this.gameObject.GetComponent<Transform>().position = insidePosition;
                    //    introDone = true;
                    //}

                    //if(trackingInt == 2)
                    //{
                    //    bookUI.SetActive(false);
                    //    telescopeTrigger.SetActive(true);
                    //}

                    //if(trackingInt == 3)
                    //{
                    //    bookUI.SetActive(false);
                    //    moveableTelescope.GetComponent<DragStar>().enabled = true;
                    //}
                    //if(trackingInt == 4)
                    //{
                    //    telescopeCamera.gameObject.SetActive(false);
                    //    telescopeView.gameObject.SetActive(false);
                    //    mainCamera.gameObject.SetActive(true);

                    //    starUI.SetActive(true);
                    //    journalUI.SetActive(true);
                    //    introColliders.SetActive(false);
                    //}
                    HideDialogue();
                }
            }
        }
    }

    public void HideDialogue() // called once it reaches the end of a sequence 
    {
        Dialogue.gameObject.SetActive(false);
        dialogueIsActive = false;
        dialogueIndex = 0;
        trackingInt++;
        Debug.Log("Tracking Int: " + trackingInt);

        this.gameObject.GetComponent<Interact>().EndDialogue();

        //if(trackingInt == 1)
        //{
        //    ShowDialogue();
        //}
        if(trackingInt == 4)
        {
            telescopeInstructions.gameObject.SetActive(true);
        }
        if(trackingInt > 4)
        {
            starUI.SetActive(true);
            journalUI.SetActive(true);
            introColliders.SetActive(false);

            townImage.SetActive(false);
            endSceneImage.SetActive(true);
            //this.gameObject.GetComponent<IntroScene>().enabled = false;
        }
    }
}
