using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragStar : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler
{
    public GameObject star;
    private RectTransform draggingPlane; // plane it gets dragged on? 
    private Vector2 offset;
    private bool isDragging;

    // Start is called before the first frame update
    void Start()
    {
        star = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("isDragging: " + isDragging);
    }

    public void OnDrag(PointerEventData eventData) // Allows dragging around the node.
    {
        transform.position = new Vector2(
        Input.mousePosition.x,
        Input.mousePosition.y) - offset;
        isDragging = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        star.transform.SetAsLastSibling();
        offset = new Vector2();
        //To make it so the draggables don't center on the mouse;
        offset = Input.mousePosition - transform.position;
        //Put this after as it for some reason breaks the transform code; I guess because it has to do a lot of searching before
        // the target?

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided");
        if (!isDragging) //makes sure the star doesn't snap immediately on collision
        {
            star.transform.position = other.transform.position; //star snaps to the empty position
            Debug.Log("Snapped");
            other.gameObject.GetComponent<StarObject>().ShowConnections(); // calls starobject (the star socket object) and has it show the connections once it's snapped
            Debug.Log("Connections Shown");
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (!isDragging)
        {
            star.transform.position = other.transform.position; //star snaps to the empty position
            Debug.Log("Snapped");
            other.gameObject.GetComponent<StarObject>().ShowConnections(); // calls starobject (the star socket object) and has it show the connections once it's snapped
            Debug.Log("Connections Shown");
        }
    }

}
