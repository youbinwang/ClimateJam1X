using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueNode", menuName = "Scriptable Objects/Dialogue Node")]


public class DialogueObject : ScriptableObject {
    // Start is called before the first frame update
    
    [TextArea(15, 20)]
    [SerializeField] public string nodeText; // The body text of the dialogue node 
    
    public string GetNodeText() // passes the node's text
    {
        return nodeText;
    }
    
}
