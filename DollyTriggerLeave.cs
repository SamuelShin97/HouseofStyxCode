using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyTriggerLeave : MonoBehaviour
{
    public GameObject camPlayer;
    public GameObject camDolley;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            camPlayer.SetActive(true);
            camDolley.SetActive(false);
        }
    }
}
