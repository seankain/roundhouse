using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crossfader : MonoBehaviour
{

    public Image panelImage;

    public IEnumerator FadeIn(float time) 
    {
        var alpha = 0f;
        while(alpha >= 0)
        {
            alpha -= time * Time.deltaTime;
            panelImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
    public IEnumerator FadeOut(float time) 
    {
        var alpha = 1f;
        while (alpha <= 1)
        {
            alpha += time * Time.deltaTime;
            yield return null;
        }
    }

}
