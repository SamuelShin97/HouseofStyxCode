using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverController : MonoBehaviour
{
    //adjust this to change speed
    float speed = 2f;
    //adjust this to change how high it goes
    float height = 0.1f;

    public Transform player;
    bool floatup;

    void Start()
    {
        floatup = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (floatup)
        {
            floatingUp();
        }
        else
        {
            floatingDown();
        }
        
    }


    IEnumerator floatingUp()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f * Time.deltaTime, transform.position.z);
        yield return new  WaitForSeconds(1);
        floatup = false;
    }

    IEnumerator floatingDown()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f * Time.deltaTime, transform.position.z);
        yield return new WaitForSeconds(1);
        floatup = true;
    }
}
