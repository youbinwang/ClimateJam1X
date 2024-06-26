using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Streetlight : MonoBehaviour
{
    private GameObject star;
    private GameObject streetLight;
    private GameObject light;
    
    GameObject FindInChildren(GameObject parent, string name)
    {
        foreach (Transform child in parent.transform)
        {
            if (child.gameObject.name == name)
            {
                return child.gameObject;
            }
            GameObject result = FindInChildren(child.gameObject, name);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }
    
    void Start()
    {
        star = FindInChildren(gameObject, "Star");
        streetLight = FindInChildren(gameObject, "Streetlight Mesh");
        light = FindInChildren(gameObject, "Light");
    }
    
    void Update()
    {
        // When star hasn't been collect
        if (star != null && star.activeSelf)
        {
            
        }
        
        // When Stars has been collect
        if (star != null && !star.activeSelf)
        {
            LightOff();
        }
    }

    void LightOff()
    {
        Debug.Log("Light Off Method");
        light.SetActive(false);
    }
}
