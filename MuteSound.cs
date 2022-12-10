using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteSound : MonoBehaviour
{
    //Define an object source of AudioSource class
    AudioSource source;

    void Start()
    {
    //Assign source to the AudioSource component
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
    //When the source stops playing, destroy the source object
        if (!source.isPlaying)
        { 
            Destroy(gameObject);
        }
    }
}
