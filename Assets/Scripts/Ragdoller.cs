using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoller : MonoBehaviour
{
    public Animator anim;
    public Rigidbody rb;
    public Collider playerCollider;
    public float DepenetrationVelocity = 1f;
    private Rigidbody[] ragdollRigidBodies;
    // Start is called before the first frame update

    private void Awake()
    {
        playerCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        rb.maxDepenetrationVelocity = DepenetrationVelocity;
    }

    private void Start()
    {
        ragdollRigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach(var r in ragdollRigidBodies)
        {
            r.isKinematic = true;
        }
    }

    public void SetRagdoll(bool ragdollActivated)
    {
        var colliders = GetComponentsInChildren<Collider>(true);
        playerCollider.enabled = !ragdollActivated;

        rb.velocity = Vector3.zero;
        rb.isKinematic = ragdollActivated;
        foreach (var collider in colliders)
        {
            if (collider != playerCollider)
            {
                collider.isTrigger = !ragdollActivated;
                collider.attachedRigidbody.isKinematic = !ragdollActivated;
                collider.attachedRigidbody.velocity = Vector3.zero;
            }
        }
        foreach(var r in ragdollRigidBodies)
        {
            r.velocity = Vector3.zero;
            r.isKinematic = !ragdollActivated;
        }
        anim.enabled = !ragdollActivated;
    }

  
}
