using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDelayStart : MonoBehaviour
{

    public float DelayTime = 3f;
    public float FadeInTime = 3f;
    public float MaxAudioVolume = 1f;
    public float FadeIncrement = 1f;
    public bool Loop = true;
    public AudioSource Audio;
    // Start is called before the first frame update
    void Start()
    {
        Audio.volume = 0;
        StartCoroutine(FadeIn());
    }



    public IEnumerator FadeIn() 
    {
        var elapsed = 0f;
        while (elapsed <= DelayTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        Audio.Play();
        Audio.loop = Loop;
        while(Audio.volume <= MaxAudioVolume)
        {
            Audio.volume += FadeIncrement * Time.deltaTime;
            yield return null;
        }
        
    }
}
