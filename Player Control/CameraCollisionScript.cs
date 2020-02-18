using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionScript : MonoBehaviour
{
    public float min = 0.4f;
    public float max = 3f;
    public float smooth = 5f;
    public float disReduce = 0.85f;
    public float distance;
    public Vector3 direction;
    public Vector3 cameraPosition;

    // Start is called before the first frame update
    void Awake()
    {
        direction = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        cameraPosition = transform.parent.TransformPoint(direction * max);

        if(Physics.Linecast(transform.parent.position, cameraPosition, out hit))
        {
            distance = Mathf.Clamp((hit.distance * disReduce), min, max);
        } else
        {
            distance = max;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, direction * distance, Time.deltaTime * smooth);
    }
}
