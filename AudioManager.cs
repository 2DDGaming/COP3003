using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //create a singleton (class that holds a public static reference to an instance of its an own type)
    //of the AudioManager
    public static AudioManager instance;
    
    //this keeps track of the singleton
    void Awake() { instance = this;  }

    //Sound Effects
    public AudioClip sfx_Landing, sfx_Attack;
    //Music
    public AudioClip music_creepy;
   
    public GameObject currentMusicObject;

    //Sound object
    public GameObject soundObject;
    
    public void PlaySFX(string sfxName)
    {   
        //Initiate switch statement to change sound effects depending on player movement
        switch (sfxName)
        {
            case "Landing":
                SoundObjectCreation(sfx_Landing);
                break;
        }    
    }

    public void PlayMusic(string musicName)
    {
        //Initiate switch statement to change music based on the scene
        switch (musicName)
        {
            case "Creepy":
                MusicObjectCreation(music_creepy);
                break;

        }
    }
    
    void MusicObjectCreation(AudioClip clip)
    {    
        //
        if (currentMusicObject)
            Destroy(currentMusicObject);
        //create runtime object of currentMusicObject 
        currentMusicObject = Instantiate(soundObject, transform);
        //assign clip to the value of Audio Source's component clip
        currentMusicObject.GetComponent<AudioSource>().clip = clip;
        //play the current music object's clip
        currentMusicObject.GetComponent<AudioSource>().Play();
    }

    void SoundObjectCreation(AudioClip clip)
    {
        //create runtime object of newObject (for sound effects)
        GameObject newObject = Instantiate(soundObject, transform);
        newObject.GetComponent<AudioSource>().clip = clip;
        newObject.GetComponent<AudioSource>().Play();
    }

}
