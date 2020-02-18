using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitIdleCollision : MonoBehaviour
{

    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
        }
            
    }
}
