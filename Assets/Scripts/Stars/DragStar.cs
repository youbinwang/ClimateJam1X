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
    private bool isDraggable;

    [SerializeField] private bool isTelescope;

    // Start is called before the first frame update
    void Start()
    {
        isDraggable = true;
        star = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData) // Allows dragging around the node.
    {
        if (isDraggable)
        {
            transform.position = new Vector2(
            Input.mousePosition.x,
            Input.mousePosition.y) - offset;
            isDragging = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isDraggable)
        {
            star.transform.SetAsLastSibling();
            offset = new Vector2();
            //To make it so the draggables don't center on the mouse;
            offset = Input.mousePosition - transform.position;
            //Put this after as it for some reason breaks the transform code; I guess because it has to do a lot of searching before
            // the target?
        }

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
            FindObjectOfType<AudioManager>().Play("Bong");
            isDraggable = false;

            if(isTelescope)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<IntroScene>().ShowDialogue();
            }
            else
            {
                other.gameObject.GetComponent<StarObject>().ShowConnections(); // calls starobject (the star socket object) and has it show the connections once it's snapped
                FindObjectOfType<AudioManager>().Play("Bong");
                Debug.Log("Connections Shown");
            }
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (!isDragging)
        {
            star.transform.position = other.transform.position; //star snaps to the empty position
            Debug.Log("Snapped");
            
            isDraggable = false;

            if (isTelescope)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<IntroScene>().telescopeInstructions.gameObject.SetActive(false);
                player.GetComponent<IntroScene>().ShowDialogue();
            }
            else
            {
                other.gameObject.GetComponent<StarObject>().ShowConnections(); // calls starobject (the star socket object) and has it show the connections once it's snapped
                FindObjectOfType<AudioManager>().Play("Bong");
                Debug.Log("Connections Shown");
            }
        }
    }

}
