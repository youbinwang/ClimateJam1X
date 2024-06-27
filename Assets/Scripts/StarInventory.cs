using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarInventory : MonoBehaviour
{
    // Start is called before the first frame update

    public int invstarCount; // star count for the inventory
    public Vector2 UIspawnPos; // where we want it to spawn on the inventory
    public TextMeshProUGUI starCountUItext; // UI text for overworld
    public TextMeshProUGUI minigameUItext; // UI text for constellation minigame

    public RectTransform screenPanel; // a rect transform determining the size of the screen so UI objects can spawn on top of it. 

    public GameObject starPrefab; // drag the draggable star prefab here 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getStarCount() // run this when refreshing inventory/add a call to this method in the interact script to update this with the UI 
    {
       invstarCount = FindObjectOfType<interact>().starCount; // gets starCount from interact script
        // sets the text to the UI in scene
       starCountUItext.text = invstarCount.ToString();
       minigameUItext.text = invstarCount.ToString();
    }


    // stuff that's relevant when the minigame is active
    public void createDragStarOnClick() // have this ON CLICK for the star prefab
    {
      if (invstarCount > 0)
        {
            // instantiates a new star to drag
            
            GameObject newStar = Instantiate(starPrefab, UIspawnPos, Quaternion.identity, screenPanel); // the game object that gets spawned in 
            // subtracts the star count
            invstarCount--;
            FindObjectOfType<interact>().starCount--;
        }
    }

}
