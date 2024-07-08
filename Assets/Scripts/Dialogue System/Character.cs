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

    [SerializeField] public Button nextButton; // the "next button" for the dialogue
    public bool dialogueActive;

    public GameObject Player;

    public TextMeshProUGUI[] journalEntries;
    void Start()
    {
        dialogueIndex = 0; // instantiate's index
        HideDialouge();
        dialogueActive = false;
        trackingInt = 0;

        for (int i = 0; i < journalEntries.Length; i++)
        {
            journalEntries[i].enabled = false;
        }

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        NextState();
    }

    public void QuestDone() //things to do after the character's quest is complete
    {
        FindObjectOfType<Interact>().constellationUI.gameObject.SetActive(true);
        FindObjectOfType<Interact>().closeButton.gameObject.SetActive(true);
        FindObjectOfType<Interact>().panel.gameObject.SetActive(true);
        trackingInt = 1; // switches the tracking int to 1a
        if(journalEntries.Length > 0)
        {
            for (int i = 0; i < journalEntries.Length; i++)
            {
                journalEntries[i].enabled = true;
            }
        }

        Debug.Log("quest updated"); //file pushing
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
        CharacterName.text = Name; // sets the name 
        if (trackingInt == 0 && dialogueIndex >= BeforeQuestDialogueObjects.Length || trackingInt == 1 && dialogueIndex >= AfterQuestDialogueObjects.Length)
        {
            HideDialouge() ;
        }else if (trackingInt == 0)
        {
            DialogueText.text = BeforeQuestDialogueObjects[dialogueIndex].GetNodeText(); // gets the text of the dialogue at the index
            CharacterName.text = BeforeQuestDialogueObjects[dialogueIndex].GetCharName(); // gets the character name for the dialouge
            CharacterImage.sprite = BeforeQuestDialogueObjects[dialogueIndex].GetCharImage(); // gets the characters' image
        } else if (trackingInt == 1)
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
            if (Input.GetMouseButtonDown(0))
            {
                if (trackingInt == 0 && dialogueIndex <= BeforeQuestDialogueObjects.Length || trackingInt == 1 && dialogueIndex <= AfterQuestDialogueObjects.Length)
                {
                    dialogueIndex++; // increases the index 
                    PlayDialogue();
                }
                else
                {
                    dialogueIndex = 0; // resets dialogue to 0 
                    HideDialouge();
                }
            }
        }
    }

    public void HideDialouge() // called once it reaches the end of a sequence 
    {
        Dialogue.gameObject.SetActive(false);
        dialogueIndex = 0;
        FindObjectOfType<Interact>().EndDialogue();
    }
}
