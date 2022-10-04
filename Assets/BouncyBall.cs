using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBall : MonoBehaviour
{
    public int Counter = 0;
    private void OnCollisionEnter(Collision collision)
    {
        Counter++;
        Debug.Log($"Bounced {Counter} times");
      
    }

}
