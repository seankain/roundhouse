using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBall : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bounce");
    }

}
