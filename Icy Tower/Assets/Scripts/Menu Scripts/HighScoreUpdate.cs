using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreUpdate : MonoBehaviour {

    public Text score;

	void Start () {
        //score = GetComponent<Text>();
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
