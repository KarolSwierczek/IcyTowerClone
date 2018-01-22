using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script displays the high score stored in PlayerPrefs.
//The scores are set on player death by PlayerStats script.
public class HighScoreUpdate : MonoBehaviour {

    public Text score;

	void Start () {
        try
        {
            score.text =
                "Best Score: " + PlayerPrefs.GetInt("score").ToString() +
                "\nMost platforms climbed: " + PlayerPrefs.GetInt("platformNumber").ToString() +
                "\nHighest Combo: " + PlayerPrefs.GetInt("hiCombo").ToString() +
                "\nYou fell " + PlayerPrefs.GetInt("deathCount").ToString() + " times";
        }
        catch (KeyNotFoundException)
        {
            score.text = "No high scores";
        }
    }
}
