using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GUISkin skin;
    void OnGUI(){
        GUI.skin = skin;
        GUI.Label (new Rect(10, 10, 400, 75), "Home");
        Boolean playButton = GUI.Button(new Rect(10, 100, 100, 45), "Play");
        if(playButton){
            SceneManager.LoadScene(0);
        }

        Boolean quitButton = GUI.Button(new Rect(10, 155, 100, 45), "Quit");
        if(quitButton){
            // Application.Quit();
        }
    }
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
