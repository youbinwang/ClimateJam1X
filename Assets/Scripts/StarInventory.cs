using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarInventory : MonoBehaviour
{
    // Start is called before the first frame update

    public int invstarCount; // star count for the inventory
    public Vector2 uiSpawnPos; // where we want it to spawn on the inventory
    public TextMeshProUGUI starCountUIText; // UI text for overworld
    public TextMeshProUGUI miniGameUIText; // UI text for constellation minigame

    public RectTransform screenPanel; // a rect transform determining the size of the screen so UI objects can spawn on top of it. 

    public GameObject starPrefab; // drag the draggable star prefab here 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetStarCount() // run this when refreshing inventory/add a call to this method in the interact script to update this with the UI 
    {
       invstarCount = FindObjectOfType<Interact>().starCount; // gets starCount from interact script
        // sets the text to the UI in scene
        starCountUIText.text = invstarCount.ToString();
        miniGameUIText.text = invstarCount.ToString();
    }


    // stuff that's relevant when the minigame is active
    public void createDragStarOnClick(Vector3 mousePos) // have this ON CLICK for the star in UI
    {
        uiSpawnPos = mousePos;

      if (invstarCount > 0)
        {
            // instantiates a new star to drag
            
            GameObject newStar = Instantiate(starPrefab, uiSpawnPos, Quaternion.identity, screenPanel); // the game object that gets spawned in 
            // subtracts the star count
            invstarCount--;
            FindObjectOfType<Interact>().starCount--;
        }
    }

}
