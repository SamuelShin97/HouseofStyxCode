using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //////////CAMERA VARIABLES//////////
    public Transform player;
    public Transform pivot;
    //public GameObject cameraFollow;
    public Vector3 rotation;
    public Vector3 offset;
    public float cameraSpeed = 10f;
    float clamp = 70f;
    float damp = 1;
    float currentAngle;
    float neededAngle;
    float playerYAngle;
    float playerXAngle;
    float angle;
    Quaternion camRotation;

    /////////MOUSE VARIABLES//////////
    float mouseX;
    float mouseY;
    public float mouseSens = 1f;
    float smoothX;
    float smoothY;
    public float smoothZoom = 4;
    public float zoomMin = 30;
    public float zoomMax = 60;
    public float rotationX = 0;
    public float rotationY = 0;
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

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//makes cursor disappear, click ESCP to get cursor back

        offset = player.position - transform.position;//offsets the camera position from the player position

        pivot.transform.position = player.transform.position;//set the pivot transform same as player
        pivot.transform.parent = null;//make pivot child of the player character

        rotation = transform.localRotation.eulerAngles;
        rotationX = rotation.x;
        rotationY = rotation.y;
    }

    // Update is called once per frame
    void Update()
    {
        //rotationX = rotationX + (mouseY * mouseSens * Time.deltaTime);
        //rotationX = Mathf.Clamp(rotationX, -clamp, clamp);
        //rotationY = rotationY + (mouseX * mouseSens * Time.deltaTime);
        //Quaternion rot = Quaternion.Euler(rotationX, rotationY, 0f);
        //transform.rotation = rot;
    }


    void LateUpdate()
    {
        pivot.transform.position = player.transform.position;//make pivot child of the player character

        mouseX = Input.GetAxis("Mouse X");
        mouseX *= mouseSens;//apply mouse sens
        pivot.Rotate(0, mouseX, 0);//rotate player based on horizontal mouse movement

        mouseY = Input.GetAxis("Mouse Y");
        mouseY *= mouseSens;//apply mouse sens
        //pivot.Rotate(-mouseY, 0, 0);//rotate player based on vertical mouse movement
        pivot.Rotate(-mouseY, 0, 0);

        //limit up look angle
        if (pivot.rotation.eulerAngles.x > 45f && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(45f, 0f, 0f);
        }

        //limit down look angle
        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 315f)
        {
            pivot.rotation = Quaternion.Euler(315f, 0f, 0f);
        }

        //playerXAngle = pivot.eulerAngles.x;//store player x axis rotation
        //playerYAngle = pivot.eulerAngles.y;//store player y axis rotation
        playerXAngle = pivot.eulerAngles.x;
        playerYAngle = pivot.eulerAngles.y;
        camRotation = Quaternion.Euler(playerXAngle, playerYAngle, 0);//set camera y-axis rotation
        transform.position = player.position - (camRotation * offset);//set postion

        if (transform.position.y < player.position.y)//if camera goes below player height
        {
            transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
        }

        transform.LookAt(player);

        /*currentAngle = transform.eulerAngles.y;
        neededAngle = cameraFollow.transform.eulerAngles.y;
        angle = Mathf.LerpAngle(currentAngle, neededAngle, Time.deltaTime * damp);

        Quaternion r = Quaternion.Euler(0, angle, 0);
        transform.position = cameraFollow.transform.position - (offset);
        transform.LookAt(cameraFollow.transform);

        /*Transform characterCamera = cameraFollow.transform;
        float follow = cameraSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, characterCamera.position, follow);*/
    }
}
