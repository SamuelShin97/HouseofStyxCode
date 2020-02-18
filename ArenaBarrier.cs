using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBarrier : MonoBehaviour
{
    // array that holds the fog animations
    public Animator[] fogAnim;
    public ParticleSystem particle;
    public Animator particleAnim;

    // list to hold number of enemies for each arena
    List<Collider> enemiesList = new List<Collider>();

    public bool barrierHasEnemies = true;
    public bool barrierActive = false;
    public bool hasBeenTriggered = false;

    Collider enemy;
    Collider enemyCopy;

    //Audio sources
    public AudioSource audioBGM;
    public AudioSource audioCombat;
    public AudioSource combatIntro;
    public Animator musicAnim_1;
    public Animator musicAnim_2;
    public float waitTime;
    public float waitToDestroy;

    //takes the first child of arena
    public GameObject fog;

    void Start()
    {
        fog = transform.GetChild(0).gameObject; //child of arena
        fogAnim = fog.GetComponentsInChildren<Animator>(); //takes array of all grandhildren animator components
        particleAnim = particle.GetComponent<Animator>();
        StartCoroutine(bgmMusic());
    }
    void Update()
    {


        // removes barrier when all enemies are killed
        if(barrierActive == true && enemiesList.Count <=0)
        {
            particle.Stop();
            particleAnim.SetTrigger("AlphaFadeOut");
            foreach(Animator anim in fogAnim ) //slowly fades out the Fog
            {
                anim.SetTrigger("AlphaFadeOut");
            }

            StartCoroutine(clearFog());
            StartCoroutine(bgmMusic());
            StartCoroutine(destroyArena());
            barrierActive = false;
        }


        //determines when player kills enemy
        //if enemy was in list. If so, removes from list.
        for(int i = 0; i < enemiesList.Count; i++)
        {
            if(!enemiesList[i])
            {
                enemiesList.Remove(enemiesList[i]);
                Debug.Log(enemiesList.Count);
            }
        }

        /*if(enemiesList.Contains(enemyCopy))
        {
            //Debug.Log("Enemy List has this");
            if (!enemyCopy)
            {
                enemiesList.Remove(enemyCopy);
                Debug.Log(enemiesList.Count);
            }
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && hasBeenTriggered == false)
        {
            hasBeenTriggered = true;
            StartCoroutine(CombatMusic());
            fog.SetActive(true);
            barrierActive = true;
            particle.Play();
        }

        if(!enemiesList.Contains(other) && (other.gameObject.tag == "Enemy"))
        {
            enemiesList.Add(other);
            enemyCopy = other;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "EnemyRange" && barrierActive == true)
        {
            enemy = other;
        }

    }

    // IEnumerator Functions that control and change audio
    // from BGM to Combat and vice versa
    IEnumerator CombatMusic()
    {
        if(audioCombat.isPlaying)
        {
            yield break;
        }
        musicAnim_1.SetTrigger("FadeOut");
        combatIntro.Play();
        musicAnim_2.Play("FadeIn");
        audioCombat.Play();
        yield return new WaitForSeconds(waitTime);
        audioBGM.Stop();
    }


    IEnumerator bgmMusic()
    {
        if (audioBGM.isPlaying)
        {
            yield break;
        }
        //combatIntro.Stop();
        musicAnim_2.SetTrigger("FadeOut");
        musicAnim_1.Play("FadeIn");
        audioBGM.Play();
        yield return new WaitForSeconds(waitTime);
        audioCombat.Stop();
    }

    IEnumerator clearFog()
    {
        yield return new WaitForSeconds(waitTime);
        fog.SetActive(false);
    }

    IEnumerator destroyArena()
    {
        yield return new WaitForSeconds(waitToDestroy);
        Destroy(gameObject);
    }
}
