using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public int DestructionScore = 100;
    private GameObject UnbrokenParent;
    private GameObject BrokenParent;
    private bool broken = false;
    private float brokenElapsed = 0f;
    public float BrokenMaxTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        UnbrokenParent.SetActive(false);
        BrokenParent.SetActive(true);
        StartCoroutine(DestroyAfter(BrokenMaxTime));
    }

    private IEnumerator DestroyAfter(float destroyTime)
    {
        var elapsed = 0;
        while (elapsed < destroyTime)
        {
            elapsed++;
            yield return null;
        }
        Destroy(this);
    }

}
