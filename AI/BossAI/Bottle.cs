using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public GameObject bottleSplashPrefab;
    public AudioClip[] bottleCollisonSplash;
    public AudioClip[] bottleCollisonHit;
    public GameObject fireWall;
    private AudioClip select;
    public bossBottleImpulse impulseBottle;
    public StateController controller;
    public float bottleDamage;

    public AudioSource sourceBottle;

    // Start is called before the first frame update

    void Awake()
    {
        //source.clip = select;
        controller = GetComponent<StateController>();
        sourceBottle = GetComponent<AudioSource>();
    }

    void Start()
    {
        impulseBottle = GameObject.Find("bossBottleImpulse").GetComponent<bossBottleImpulse>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        //check for collisions with world
        //play audio source
        if (collision.gameObject.tag == "Enviroment")
        {
            //Debug.Log("Bottle collision with world");
            int index = Random.Range(0, bottleCollisonSplash.Length);
            select = bottleCollisonSplash[index];
            //call for method for bottle splash
            ContactPoint contact = collision.contacts[0];
            Vector3 pos = contact.point;
            GameObject splash = Instantiate(bottleSplashPrefab, pos, Quaternion.Euler(-90, 0, 0)) as GameObject;//instantiate prefab
            AudioSource.PlayClipAtPoint(select, splash.transform.position, 1.3f);
            Debug.Log("Ground Hit!" + pos);
            //Destroy(gameObject, 0f);
        }

        //check for collisions with player
        if (collision.gameObject.tag == "Player" )
        {
            collision.gameObject.GetComponent<BS_Main_Health>().ApplyDamage(bottleDamage, "Boss");
            impulseBottle.BossBottleImpulse();
            int index = Random.Range(0, bottleCollisonHit.Length);
            select = bottleCollisonHit[index];
            sourceBottle.PlayOneShot(select, 0.5f);
            Debug.Log("Player Hit!");
            //Destroy(gameObject, 0f);
        }
        else if (collision.gameObject.tag == "Enemy" && collision.gameObject.name != "FinalBossV2")
        {
            bottleDamage = 0.75f;
            collision.gameObject.GetComponent<BS_Main_Health>().ApplyDamage(bottleDamage, "Boss");
            int index = Random.Range(0, bottleCollisonHit.Length);
            select = bottleCollisonHit[index];
            sourceBottle.PlayOneShot(select, 0.5f);
        }


        if (collision.gameObject.tag == "FireWall")
        {
            //Transform wall = Instantiate(fireWall) as Transform;
            Physics.IgnoreCollision(fireWall.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}
