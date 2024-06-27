using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    // attach this script to all the characters. Handles dialogue. feel free to edit! 

    [SerializeField] public string Name; // the character's name
    [SerializeField] public DialogueObject[] BeforeQuestDialogueObjects; // Dialogue sequence before quest
    [SerializeField] public DialogueObject[] AfterQuestDialogueObjects; // Dialogue sequence after quest
    public int dialogueIndex; // index for the dialouge node IN THE ARRAY. NOT the same thing as the tracking int. 
    public int trackingInt; // int that keeps track of game state - triggers certain dialogue when this int is reached. (tracking ints are basically the quest system) 0 is BEFORE QUEST and 1 is AFTER QUEST

    [SerializeField] public GameObject Dialogue; // SF for now in case we want to have custom textboxes but probably not lol 
    [SerializeField] public TextMeshProUGUI CharacterName;
    [SerializeField] public TextMeshProUGUI DialogueText; 

    void Start()
    {
        trackingInt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void questDone() //things to do after the character's quest is complete
    {
        trackingInt = 1; // switches the tracking int to 1
    }

    public void showDialogue() // called by farah's dialogue function. 
    {
        dialogueIndex = 0; 
        playDialogue();
        Dialogue.gameObject.SetActive(true);
        
    }


    public void playDialogue() // played once the DIALOGUE function is called
    {
        CharacterName.text = Name; // sets the name 
        if (trackingInt == 0)
        {
            DialogueText.text = BeforeQuestDialogueObjects[dialogueIndex].getNodeText(); // gets the text of the dialogue at the index
        } else if (trackingInt == 1)
        {
            DialogueText.text = AfterQuestDialogueObjects[dialogueIndex].getNodeText(); // gets the text of the dialogue at the index
        }
        // later i can add a typewriter text thing if needed! 
    }

    public void nextButton() // set this function to play when the button on the dialogue is clicked. 
    {
        if (trackingInt == 0 && dialogueIndex <= BeforeQuestDialogueObjects.Length || trackingInt == 1 && dialogueIndex <=AfterQuestDialogueObjects.Length) {
            dialogueIndex++; // increases the index 
        }
        else
        {
            dialogueIndex = 0; // resets dialogue to 0 
            hideDialouge();
        }
        
        
    }

    public void hideDialouge() // called once it reaches the end of a sequence 
    {
        Dialogue.gameObject.SetActive(false);
    }
}
