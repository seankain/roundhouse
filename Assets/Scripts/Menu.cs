using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Menu : MonoBehaviour
{

    public Button PlayButton;
    public Button QuitButton;
    public CinemachineVirtualCamera Camera1;
    public CinemachineVirtualCamera Camera2;
    private float buttonAlpha = 0;
    private TextMeshProUGUI playTmp;
    private TextMeshProUGUI quitTmp;

    // Start is called before the first frame update
    void Start()
    {
        //TextMeshProUGUI
        playTmp = PlayButton.GetComponentInChildren<TextMeshProUGUI>();
        playTmp.color = new Color(255, 255, 255, 0);
        quitTmp = QuitButton.GetComponentInChildren<TextMeshProUGUI>();
        quitTmp.color = new Color(255,255, 255, 0);
        StartCoroutine(BeginMenuPan());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator BeginMenuPan()
    {
        var elapsed = 0f;
        while(elapsed < 5f)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        Camera1.enabled = false;
        Camera2.enabled = true;
        StartCoroutine(ToggleButtons());
    }

    public IEnumerator ToggleButtons()
    {
        var direction = 1;
        if(buttonAlpha >= 254)
        {
            direction = -1;
        }
        while (buttonAlpha < 255)
        {
            buttonAlpha += direction * Time.deltaTime;
            playTmp.color = new Color(255, 255, 255, buttonAlpha);
            quitTmp.color = new Color(255, 255, 255, buttonAlpha);
            yield return null;
        }
    }

    void PlayClicked() 
    { 
      
    }
    void QuitClicked()
    {
        Application.Quit();
    }

}
