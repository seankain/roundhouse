using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TurnEndEventHandler(object sender, EventArgs e);
public delegate void TurnStartEventHandler(object sender, EventArgs e);

public class TurnTaker : MonoBehaviour
{

    public event TurnEndEventHandler TurnEnded;
    public event TurnStartEventHandler TurnStarted;

    public void OnTurnStarted(EventArgs e)
    {
        TurnStartEventHandler handler = TurnStarted;
        handler?.Invoke(this, e);
    }

    public void OnTurnEnded(EventArgs e)
    {
        TurnEndEventHandler handler = TurnEnded;
        handler?.Invoke(this, e);
    }

}
