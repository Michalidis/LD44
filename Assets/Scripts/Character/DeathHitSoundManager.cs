using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHitSoundManager : MonoBehaviour
{
    private AudioSource sound_player;
    public AudioClip[] death_sounds;
    public AudioClip[] hurt_sounds;
    public AudioClip resurrect_sound;
    // Start is called before the first frame update
    void Start()
    {
        sound_player = GetComponent<AudioSource>();
    }

    public void PlayDeathSound()
    {
        if (death_sounds.Length > 0)
        {
            sound_player.clip = death_sounds[Mathf.RoundToInt(Random.Range(0, death_sounds.Length))];
            sound_player.Play();
        }
    }

    public void PlayHurtSound()
    {
        if (hurt_sounds.Length > 0)
        {
            sound_player.clip = hurt_sounds[Mathf.RoundToInt(Random.Range(0, hurt_sounds.Length))];
            sound_player.Play();
        }

    }

    public void PlayResurrectSound()
    {
        if (resurrect_sound != null)
        {
            sound_player.clip = resurrect_sound;
            sound_player.Play();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
