using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchSyncFireWIthLight : MonoBehaviour
{
    private Light light;
    private ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        light = transform.parent.GetComponentInChildren<Light>();
        particles = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (light.enabled)
        {
            if (!particles.isPlaying) particles.Play();

        }
        else 
        {
            if (particles.isPlaying) particles.Stop();
        }
    }
}
