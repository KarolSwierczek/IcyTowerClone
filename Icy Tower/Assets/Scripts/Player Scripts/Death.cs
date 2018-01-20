using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour {
    public Texture background;

    private GameObject player;
    private CameraMovement cameraMovement;
    private int[] stats;
    private bool dispalyStats = false;
    private GUIStyle style = new GUIStyle();

    private void Start()
    {
        cameraMovement = GetComponentInParent<CameraMovement>();
        player = GameObject.Find("Player");

        style.alignment = TextAnchor.UpperCenter;
        style.fontSize = 35;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Physics.IgnoreLayerCollision(0, LayerMask.NameToLayer("Platform"), true);
            cameraMovement.SetDeath(true);
            stats = player.GetComponent<PlayerStats>().OnDeath();
            dispalyStats = true;
        }
    }

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
            GUI.TextArea(new Rect((Screen.width - 400)/2, (Screen.height - 300) / 2, 400, 300), deathMessage, style);
            if(GUI.Button(new Rect((Screen.width - 150) / 2, (Screen.height + 350) / 2, 150, 40), "Return to menu"))
            {
                SceneManager.LoadScene("menu");
            }
        }
    }
}
