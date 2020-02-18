using System.Collections.Generic;
using UnityEngine;

public class EnvCam_Control : MonoBehaviour
{
    public List<Camera> Cameras;

    private void Start()
    {
        EnableCamera(11);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EnableCamera(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EnableCamera(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EnableCamera(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EnableCamera(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            EnableCamera(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            EnableCamera(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            EnableCamera(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            EnableCamera(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            EnableCamera(8);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            EnableCamera(9);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            EnableCamera(10);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            EnableCamera(11);
        }

        /*
         * If you want to add more cameras, you need to add
         * some more 'else if' conditions just like above
         */
    }

    private void EnableCamera(int n)
    {
        Cameras.ForEach(cam => cam.enabled = false);
        Cameras[n].enabled = true;
    }
}