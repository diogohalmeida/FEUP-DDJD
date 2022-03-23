using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] sounds;

    private AudioSource player;

    void Start()
    {
        player = this.gameObject.GetComponent<AudioSource>();
    }

    public void Play(int soundPosition){
        player.clip = sounds[soundPosition];
        player.Play();
    }
}
