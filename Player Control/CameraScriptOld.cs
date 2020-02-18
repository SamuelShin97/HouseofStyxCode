using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptOld : MonoBehaviour
{

    public Transform pivot;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        pivot.transform.position = target.transform.position;
        //pivot.transform.parent = null;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        pivot.transform.position = target.transform.position;
    }
}
