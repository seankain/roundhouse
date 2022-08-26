using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MouseOverPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public UnityEvent MouseOverEvent;
    private bool mouseIn = false;

    private void Update()
    {
        if (mouseIn)
        {
            MouseOverEvent.Invoke();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseIn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseIn = false;
    }

}
