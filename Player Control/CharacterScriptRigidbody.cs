using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScriptRigidbody : MonoBehaviour
{
    //////////PLAYER MOVEMENT VARIABLES////////////
    float moveHorizontal;//horizontal movement
    float moveVertical;//vertical movement
    public float speed = 15f; //player speed
    public float customGravity = 1f; //player custom gravity
    public float jump = 5f; //player jump height velocity
    public float smooth = 1f;//smooth the rotation

    public const float maxDash = 3f;//distance of dash
    public float dashDistance = 8f;//speed of dash 
    public float stopSpeed = 0.1f;//slow down speed
    float temp;
    float currentDash = maxDash;
    public Transform pivot;
    public GameObject character;

    private Camera playerCam;
    private CharacterController controller;//character controller
    private Vector3 playerVelocity;//player velocity
    private Vector3 movement;//player movement

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //movement = new Vector3(Input.GetAxis("Horizontal"), movement.y, Input.GetAxis("Vertical"));//sets movement x and z axis OLD MOVEMENT
        movement = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        movement = movement.normalized;//normalize movement
        movement *= speed;//sets speed of movement
        controller.Move(movement * Time.deltaTime);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            //var rot = Quaternion.LookRotation(new Vector3(movement.x, 0f, movement.z));
            //character.transform.rotation = Quaternion.Slerp(character.transform.rotation, rot, smooth * Time.deltaTime);
            var rotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smooth);
            //transform.forward = movement; //sharper movements
        }

        /*if(controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }*/

        characterJump();
        characterDash();
        //cameraZoom();
    }

    void characterJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            playerVelocity.y = jump;
        }

        //playerVelocity.y = playerVelocity.y - (customGravity * Time.deltaTime);//add custom gravity to player's velocity
        playerVelocity.y = playerVelocity.y + (Physics.gravity.y * customGravity * Time.deltaTime);//add gravity to player's velocity
        controller.Move(playerVelocity * Time.deltaTime);//apply gravity to character with Move
    }

    void characterDash()
    {

        if (Input.GetButtonDown("Dash"))
        {
            currentDash = 0;
        }
        if (currentDash < maxDash)
        {
            playerVelocity = transform.forward * dashDistance;
            currentDash += stopSpeed;
        }
        else
        {
            playerVelocity.x = 0;
            playerVelocity.z = 0;
        }
        controller.Move(playerVelocity * Time.deltaTime);//apply gravity to character with Move
    }
}

    /*[SerializeField]
    public float speed = 4;
    [SerializeField]
    public float rotationSpeed = 150;
    [SerializeField]
    public float moveDelay = 0.1f;

    float horizontal;
    float vertical;

    Quaternion rotation;
    Rigidbody rigidB;

    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.rotation;
        rigidB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(vertical) > moveDelay)//movement delay
        {
            rigidB.velocity = transform.forward * vertical * speed;
        }
        else
        {
            rigidB.velocity = Vector3.zero;
        }

        if (Mathf.Abs(horizontal) > moveDelay)//movement delay
        {
            rotation *= Quaternion.AngleAxis(rotationSpeed * horizontal * Time.deltaTime, Vector3.up);
        }

        transform.rotation = rotation;

    }

    void FixedUpdate()
    {
        
    }*/
