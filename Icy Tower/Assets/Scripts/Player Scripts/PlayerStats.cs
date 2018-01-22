using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script keeps track of current platform number, score and combo. 
//On falling down it updates the high score stored in PlayerPrefs.

public class PlayerStats : MonoBehaviour {

    public Font StatsFont;

    private int platformNumber = 0;
    private int score = 0;
    private int combo = 0; //Number of consecutive super jumps
    private int hiCombo = 0; //The highest super jump combo on this run.
    private GUIStyle myStyle = new GUIStyle();

    private void Start()
    {
        myStyle.font = StatsFont;
        myStyle.fontSize = 50;
        myStyle.normal.textColor = new Color(0.082f, 0.276f, 0.086f, 1.0f);
    }


    //Score is calculated here and the platform number is updated based on y position of platform.
    private void OnCollisionEnter(Collision collision)
    {
        int newPlatformNumber = (int)collision.transform.position.y / 4;
        score += (newPlatformNumber - platformNumber) * (combo + 1);
        platformNumber = newPlatformNumber;
    }


    //This functions increments the combo counter and updates hiCombo.
    //isSuperJumping is passed from Controller script.
    //Combo is broken on a normal jump.
    //Combo affects the score gain per platform climbed.
    public void JumpCounter(bool isSuperJumping)
    {
        if (isSuperJumping)
        {
            combo += 1;
            if(combo > hiCombo)
            {
                hiCombo = combo;
            }
        }
        else
        {
            combo = 0;
        }
    }


    //This function is called from Death script.
    public int[] OnDeath()
    {
        saveScore(score, platformNumber, hiCombo);
        return new int[] { score, platformNumber, hiCombo };
    }


    //This function updates the highscore stored in PlayerPrefs. 
    //If there's no score recorded a key is created first.
    private void saveScore(int score, int platformNumber, int hiCombo)
    {
        if(!PlayerPrefs.HasKey("score") || PlayerPrefs.GetInt("score") < score)
        {
            PlayerPrefs.SetInt("score", score);
        }
        if (!PlayerPrefs.HasKey("platformNumber") || PlayerPrefs.GetInt("platformNumber") < platformNumber)
        {
            PlayerPrefs.SetInt("platformNumber", platformNumber);
        }
        if (!PlayerPrefs.HasKey("hiCombo") || PlayerPrefs.GetInt("hiCombo") < hiCombo)
        {
            PlayerPrefs.SetInt("hiCombo", hiCombo);
        }

        if (!PlayerPrefs.HasKey("deathCount"))
        {
            PlayerPrefs.SetInt("deathCount", 1);
        }
        else
        {
            PlayerPrefs.SetInt("deathCount", PlayerPrefs.GetInt("deathCount") + 1);
        }
    }


    //Displays stats in real time during gameplay.
    private void OnGUI()
    {
        GUI.Label(new Rect(50, Screen.height - 200, 300, 100), "Platform: " + platformNumber.ToString(), myStyle);
        //GUI.Label(new Rect(10, 30, 120, 20), "Combo: " + combo.ToString());
        GUI.Label(new Rect(50, Screen.height - 100, 300, 100), "SCORE: " + score.ToString(), myStyle);
    }
}
