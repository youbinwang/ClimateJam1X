using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarInventory : MonoBehaviour
{
    // Start is called before the first frame update

    public int invstarCount; // star count for the inventory
    public TextMeshProUGUI starCountUIText; // UI text 

    void Start()
    {
        starCountUIText.text = GetStarCount().ToString();
    }

    public int GetStarCount() // run this when refreshing inventory/add a call to this method in the interact script to update this with the UI 
    {
        Debug.Log("Get Star Count Got");
        invstarCount = FindObjectOfType<Interact>().starCount; // gets starCount from interact script
        // sets the text to the UI in scene
        starCountUIText.text = invstarCount.ToString();
        return invstarCount;
    }

    // stuff that's relevant when the minigame is active
 

}
