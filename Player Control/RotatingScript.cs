using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingScript : MonoBehaviour
{
    public float rotationSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad5))
        {
            transform.Rotate(Vector3.right * rotationSpeed);
        }
        if (Input.GetKey(KeyCode.Keypad8))
        {
            transform.Rotate(Vector3.left * rotationSpeed);
        }

    }
}
