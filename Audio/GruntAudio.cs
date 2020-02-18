using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntAudio : MonoBehaviour
{
    public AudioClip flesh;
    public AudioClip overheadClip;
    public AudioClip slashClip;
    public AudioClip stabClip;
    public AudioClip[] movementClips;
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
        source.pitch = Random.Range(0.9f, 1.1f);
        source.volume = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(flesh, 1f);
    }

    public void StabAttackEvent()
    {
        source.pitch = Random.Range(0.9f, 1.1f);
        source.volume = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(stabClip, 1f);
    }

    public void SlashAttackEvent()
    {
        source.pitch = Random.Range(0.9f, 1.1f);
        source.volume = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(slashClip, 1f);
    }

    public void OverheadAttackEvent()
    {
        source.pitch = Random.Range(0.9f, 1.1f);
        source.volume = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(overheadClip, 1f);
    }

    public void Step1Event()
    {
        /*int index = Random.Range(0, movementClips.Length);
        select = movementClips[index];
        source.pitch = Random.Range(0.9f, 1.1f);
        source.volume = Random.Range(0.7f, 1.0f);
        source.PlayOneShot(select, 1f);*/
    }

    public void Step2Event()
    {
        /*int index = Random.Range(0, movementClips.Length);
        select = movementClips[index];
        source.pitch = Random.Range(0.9f, 1.1f);
        source.volume = Random.Range(0.7f, 1.0f);
        source.PlayOneShot(select, 1f);*/
    }
}
