using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Breakable : MonoBehaviour
{
    public int DestructionScore = 100;
    public float BreakCollisionSpeed = 10f;
    public GameObject BrokenPrefab;
    private bool broken = false;
    private float brokenElapsed = 0f;
    public float BrokenMaxTime = 5f;
    private EventDrivenParticle particle;
    private MeshRenderer meshRenderer;
    private Rigidbody rb;
    private BoxCollider boxCollider;

    void Awake()
    {
        particle = GetComponent<EventDrivenParticle>();
        meshRenderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{name} breakable collision impulse {collision.impulse}, relative velocity {collision.relativeVelocity}");
        if (Mathf.Abs(Vector3.Magnitude(collision.impulse)) < BreakCollisionSpeed) { return; }
        broken = true;
        Debug.Log($"{name} collision, should be exploding");
        meshRenderer.enabled = false;
        boxCollider.isTrigger = true;
        Instantiate(BrokenPrefab, gameObject.transform.position, Quaternion.identity);
        if (particle != null)
        {
            particle.Emit(new ParticleEmitEventArgs { Position = this.gameObject.transform.position });
        }
    }

    void Update()
    {
        if (!broken) { return; }
        brokenElapsed += Time.deltaTime;
        if(brokenElapsed >= Time.deltaTime)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyAfter(float destroyTime)
    {
        var elapsed = 0f;
        while (elapsed < destroyTime)
        {
            elapsed+= Time.deltaTime;
            yield return null;
        }
        Debug.Log("Destryoing breakable");
        Destroy(this);
    }

}

public delegate void BreakEventHandler(object sender, ParticleEmitEventArgs e);
public class ParticleEmitEventArgs
{
    public Vector3 Position;
}
