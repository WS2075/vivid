using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.MOP2;


public class Particle : MonoBehaviour
{
    [SerializeField]
    private ObjectPool particlePool;

    private ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        particle = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(particle.isStopped)
        {
            particlePool.Release(gameObject);
        }
    }
}
