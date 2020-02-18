using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleShard : MonoBehaviour
{
    public StateController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<StateController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //player particle collisions
    private void OnParticleCollision(GameObject col)
    {
        //check for collisions with player
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<BS_Main_Health>().ApplyDamage(2f, "Boss");
            Debug.Log("Particle Destroyed!");
            Destroy(gameObject, 0f);
        }
    }
}
