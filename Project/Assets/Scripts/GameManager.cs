using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int currentScore;
    public static int highScore;

    public static int currentLevel = 0;
    public static int unlockedLevel;

    public float startTime;
    private string currentTime;

    public GUISkin skin;
    public Rect timerRect;
    void Update(){
        // Take start time and subtract a second from it. Deltatime is the length of the last frame
        startTime -= Time.deltaTime;
        currentTime = string.Format("{0:0.0}", startTime);
        if(startTime <= 0){
            startTime = 0;
            SceneManager.LoadScene(2);
        }
    }

    void Start(){
        DontDestroyOnLoad(gameObject);
    }

    public static void CompleteLevel(){
        if (currentLevel < 1){
            currentLevel += 1;
            // can pass string name or ID: loads level we pass to it
            SceneManager.LoadScene(currentLevel);
        }
        else{
            print ("You Win!");
        }
    }

    void OnGUI(){
        // Create / attach our GUI in materials folder
        GUI.skin = skin;
        // Create Timer Label and attach GUI style created
        GUI.Label(timerRect, currentTime, skin.GetStyle("Timer"));
    }
}
