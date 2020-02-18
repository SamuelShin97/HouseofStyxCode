using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScreamer : MonoBehaviour
{

    public Transform screamer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(screamer == null)
        {
            
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, screamer.position, 1);
        }
        
    }
}
