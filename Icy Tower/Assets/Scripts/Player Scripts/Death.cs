using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script detects death, displays a death message and stops the camera from rising. 

public class Death : MonoBehaviour {
    public Texture background; //Background for the death message.

    private GameObject player;
    private CameraMovement cameraMovement;
    private int[] stats; //Array of stats passed from PlayerStats script.
    private bool dispalyStats = false;
    private GUIStyle messageStyle = new GUIStyle();
    private GUIStyle buttonStyle = new GUIStyle();

    private void Start()
    {
        cameraMovement = GetComponentInParent<CameraMovement>();
        player = GameObject.Find("Player");

        messageStyle.alignment = TextAnchor.UpperCenter;
        messageStyle.fontSize = 35;

        buttonStyle.fontSize = 20;
        buttonStyle.alignment = TextAnchor.MiddleCenter;
        buttonStyle.normal.textColor = Color.white;
    }


    //There's a trigger collider attached to the cameras. 
    //When the player enters it the collision between the player and the platforms is turned off
    //to let the player fall under the edge of the screen.
    //Camera rising is stopped and stats are pulled from PlayerStats.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), true);
            cameraMovement.SetDeath(true);
            stats = player.GetComponent<PlayerStats>().OnDeath();
            dispalyStats = true;
        }
    }

    //When the player falls through the trigger, the character is deactivated.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.SetActive(false);
        }
    }

    private void OnGUI()
    {
        if (dispalyStats)
        {
            string deathMessage = "You fell down! \n\nScore: "+ stats[0].ToString() + "\nPlatforms climbed: " 
                + stats[1].ToString() + "\nHighest combo: " + stats[2].ToString();


            GUI.DrawTexture(new Rect((Screen.width - 400) / 2, (Screen.height - 300) / 2, 400, 300), background);
            GUI.TextArea(new Rect((Screen.width - 400)/2, (Screen.height - 300) / 2, 400, 300), deathMessage, messageStyle);
            if(GUI.Button(new Rect((Screen.width - 150) / 2, (Screen.height + 300) / 2, 150, 50), "Return to menu", buttonStyle))
            {
                SceneManager.LoadScene("menu");
            }
        }
    }
}
