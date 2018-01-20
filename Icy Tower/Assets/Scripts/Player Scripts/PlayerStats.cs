using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    private int platformNumber = 0;
    private int score = 0;
    private int combo = 0; //Number of consecutive super jumps
    private int hiCombo = 0; //The highest super jump combo on this run.

    private void OnCollisionEnter(Collision collision)
    {
        int newPlatformNumber = (int)collision.transform.position.y / 4;
        score += (newPlatformNumber - platformNumber) * (combo + 1);
        platformNumber = newPlatformNumber;
    }


    //This functions increments the combo counter and updates hiCombo.
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


    public int[] OnDeath()
    {
        saveScore(score, platformNumber, hiCombo);
        return new int[] { score, platformNumber, hiCombo };
    }


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


    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 120, 20), "Platform number: " + platformNumber.ToString());
        GUI.Label(new Rect(10, 30, 120, 20), "Combo: " + combo.ToString());
        GUI.Label(new Rect(10, 50, 120, 20), "Score: " + score.ToString());
    }
}
