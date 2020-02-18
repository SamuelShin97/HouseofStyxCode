using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //*****************Health Bar**********
    public RectTransform healthTransform; //used for position of health bar
    private float cachedY;
    private float minXValue; //position of health bar @ 0 health
    private float maxXValue; //starting position of health bar @ full health
    //**********Anguish Cup****************
    public RectTransform anguishTransform; //used for position of anguish chalice
    private float cachedX;
    private float minYValue; //position of mask @ 0 anguish
    private float maxYValue; //position of mask @ 100 anguish
    //**********HUD Images***********************
    public Image knife_image; //will be toggled on/off depending on active weapon
    public Image pan_image;
    public Image bottle_image;
    public Image george_image;
    public Image george_highlight;
    public Image child_image;
    public Image child_highlight;
    public Image fighter_image;
    public Image fighter_highlight;
    public Image chalice_image;
    //***********Stats*******************
    public float maxHealth { get; set; }
    public float currentHealth { get; set; }
    public float atk1_dmg { get; set; }
    public float atk2_dmg { get; set; }
    public float atk1_spd { get; set; }
    public float atk2_spd { get; set; }
    public float anguish { get; set; }
    public float anguish_max { get; set; }
    public float anguish_rate { get; set; }
    public float anguish_gain { get; set; }
    public float anguish_loss { get; set; }
    float elapsed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        cachedY = healthTransform.position.y;
        maxXValue = healthTransform.position.x;
        minXValue = healthTransform.position.x - healthTransform.rect.width; //bar goes to the left

        cachedX = anguishTransform.position.x;
        minYValue = anguishTransform.position.y;
        maxYValue = anguishTransform.position.y + anguishTransform.rect.height; //mask goes upward

        //************Stats related code***************
        maxHealth = 100f;
        currentHealth = maxHealth;
        atk1_dmg = 6f;
        atk2_dmg = 11f;
        atk1_spd = .8f;
        atk2_spd = .5f;
        anguish = 0f;
        anguish_rate = .5f;
        anguish_gain = 10f;
        anguish_loss = 5f;
        anguish_max = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        //**************************Stats related code**************************
        elapsed += Time.deltaTime; //plus 1 second
        if (elapsed >= 1f) //perform following action every second
        {
            elapsed = elapsed % 1f;//reset time
            anguish_tick(); //anguish increases by 0.1

        }
        handleHealth(); //update health bar position according to currentHealth
        handleAnguish(); //update anguish mask  position according to currentAnguish
        togglePersona();
        toggleWeapon();

        if (currentHealth <= 0)
        {
            //deathScreen();
        }

        changePersona();
        characterHit();
    }

    public enum activePersona
    {
        child,
        george,
        fighter
    }
    public activePersona ap = activePersona.george; //ap is used in togglePersona()

    public enum activeWeapon
    {
        knife,
        pan,
        bottle,
        nothing
    }
    public activeWeapon aw = activeWeapon.bottle; //aw is used in toggleWeapon()

    private void togglePersona()
    {
        switch (ap) //toggles highlighter and sets alpha accordingly
        {
            case activePersona.george:
                george_highlight.enabled = true; //enables highlighter and sets alpha to 1
                var tempclr = george_image.color;
                tempclr.a = 1f;
                george_image.color = tempclr;

                fighter_highlight.enabled = false; //disables both imgs and reduces alpha
                tempclr = fighter_image.color;
                tempclr.a = .5f;
                fighter_image.color = tempclr;

                child_highlight.enabled = false;
                tempclr = child_image.color;
                tempclr.a = .5f;
                child_image.color = tempclr;


                //change stats: default
                maxHealth = 100f;
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
                atk1_dmg = 6f;
                atk2_dmg = 11f;
                atk1_spd = .8f;
                atk2_spd = .5f;
                anguish_rate = .5f;
                anguish_gain = 10f;
                anguish_loss = 5f;
                anguish_max = 100f;
                //speed = 5f;
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;

            case activePersona.child:
                george_highlight.enabled = false;
                tempclr = george_image.color;
                tempclr.a = .5f;
                george_image.color = tempclr;

                fighter_highlight.enabled = false;
                tempclr = fighter_image.color;
                tempclr.a = .5f;
                fighter_image.color = tempclr;

                child_highlight.enabled = true;
                tempclr = child_image.color;
                tempclr.a = 1f;
                child_image.color = tempclr;

                //stats for child
                maxHealth = 75f;
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
                atk1_dmg = 4f;
                atk2_dmg = 8f;
                atk1_spd = 1.2f;
                atk2_spd = .7f;
                anguish_rate = .5f;
                anguish_gain = 10f;
                anguish_loss = 5f;
                anguish_max = 100f;
                //speed = 8f;
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                break;

            case activePersona.fighter:
                george_highlight.enabled = false;
                tempclr = george_image.color;
                tempclr.a = .5f;
                george_image.color = tempclr;

                fighter_highlight.enabled = true;
                tempclr = fighter_image.color;
                tempclr.a = 1f;
                fighter_image.color = tempclr;

                child_highlight.enabled = false;
                tempclr = child_image.color;
                tempclr.a = .5f;
                child_image.color = tempclr;

                // stats for fighter
                maxHealth = 125f;
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
                atk1_dmg = 10f;
                atk2_dmg = 14f;
                atk1_spd = 0.5f;
                atk2_spd = .4f;
                anguish_rate = .5f;
                anguish_gain = 10f;
                anguish_loss = 5f;
                anguish_max = 100f;
                //speed = 3f;
                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                break;

        }

    }

    private void toggleWeapon()
    {
        switch (aw) //toggles weapon displays
        {
            case activeWeapon.knife:
                knife_image.enabled = true;
                pan_image.enabled = false;
                bottle_image.enabled = false;
                break;
            case activeWeapon.pan:
                pan_image.enabled = true;
                knife_image.enabled = false;
                bottle_image.enabled = false;
                break;
            case activeWeapon.bottle:
                bottle_image.enabled = true;
                knife_image.enabled = false;
                pan_image.enabled = false;
                break;
            case activeWeapon.nothing:
                bottle_image.enabled = false;
                knife_image.enabled = false;
                pan_image.enabled = false;
                break;
            default:
                return;
        }

    }

    private float mapValue(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin; //this shit works
    }

    public void handleHealth()
    {
        float currentXValue = mapValue(currentHealth, 0, maxHealth, minXValue, maxXValue);

        healthTransform.position = new Vector3(currentXValue, cachedY);

        //in original video there's a section here that handles healthBar color changes.
        //we don't need that so I left it out
    }

    private void handleAnguish()
    {
        float currentYValue = mapValue(anguish, 0, anguish_max, minYValue, maxYValue);
        anguishTransform.position = new Vector3(cachedX, currentYValue);
        if (anguish >= anguish_max)
        {
            StartCoroutine(dmgTime());
        }
    }

    IEnumerator dmgTime()
    {
        var temp = chalice_image.color;
        temp.a = 1f;
        chalice_image.color = temp;
        yield return new WaitForSeconds(1f);
        temp.a = .5f;
        chalice_image.color = temp;
    }

    void characterHit()
    {
        float oldHealth = currentHealth;

        /*if(currentHealth < oldHealth) { 
            animator.SetTrigger("hit");
            hit.clip = playerHit;
            hit.Play();
        }*/
    }

    void changePersona()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            if (ap != activePersona.george)
            {
                anguish = 0f;
                ap = activePersona.george;
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (ap != activePersona.child)
            {
                anguish = 0f;
                ap = activePersona.child;
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            if (ap != activePersona.fighter)
            {
                anguish = 0f;
                ap = activePersona.fighter;
            }
        }
    }

    //*********************Stats related code******************
    public void takeDamage(float x)
    {
        currentHealth -= x;
    }
    void anguish_tick()
    {
        if (anguish >= 100)
            return;
        else anguish += anguish_rate;
    }
    public void anguish_up() //called when player is hit
    {
        if (anguish >= 100)
            return;
        else anguish += anguish_gain;
    }
    public void anguish_down() //called when player hits
    {
        if (anguish <= 0)
            return;
        else anguish -= anguish_loss;
    }
    //********************End stats related code*********************
}
