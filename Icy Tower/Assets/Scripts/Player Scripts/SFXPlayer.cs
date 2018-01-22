using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script plays a random sound from a given array when the player is jumping/superjumping.
public class SFXPlayer : MonoBehaviour {

    public AudioClip[] JumpingSounds;
    public AudioClip[] SuperJumpingSounds;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playJumpingSound(bool isSuperJumping)
    {
        int randomIndex = Mathf.FloorToInt(Random.value * 3.99f);
        if (isSuperJumping)
        {
            audioSource.clip = SuperJumpingSounds[randomIndex];
            
        }
        else
        {
            audioSource.clip = JumpingSounds[randomIndex];
        }
        audioSource.Play();
    }
}
