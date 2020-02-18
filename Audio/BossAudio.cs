using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAudio : MonoBehaviour
{
    //sounds for damage, defeat, heavy, swipe, and walk animations
    //public AudioClip damageClip;
    //public AudioClip defeatClip;
    public AudioClip hitFlesh;
    public AudioClip heavyClip;
    public AudioClip swipeClip;
    public AudioClip[] walkClips;
    private AudioClip select;

    //private AudioSource sourceDamage;
    //private AudioSource sourceDefeat;
    //public AudioSource[] sounds;
    public AudioSource sourceHeavy;
    public AudioSource sourceSwipe;
    public AudioSource sourceFlesh;
    public AudioSource source;

    void Awake()
    {
        //source.clip = select;
        //sourceDefeat = GetComponent<AudioSource>();
        sourceFlesh = GetComponent<AudioSource>();
        sourceHeavy = GetComponent<AudioSource>();
        sourceSwipe = GetComponent<AudioSource>();
        source = GetComponent<AudioSource>();
        //sourceWalk = GetComponent<AudioSource>();
        /*sounds = GetComponent<AudioSource[]>();
        sourceHeavy = sounds[0];
        sourceSwipe = sounds[1];
        sourceFlesh = sounds[2];
        source = sounds[3];*/
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    /*public void DamageEvent()
    {
        sourceDamage.PlayOneShot(damageClip, 1f);
    }

    public void DefeatEvent()
    {
        sourceDefeat.PlayOneShot(defeatClip, 1f);
    }*/

    public void DamageEvent()
    {
        source.pitch = Random.Range(0.9f, 1.1f);
        source.volume = Random.Range(0.9f, 1.1f);
        sourceFlesh.PlayOneShot(hitFlesh, 1f);
    }

    public void HeavyAttackEvent()
    {
        source.pitch = Random.Range(0.9f, 1.1f);
        source.volume = Random.Range(0.9f, 1.1f);
        sourceHeavy.PlayOneShot(heavyClip, 1f);
    }

    public void SwipeAttackEvent()
    {
        source.pitch = Random.Range(0.9f, 1.1f);
        source.volume = Random.Range(0.9f, 1.1f);
        sourceSwipe.PlayOneShot(swipeClip, 1f);
    }

    public void Step1Event()
    {
        int index = Random.Range(0, walkClips.Length);
        select = walkClips[index];
        source.pitch = Random.Range(0.9f, 1.1f);
        source.volume = Random.Range(0.7f, 1.0f);
        source.PlayOneShot(select, 1f);
    }

    public void Step2Event()
    {
        int index = Random.Range(0, walkClips.Length);
        select = walkClips[index];
        source.pitch = Random.Range(0.9f, 1.1f);
        source.volume = Random.Range(0.7f, 1.0f);
        source.PlayOneShot(select, 1f);
    }
}


