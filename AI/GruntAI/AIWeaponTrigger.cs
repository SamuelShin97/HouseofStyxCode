using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWeaponTrigger : MonoBehaviour
{
    public GameObject impactParticle;
    public Vector3 impactNormal;
    GameObject blood;
    bool attacking;

    StateController grunt;
    GameObject player;                          // Reference to the player GameObject.
    UI playerHealth;                  // Reference to the player's health.

    private bool justHit { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        justHit = false;
        grunt = GetComponentInParent<StateController>();
        
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<UI>();
    }
    IEnumerator dmgTime()
    {
        justHit = true;
        yield return new WaitForSeconds(2f);
        justHit = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("no player");
        if (other.tag == "Player" && grunt.isAttacking)
        {
            Debug.Log("player hit");
            //Instantiate(impactParticle, grunt.transform.position, Quaternion.LookRotation(hit.normal));
            //impactParticle = Instantiate(blood, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
            if(!justHit)
                playerHealth.takeDamage(5f);
            StartCoroutine(dmgTime());
            playerHealth.anguish_up();
            //other.gameObject.GetComponent<BloodSpray>().Play();
            //Debug.Log(GameManager.playerHealth);
        }
    }
    
}
