using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeBoxManager : MonoBehaviour
{
    private AudioSource sound_player;

    public AudioClip[] level_1_ambience;
    // Start is called before the first frame update
    void Start()
    {
        sound_player = GetComponent<AudioSource>();
        sound_player.clip = GetRandomClip(level_1_ambience);
        sound_player.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    AudioClip GetRandomClip(AudioClip[] clip_collection)
    {
        if (clip_collection.Length == 0)
        {
            return null;
        }

        return clip_collection[Mathf.RoundToInt(Random.Range(0, clip_collection.Length))];
    }
}
