using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splode : MonoBehaviour
{
    public float ExplosionForce = 40f;
    public float ExplosionRadius = 1f;
    Rigidbody[] rbs;
    bool sploded = false;

    void Awake()
    {
        rbs = GetComponentsInChildren<Rigidbody>();
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if (sploded) { return; }
        sploded = true;
        foreach (var rb in rbs)
        {
            rb.AddExplosionForce(this.ExplosionForce, this.gameObject.transform.position,0,0,ForceMode.VelocityChange);
        }
    }

}
