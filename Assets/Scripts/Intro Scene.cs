using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour
{
    [SerializeField] private DialogueObject[] Looking; // Thalia's dialogue that plays while she looks through the telescope to a blank sky
    [SerializeField] private DialogueObject[] AfterLooking; // Thalia's dialogue after seeing the empty sky, discussing the book
    [SerializeField] private DialogueObject[] PromptTown; //Thalia's dialogue prompting the player to go to town. Plays while the player looks at the town through the telescope, may have the main camera turn back on for the last line so the player really knows to go left, but thats a problem for tomorrow Farah to figure out

    public int dialogueIndex; // index for the dialouge node IN THE ARRAY. NOT the same thing as the tracking int. 
    public int trackingInt; // int that keeps track of game state - triggers certain dialogue when this int is reached. (tracking ints are basically the quest system) 0 is BEFORE QUEST and 1 is AFTER QUEST

    [SerializeField] private GameObject Dialogue; // SF for now in case we want to have custom textboxes but probably not lol 
    [SerializeField] private TextMeshProUGUI CharacterName;
    [SerializeField] private TextMeshProUGUI DialogueText;
    [SerializeField] private Image CharacterImage; // face image

    [SerializeField] private Sprite ThaliaFace;

    [SerializeField] public String TalkingSound;

    public bool dialogueActive;
    void Start()
    {
        dialogueIndex = 0; // instantiate's index
        HideDialogue();
        dialogueActive = false;
        trackingInt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        NextState();
    }

    public void ShowDialogue() // called by farah's dialogue function. 
    {
        dialogueIndex++;
        PlayDialogue();
        Dialogue.gameObject.SetActive(true);
        dialogueActive = true;
    }

    public void PlayDialogue() // played once the DIALOGUE function is called
    {
        CharacterName.text = "Thalia"; // sets the name 
        CharacterImage.sprite = ThaliaFace;

        if (trackingInt == 0 && dialogueIndex >= Looking.Length || trackingInt == 1 && dialogueIndex >= AfterLooking.Length || trackingInt == 2 && dialogueIndex >= PromptTown.Length)
        {
            HideDialogue();
        }
        else if (trackingInt == 0)
        {
            DialogueText.text = Looking[dialogueIndex].GetNodeText(); // gets the text of the dialogue at the index
        }
        else if (trackingInt == 1)
        {
            DialogueText.text = AfterLooking[dialogueIndex].GetNodeText(); // gets the text of the dialogue at the index
        }
        else if (trackingInt == 2)
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
                if (trackingInt == 0 && dialogueIndex <= Looking.Length || trackingInt == 1 && dialogueIndex <= AfterLooking.Length || trackingInt == 2 && dialogueIndex <= PromptTown.Length)
                {
                    dialogueIndex++; // increases the index 
                    PlayDialogue();

                }
                else
                {
                    dialogueIndex = 0; // resets dialogue to 0 
                    HideDialogue(); // i need to change this so each set of dialogue plays in a sequence timed with the telescope and stuff but my brain needs to be functioning properly for me to do that so this is my reminder to myself for wednesday
                }
            }
        }
    }

    public void HideDialogue() // called once it reaches the end of a sequence 
    {
        Dialogue.gameObject.SetActive(false);
        dialogueIndex = 0;
        var interact = FindObjectOfType<Interact>();
        if (interact != null)
        {
            interact.EndDialogue();
        }
    }
}
