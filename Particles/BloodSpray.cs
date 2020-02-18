using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSpray : MonoBehaviour
{
    /*
        public IEnumerator Start()
        {
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }
        */
    public bool play;
    public ParticleSystem blood1;
    public ParticleSystem blood2;
    public ParticleSystem blood3;

    public void Start()
    {
        play = false;
    }
    public void Update()
    {
        if (play)
        {
            blood1.Play();
            blood2.Play();
            blood3.Play();
            play = false;
        }
    }

    public void Play()
    {
        play = true;
    }
}
