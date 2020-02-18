using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamController : MonoBehaviour
{

    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }
}
