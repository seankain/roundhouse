using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonSign : MonoBehaviour
{

    public GameObject UnlitSign;
    public GameObject LitSign;
    public AudioSource SignAudio;
    public List<float> FlickerTimes;
    public float LightStartTime = 1f;
    public float AudioStartDelay = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LightSign());
    }

    private IEnumerator LightSign()
    {
        var elapsed = 0f;
        while (elapsed < LightStartTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        SignAudio.Play();
        UnlitSign.SetActive(false);
    }

}
