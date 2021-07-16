using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    void Start()
    {
        Invoke("DestroyParticle", 1);
    }
    void DestroyParticle()
    {
        Destroy(gameObject);
    }
}
