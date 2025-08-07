using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Plays background music tracks depending on the current game stage. The
/// manager persists as a singleton so music continues across scenes.
/// </summary>
public class OSTManager : MonoBehaviour
{
    public static OSTManager instance; // singleton instance, what a goofy name
    /// <summary>Audio clips for the different stages of the game.</summary>
    public AudioClip ost1, ost2, ost3;
    private AudioSource source;

    void Start()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    /// <summary>Play the provided music clip in a loop.</summary>
    public void PlayOST(AudioClip clip)
    {
        print("sound srart");
        source.clip = clip; // set the clip to the audio source
        source.Play(); // play the audio source
        source.loop = true; // set the audio source to loop

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.musicStage < 1 && GameManager.instance.gameStage >= 1)
        {
            GameManager.instance.musicStage = 1; // Change the music stage to 1 if the game stage exceeds 1
            PlayOST(ost1); // Play the first OST
        }
        if (GameManager.instance.musicStage < 2 && GameManager.instance.gameStage > 3)
        {
            GameManager.instance.musicStage = 2; // Change the music stage to 2 if the game stage exceeds 3
            PlayOST(ost2); // Play the second OST
        }
        if (GameManager.instance.musicStage < 3 && GameManager.instance.gameStage > 5)
        {
            GameManager.instance.musicStage = 3; // Change the music stage to 3 if the game stage exceeds 6
            PlayOST(ost3); // Play the third OST
        }
    }
}
