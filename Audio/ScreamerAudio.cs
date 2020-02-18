using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamerAudio : MonoBehaviour
{
    public AudioClip teleport;
    public AudioClip hitFlesh;
    public AudioClip[] screamAttacks;
    private AudioClip select;

    public AudioSource source;
    // Start is called before the first frame update

    //get source component type
    void Awake()
    {
        //source.clip = select;
        //sourceMovement = GetComponent<AudioSource>();
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //********************Sound FX Events*********************
    public void HitEvent()
    {
        source.pitch = Random.Range(1.0f, 1.6f);
        source.volume = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(hitFlesh, 1f);
    }

    public void ScreamAttack()
    {
        int index = Random.Range(0, screamAttacks.Length);
        select = screamAttacks[index];
        source.pitch = Random.Range(0.9f, 1.1f);
        source.volume = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(select, 1f);
    }

    public void Teleport()
    {
        source.pitch = Random.Range(0.6f, 1.0f);
        source.volume = Random.Range(0.7f, 0.9f);
        source.PlayOneShot(teleport, 1f);
    }
}
