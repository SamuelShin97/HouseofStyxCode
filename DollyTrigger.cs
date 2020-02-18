using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DollyTrigger : MonoBehaviour
{
    public CutSceneMovement updatePosition;
    public PlayableDirector director;
    bool check = false;

    // Start is called before the first frame update
    void Start()
    {
        updatePosition = updatePosition.GetComponent<CutSceneMovement>();
        director = director.GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (check)
        {
            GetComponent<Collider>().enabled = false;
        }

        if (other.CompareTag("Player"))
        {
            Debug.Log("Currently Playing: " + director);
            updatePosition.currentDirector = director;
            //Debug.Log("Update current directory: " + updatePosition.currentDirector);
            director.Play();
            check = true;
        }
    }
}
