using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    // attach this script to all the characters. Handles dialogue. feel free to edit! 

    [SerializeField] private string Name; // the character's name
    [SerializeField] private DialogueObject[] BeforeQuestDialogueObjects; // Dialogue sequence before quest
    [SerializeField] private DialogueObject[] AfterQuestDialogueObjects; // Dialogue sequence after quest
    public int dialogueIndex; // index for the dialouge node IN THE ARRAY. NOT the same thing as the tracking int. 
    public int trackingInt; // int that keeps track of game state - triggers certain dialogue when this int is reached. (tracking ints are basically the quest system) 0 is BEFORE QUEST and 1 is AFTER QUEST

    public int starsReqd; // stars required for the quest to be considered "over"

    [SerializeField] private GameObject Dialogue; // SF for now in case we want to have custom textboxes but probably not lol 
    [SerializeField] private TextMeshProUGUI CharacterName;
    [SerializeField] private TextMeshProUGUI DialogueText;
    [SerializeField] private Image CharacterImage; // face image

    [SerializeField] public String EncounterSound;
    [SerializeField] public String TalkingSound;

    // the "next button" for the dialogue
    // public Button nextButton;
    public bool dialogueActive;

    // What?
    private GameObject player;

    //public TextMeshProUGUI[] journalEntries;
    [SerializeField] private Journal journal;
    public GameObject entry; //entry to be enabled
    private int journalIndex; //finds index of entry in the journal array

    void Start()
    {
        dialogueIndex = 0; // instantiate's index
        //HideDialogue();
        dialogueActive = false;
        trackingInt = 0;

        //for (int i = 0; i < journalEntries.Length; i++)
        //{
        //    journalEntries[i].enabled = false;
        //}

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        NextState();
    }

    public void QuestDone() //things to do after the character's quest is complete
    {
        var interact = FindObjectOfType<Interact>();
        interact.constellationUI.gameObject.SetActive(true);
        interact.closeButton.gameObject.SetActive(true);
        interact.panel.gameObject.SetActive(true);
        trackingInt = 1; // switches the tracking int to 1a
        //if(journalEntries.Length > 0)
        //{
        //    for (int i = 0; i < journalEntries.Length; i++)
        //    {
        //        journalEntries[i].enabled = true;
        //    }
        //}

        Debug.Log("quest updated"); //file pushing
    }

    public void ShowDialogue() // called by farah's dialogue function. 
    {
        dialogueIndex = 0; 
        PlayDialogue();
        Dialogue.gameObject.SetActive(true);
        dialogueActive = true;
        FindObjectOfType<AudioManager>().Play(EncounterSound); // plays the encounter sound 
    }

    public void PlayDialogue() // played once the DIALOGUE function is called
    {
        CharacterName.text = Name; // sets the name 
        
        if (trackingInt == 0 && dialogueIndex >= BeforeQuestDialogueObjects.Length || trackingInt == 1 && dialogueIndex >= AfterQuestDialogueObjects.Length)
        {
            HideDialogue() ;
        }
        else if (trackingInt == 0)
        {
            DialogueText.text = BeforeQuestDialogueObjects[dialogueIndex].GetNodeText(); // gets the text of the dialogue at the index
            CharacterName.text = BeforeQuestDialogueObjects[dialogueIndex].GetCharName(); // gets the character name for the dialouge
            CharacterImage.sprite = BeforeQuestDialogueObjects[dialogueIndex].GetCharImage(); // gets the characters' image
        }
        else if (trackingInt == 1)
        {
            DialogueText.text = AfterQuestDialogueObjects[dialogueIndex].GetNodeText(); // gets the text of the dialogue at the index
            CharacterName.text = AfterQuestDialogueObjects[dialogueIndex].GetCharName(); // gets the character name for the dialouge
            CharacterImage.sprite = AfterQuestDialogueObjects[dialogueIndex].GetCharImage(); 
        }
        // later i can add a typewriter text thing if needed!
    }

    public void NextState()
    { 
        if (dialogueActive)
        {
            if (dialogueActive && Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<AudioManager>().Play(TalkingSound); // plays the talking sound 
                Debug.Log("Played talking sound");
                if (trackingInt == 0 && dialogueIndex <= BeforeQuestDialogueObjects.Length || trackingInt == 1 && dialogueIndex <= AfterQuestDialogueObjects.Length)
                {
                    dialogueIndex++; // increases the index 
                    PlayDialogue();
                    
                }
                else
                {
                    dialogueIndex = 0; // resets dialogue to 0 
                    HideDialogue();
                }
            }
        }
    }

    public void HideDialogue() // called once it reaches the end of a sequence 
    {
        dialogueActive = false; 
        Dialogue.gameObject.SetActive(false);
        dialogueIndex = 0;
        var interact = FindObjectOfType<Interact>();
        if (interact != null)
        {
            interact.EndDialogue();
        }
        FindObjectOfType<AudioManager>().Stop(EncounterSound); // stops the encounter sound 
    }

    public void UpdateJournal()
    {
       journalIndex = System.Array.IndexOf(journal.journalPages, entry);
        journal.pageAvailable[journalIndex] = true;

        if(journalIndex  % 2 == 0 || journalIndex == 0) //checking if the index is even or 0 (odd-numbered pages)
        {
            for(int i = 0; i < journal.journalPages.Length; i++)
            {
                journal.journalPages[i].SetActive(false);
            }

            journal.journalPages[journalIndex].SetActive(true);

            if (journal.pageAvailable[journalIndex + 1])
            {
                journal.journalPages[journalIndex + 1].SetActive(true);
            }
        }

        else if (journalIndex % 2 == 1) //checking if the index is odd (even numbered pages)
        {
            for (int i = 0; i < journal.journalPages.Length; i++)
            {
                journal.journalPages[i].SetActive(false);
            }

            journal.journalPages[journalIndex].SetActive(true);

            if (journal.pageAvailable[journalIndex - 1])
            {
                journal.journalPages[journalIndex - 1].SetActive(true);
                Debug.Log("setting previous page active");
            }
        }
    }
   
}
