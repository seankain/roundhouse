using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterInstancePool : MonoBehaviour
{
    public GameObject Prefab;
    public int MaxPoolSize = 20;
    private List<ParticleSystem> instances = new List<ParticleSystem>();

    public void Play(Vector3 location)
    {
        if (instances.Count < MaxPoolSize)
        {
            var instance = Instantiate(Prefab, location, Quaternion.identity, null);
            instances.Add(instance.GetComponent<ParticleSystem>());
        }
        else
        {
            foreach(var i in instances)
            {
                if (!i.isStopped)
                {
                    i.gameObject.transform.SetPositionAndRotation(location, Quaternion.identity);
                    i.Play();
                }
            }
        }
    }
}
