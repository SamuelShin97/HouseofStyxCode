using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamerExplosion : MonoBehaviour
{

    public ParticleSystem goreExplosion;
    Vector3 position;

    
    public void ScreamerExplosionParticles()
    {
        position = transform.position;
        Instantiate(goreExplosion, position, Quaternion.identity);
    }
}
