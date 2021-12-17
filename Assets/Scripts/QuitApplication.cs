using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{   
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Shuttering down your game");
            Application.Quit();
        }
    }
}
