using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWeaponCollisionTrigger : MonoBehaviour
{
    public GameObject impactParticle;
    public Vector3 impactNormal;
    GameObject blood;
    GameObject player;
    UI playerStats;
    
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<UI>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            StateController enemyStates = other.gameObject.GetComponent<StateController>();
            Debug.Log("Grunt hit");
            //other.GetComponent<EnemyHealth>().TakeDamage(10);
            //impactParticle = Instantiate(blood, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
            //enemyStates.TakeDamage(15);
            playerStats.anguish_down();
            //Debug.Log("Anguish down. " + playerStats.anguish);
            // BloodSpray blood = other.gameObject.GetComponent<BloodSpray>();
            // blood.Play();

        }
        else if (other.tag == "Boss")
        {
            StateController enemyStates = other.gameObject.GetComponent<StateController>();
            Debug.Log("Boss hit");
            //other.GetComponent<EnemyHealth>().TakeDamage(10);
            //impactParticle = Instantiate(blood, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
            //enemyStates.TakeDamage(8);
            playerStats.anguish_down();
            //SceneManager.LoadScene("ReScaledEnvironment");

        }
        else if (other.tag == "Screamer")
        {
            StateController enemyStates = other.gameObject.GetComponent<StateController>();
            Debug.Log("Screamer hit");
            //other.GetComponent<EnemyHealth>().TakeDamage(10);
            //impactParticle = Instantiate(blood, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
            //enemyStates.TakeDamage(12);
            playerStats.anguish_down();
        }
        else Debug.Log("no enemy");
    }

    
}
