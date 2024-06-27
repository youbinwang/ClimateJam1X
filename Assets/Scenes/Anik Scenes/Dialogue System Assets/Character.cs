using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    // attach this script to all the characters. Handles dialogue. feel free to edit! 

    [SerializeField] private string Name; // the character's name
    [SerializeField] private DialogueObject[] BeforeQuestDialogueObjects; // Dialogue sequence before quest
    [SerializeField] private DialogueObject[] AfterQuestDialogueObjects; // Dialogue sequence after quest
    public int dialogueIndex; // index for the dialouge node IN THE ARRAY. NOT the same thing as the tracking int. 
    public int trackingInt; // int that keeps track of game state - triggers certain dialogue when this int is reached. (tracking ints are basically the quest system) 0 is BEFORE QUEST and 1 is AFTER QUEST

    [SerializeField] private GameObject Dialogue; // SF for now in case we want to have custom textboxes but probably not lol 
    [SerializeField] private TextMeshProUGUI CharacterName;
    [SerializeField] private TextMeshProUGUI DialogueText; 

    void Start()
    {
        HideDialouge();
        trackingInt = 0;
    }

    public void QuestDone() //things to do after the character's quest is complete
    {
        trackingInt = 1; // switches the tracking int to 1a
    }

    public void ShowDialogue() // called by farah's dialogue function. 
    {
        dialogueIndex = 0; 
        PlayDialogue();
        Dialogue.gameObject.SetActive(true);
        
    }


    public void PlayDialogue() // played once the DIALOGUE function is called
    {
        CharacterName.text = Name; // sets the name 
        if (trackingInt == 0)
        {
            DialogueText.text = BeforeQuestDialogueObjects[dialogueIndex].GetNodeText(); // gets the text of the dialogue at the index
        } else if (trackingInt == 1)
        {
            DialogueText.text = AfterQuestDialogueObjects[dialogueIndex].GetNodeText(); // gets the text of the dialogue at the index
        }
        // later i can add a typewriter text thing if needed!
    }

    public void NextButton() // set this function to play when the button on the dialogue is clicked. 
    {
        if (trackingInt == 0 && dialogueIndex <= BeforeQuestDialogueObjects.Length || trackingInt == 1 && dialogueIndex <=AfterQuestDialogueObjects.Length) {
            dialogueIndex++; // increases the index 
        }
        else
        {
            dialogueIndex = 0; // resets dialogue to 0 
            HideDialouge();
        }
        
        
    }

    public void HideDialouge() // called once it reaches the end of a sequence 
    {
        Dialogue.gameObject.SetActive(false);
    }
}
