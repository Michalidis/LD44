using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialsHolder : MonoBehaviour
{
    public GameObject Fireball;
    public AudioSource audio_player;
    public AudioClip Fireball_sound;
    public AudioClip Item_picked_up;
    // Start is called before the first frame update
    void Start()
    {
        audio_player = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayItemPickedUpSound()
    {
        audio_player.clip = Item_picked_up;
        audio_player.Play();
    }
    public void PlayFireballSound()
    {
        audio_player.clip = Fireball_sound;
        audio_player.Play();
    }
}
