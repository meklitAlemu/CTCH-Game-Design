using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // score counters/variables
    public int currentScore;
    public int highScore;

    // level counters/variables
    public int currentLevel = 0;
    public int unlockedLevel;

    // timers variables
    public float startTime;
    private string currentTime;
    public Rect timerRect;

    // GUI SKI
    public GUISkin skin;
    public Color warningColorTimer;
    public Color defaultColorTimer;

    // Wisp counter / variables
    public int wispCount = 0;
    private int totalWispCount;

    //References
    public GameObject wispParent;

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
        // Keep track of all our wispssss
        totalWispCount = wispParent.transform.childCount;
        // Check current level, and load it
        if(PlayerPrefs.GetInt("LevelCompleted") > 0){
            currentLevel = PlayerPrefs.GetInt("LevelCompleted");
        }else{
            currentLevel = 0;        
        }
    }

    // Method to save player's progress
    public void SavePlayerProgress()
    {
        PlayerPrefs.SetInt("LevelCompleted", currentLevel);
        PlayerPrefs.SetInt("CurrentScore", currentScore);
    }

    // Method to load player's progress
    public void LoadPlayerProgress()
    {
        currentLevel = PlayerPrefs.GetInt("LevelCompleted");
        currentScore = PlayerPrefs.GetInt("CurrentScore");
    }
    public void CompleteLevel(){
        if (currentLevel < 1){
            currentLevel += 1;
            SavePlayerProgress();
            // PlayerPrefs.SetInt("Level " + currentLevel.ToString() + " score ", currentScore);
            // can pass string name or ID: loads level we pass to it
            SceneManager.LoadScene(currentLevel);
        }
        else{
            print ("You Win!");
        }
    }
    public void AddWisp(){
        wispCount++;
    }
    void OnGUI(){
        // Create / attach our GUI in materials folder
        GUI.skin = skin;

        // if(startTime < 5f){
        //     skin.GetStyle("Timer").normal.textColor = warningColorTimer;
        // }
        // else{
        //     skin.GetStyle("Timer").normal.textColor = defaultColorTimer;
        // }
        // Create Timer Label and attach GUI style created
        GUI.Label(timerRect, currentTime, skin.GetStyle("Timer"));
        // Wisp Count Label
        GUI.Label(new Rect(45, 100, 200, 200), wispCount.ToString() + " / " + totalWispCount.ToString());

    }

}
