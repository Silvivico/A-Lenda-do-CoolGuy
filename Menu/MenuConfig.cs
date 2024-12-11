using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuConfig : MonoBehaviour
{
   
    public Canvas cliente;
 

    void Update()
    {
        
        if (Input.GetKey(KeyCode.X))
        {
         cliente.enabled = false;
        }

    }
 
}
