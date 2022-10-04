using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Texture2D WalkTexture;
    public Texture2D SelectTexture;
    public TextMeshProUGUI playerMessage;
    public RawImage powerMeterImage;
    public Button DollAllButton;
    public TextMeshProUGUI scoreOutput;
    private GameState gameState;

    void Awake()
    {
        DollAllButton.onClick.AddListener(() => {
            var enemies = FindObjectsOfType<Enemy>();
            foreach(var enemy in enemies)
            {
                enemy.Hit(100, Vector3.zero, Vector3.zero);
            }
        });
    }
    private void Start()
    {
        gameState = FindObjectOfType<GameState>();
    }

    private void Update()
    {
        scoreOutput.text = ((int)gameState.TotalScore).ToString("D8");
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out var hitInfo)){
            if (hitInfo.collider.gameObject.tag == "Walkable")
            {
                Cursor.SetCursor(WalkTexture, Vector2.zero, CursorMode.Auto);
            }
            else if(hitInfo.collider.gameObject.tag == "Enemy")
            {
                Cursor.SetCursor(SelectTexture, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
            }
            //Debug.Log(hitInfo.collider.gameObject.name);
        }
  
    }

    public void ShowMessage(string messageText,int time=0)
    {
        playerMessage.text = messageText;
        playerMessage.enabled = true;
        if(time > 0)
        {
            TimeoutMessage(time);
        }
        
    }

    public void HideMessage()
    {
        playerMessage.text = string.Empty;
        playerMessage.enabled = false;
    }

    public IEnumerator TimeoutMessage(int time)
    {
        var elapsed = 0f;
        while(elapsed < time)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        HideMessage();
    }

    public IEnumerator FadePowerMeter(bool direction,float speed) 
    {
        //Fade in
        if (direction) 
        {
            var alpha = 255f;
            while (alpha > 0)
            {
                powerMeterImage.color = new Color(255, 255, 255, alpha);
                alpha -= 1 * speed * Time.deltaTime;
                yield return null;
            }
        }
        //Fade out
        else
        {
            var alpha = 0f;
            while (alpha < 255)
            {
                Debug.Log(alpha);
                powerMeterImage.color = new Color(255, 255, 255, alpha);
                alpha += 1 * speed * Time.deltaTime;
                yield return null;
            }
        }
    }

    public void HidePowerMeter()
    {
        powerMeterImage.color = new Color(255, 255, 255, 0);
    }
    public void ShowPowerMeter()
    {
        powerMeterImage.color = new Color(255, 255, 255,255);
    }
}
