using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventDrivenParticle : MonoBehaviour
{
    public EmitterInstancePool InstancePool;

    public void Emit(ParticleEmitEventArgs e)
    {
        InstancePool.Play(e.Position);
    }
}
