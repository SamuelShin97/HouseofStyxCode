using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBloodLauncher : MonoBehaviour
{

    //could create an empty array to store particles and place prefab of particle in it on every collision
    public ParticleSystem bloodParticleLauncher;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            //bloodParticleLauncher.Emit(10);
            bloodParticleStart(0.5f, -0.5f, -0.5f, 100);
        }

    }

    void bloodParticleStart(float x, float y, float z, int number)  //later add collision as varaible
    {
        //normalVector = other.contacts[0].normal;    The normal vector for the fist contact point is stored in 'normalVector' (or use average of all contacts)
        //particles.transform.forward = normalVector;

        bloodParticleLauncher.transform.position = new Vector3(x, y, z);
        bloodParticleLauncher.Emit(number);
    }
}
