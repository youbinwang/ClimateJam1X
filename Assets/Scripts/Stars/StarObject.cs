using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarObject : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public GameObject[] connections; // array of connections to set active when dragged into place

    public void ShowConnections() // shows the connections once the star is snapped
    {
        for (int i = 0; i < connections.Length; i++)
        {
            connections[i].gameObject.SetActive(true);
            Debug.Log("Setting connection " + i + "active");

        }
    }
}
