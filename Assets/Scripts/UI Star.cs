using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIStar : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        FindObjectOfType<StarInventory>().createDragStarOnClick(Input.mousePosition); // passes mouse position to the spawner to spawn a star at click
    }
}
