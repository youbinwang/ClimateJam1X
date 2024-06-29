using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIStar : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {

        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        FindObjectOfType<StarInventory>().CreateDragStarOnClick(Input.mousePosition); // passes mouse position to the spawner to spawn a star at click
    }
}
