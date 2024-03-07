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
        if (PlayerPrefs.GetInt("LevelCompleted") > 0){
            // continue game
            Boolean continueButton = GUI.Button(new Rect(10, 100, 100, 45), "Continue");
            if(continueButton){
                SceneManager.LoadScene(PlayerPrefs.GetInt("LevelCompleted"));   
            }
        }
        Boolean playButton = GUI.Button(new Rect(10, 155, 100, 45), "New Game");
        // start at Level 1 and load scene
        if(playButton){
            SceneManager.LoadScene(0);
            PlayerPrefs.SetInt("LevelCompleted", 0);
        }
        // quit game
        Boolean quitButton = GUI.Button(new Rect(10, 210, 100, 45), "Quit");
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
