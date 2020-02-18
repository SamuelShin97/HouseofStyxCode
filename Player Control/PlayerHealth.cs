using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   // The current health the player has.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
    public float anguish { get; set; }
    public float anguish_max { get; set; }
    public float anguish_rate { get; set; }
    public float anguish_gain { get; set; }
    public float anguish_loss { get; set; }
    float elapsed = 0f;

    bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.


    void Awake()
    {
       
        // Set the initial health of the player.
        currentHealth = startingHealth;
        anguish = 0f;
        anguish_rate = .5f;
        anguish_gain = 10f;
        anguish_loss = 5f;
        anguish_max = 100f;
    }


    void Update()
    {
        /*
        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
        */
        elapsed += Time.deltaTime; //plus 1 second
        if (elapsed >= 1f) //perform following action every second
        {
            elapsed = elapsed % 1f;//reset time
            anguish_tick(); //anguish increases by 0.1
            //Debug.Log("one second has passed");
        }
    }


    public void TakeDamage(int amount)
    {
        Debug.Log("player took damage");
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;
        anguish_up();
        Debug.Log("Health: " + currentHealth);
        Debug.Log("Anguish: " + anguish);
        // Set the health bar's value to the current health.
        //healthSlider.value = currentHealth;

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }
    }


    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        SceneManager.LoadScene("ReScaledEnvironment");
        // Tell the animator that the player is dead.

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).

        // Turn off the movement scripts.

    }
    void anguish_tick()
    {
        anguish += anguish_rate;
    }
    public void anguish_up() //called when player is hit
    {
        anguish += anguish_gain;
    }
    public void anguish_down() //called when player hits
    {
        anguish -= anguish_loss;
    }
}
