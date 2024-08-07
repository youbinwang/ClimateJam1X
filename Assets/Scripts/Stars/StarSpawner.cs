using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [SerializeField] public GameObject starPrefab; // the spawnable star prefab
    [SerializeField] public RectTransform screenPanel; // panel for stars to spawn on

    // x and y screen boundaries 
    [SerializeField] public float xBound;
    [SerializeField] public float yBound;
    public bool canSpawn;

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
    }

    public void spawnAStar()
    {
        Debug.Log("Clicked");
        if (canSpawn)
        {
            if (FindObjectOfType<Interact>().starCount > 0)
            {
                Vector2 spawnPosition = GetSpawn(); // gets the spawn position of the dialogue object at random 
                Instantiate(starPrefab, spawnPosition, Quaternion.identity, screenPanel);
                FindObjectOfType<Interact>().starCount--; // subtracts star count
                FindObjectOfType<Interact>().InventoryStars(); // substracts star count in UI text
                Debug.Log("Star Count:" + FindObjectOfType<Interact>().starCount);
                Debug.Log("Clone Created");
                FindObjectOfType<AudioManager>().Play("Click");
              
            }
        }

    }
    public Vector2 GetSpawn() // code pulled from https://stackoverflow.com/questions/47674034/spawn-image-at-random-position-in-canvas , gets the spawn position for the dialogue node 
    {


        float xPos = Random.Range(xBound, Screen.width - xBound);
        float yPos = Random.Range(yBound, Screen.height - yBound);

        Vector2 spawnPosition = new Vector2(xPos, yPos);
        //Vector2 spawnPosition = new Vector2(Mathf.Clamp(xPos,0, Screen.width), Mathf.Clamp(yPos,0,Screen.height));

        return spawnPosition;
    }

}