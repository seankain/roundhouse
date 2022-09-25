using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSoundEmitter : MonoBehaviour
{
    public AudioSource Audio;
    public List<AudioClipLocation> clipLocations;
    public bool Automatic = false;
    public float Probability = 0.5f;
    public float Rate = 10f;
    private float elapsed = 0;

    public void PlayRandom()
    {
        var index = UnityEngine.Random.Range(0, clipLocations.Count - 1);
        var clipLocation = clipLocations[index];
        StartCoroutine(PlaySubclip(clipLocation));
    }

    private IEnumerator PlaySubclip(AudioClipLocation location)
    {
        Audio.time = location.Start;
        Audio.Play();
        yield return new WaitForSeconds(location.Duration);
        Audio.Stop();
    }

    void Update()
    {
        if (!Automatic) { return; }
        elapsed += Time.deltaTime;
        if(elapsed >= Rate)
        {
            elapsed = 0;
            if(Random.value < Probability)
            {
                PlayRandom();
            }
               
        }
    }
}


[Serializable]
public class AudioClipLocation
{
    public float Start = 0;
    public float Duration = 0;
}
