using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDustController : MonoBehaviour
{

    StateController boss;
    ParticleSystem dust;
    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponentInParent<StateController>();
        dust = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(boss.walking == true)
        {
            Debug.Log("Play dust");
            if (dust.isStopped)
            {
                dust.Play();
            }
            
        }
        else
        {
            dust.Stop();
        }
    }
}
