using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Vial : MonoBehaviour
{
    public AudioSource pickupSound;
    public BS_Main_Health playerHealth;
    private float recoverAmount; //random between 3-5
    public bool DeathOverride;  // check to not despawn automatically
    public bool HealAmountOverride;
    [Tooltip("Amount to override. Enter a percentage % to heal")]
    public float overrideAmount; // Overrides recoverAmount by designated % chosen

    private void Awake()
    {
        pickupSound = GameObject.FindGameObjectWithTag("HealthAudio").GetComponent<AudioSource>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<BS_Main_Health>();
    }

    private void Start()
    {
        DestroyObjectDelay();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && HealAmountOverride != true)
        {
            pickupSound.Play();
            recoverAmount = Random.Range(playerHealth._maxHealth * 0.1f, playerHealth._maxHealth * 0.17f);
            playerHealth.healDamage(recoverAmount);
            Destroy(this.gameObject);
        }

        else if(other.tag == "Player" && HealAmountOverride == true)
        {
            pickupSound.Play();
            playerHealth.healDamage(playerHealth._maxHealth * ( overrideAmount/ 100));
            Destroy(this.gameObject);
        }
    }


    void DestroyObjectDelay()
    {
        if (DeathOverride != true)
        {
            Destroy(this.gameObject, 12.0f);
        }
    }
}
