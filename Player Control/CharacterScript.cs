using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    float speed = 6f;
    float rotationSpeed = 70f;
    float gravity = 8f;
    float rotation = 0f;
    float jumpSpeed = 10f;
    float chargeTimeMax = 1f;
    float chargeTimeCurrent;
    float chargeTopSpeed = 20f;
    float chargeStopSpeed = 0.1f;

    Vector3 direction = Vector3.zero;
    
    CharacterController controller;
    Rigidbody rb;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        chargeTimeCurrent = chargeTimeMax;

        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    //moving
    //charging
    //jumping
    void Movement()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                animator.SetBool("running", true);
                animator.SetInteger("condition", 1);
                direction = new Vector3(0, 0, 1);
                direction *= speed;
                direction = transform.TransformDirection(direction);
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                animator.SetBool("running", false);
                animator.SetInteger("condition", 0);
                direction = new Vector3(0, 0, 0);
            }

            if (Input.GetKey(KeyCode.S))
            {
                animator.SetBool("running", true);
                animator.SetInteger("condition", 1);
                direction = new Vector3(0, 0, -1);
                direction *= speed;
                direction = transform.TransformDirection(direction);
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                animator.SetBool("running", false);
                animator.SetInteger("condition", 0);
                direction = new Vector3(0, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("jumping", true);
                animator.SetInteger("condition", 1);
                direction = new Vector3(0, 3, 1);
                direction *= speed;
                direction = transform.TransformDirection(direction);
                rb.AddForce(direction * jumpSpeed, ForceMode.Impulse);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) || (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.LeftShift)))
            {
                chargeTimeCurrent = 0f;

                while (chargeTimeCurrent < chargeTimeMax)
                {
                    animator.SetBool("charging", true);
                    animator.SetInteger("condition", 2);
                    direction = new Vector3(0, 0, 1);
                    direction *= chargeTopSpeed;
                    direction = transform.TransformDirection(direction);
                    chargeTimeCurrent += chargeStopSpeed;
                    Debug.Log(chargeTimeCurrent);
                }

                if (chargeTimeCurrent == chargeTimeMax)
                {
                    animator.SetBool("charging", false);
                    animator.SetBool("running", true);
                    animator.SetInteger("condition", 1);
                    direction = new Vector3(0, 0, 1);
                    direction *= speed;
                    direction = transform.TransformDirection(direction);
                }
            }
        }
        rotation += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rotation, 0);

        direction.y -= gravity * Time.deltaTime;
        controller.Move(direction * Time.deltaTime);
    }

    //check input
    void GetInput()
    {
        if (controller.isGrounded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(animator.GetBool ("moving") == true)
                {
                    animator.SetBool("moving", false);
                    animator.SetInteger("condition", 0);//attack while moving?
                }
                if(animator.GetBool ("moving") == false)
                {
                    Attack();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                
            }
        }
    }

    //attacking
    void Attack()
    {

    }
    
}
