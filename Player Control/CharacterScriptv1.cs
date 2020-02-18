using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Cinemachine;
using UnityEngine.SceneManagement;


public class CharacterScriptv1 : MonoBehaviour
{
    
    
    //Player States. More can be added
    public enum playerState
    {
        outOfCombat,
        inCombat,
        falling,
        pause,
        death,
        inspecting
    }

    public playerState currentState;
    public playerState previousState;
    private float lastStateChange = 0.0f;
    GameManager gm;

    //function to set the current state
    public void setCurrentState(playerState state)
    {
        currentState = state;
        lastStateChange = Time.time;
    }


    // end of player statestructure

    //////////PLAYER MOVEMENT VARIABLES////////////
    public float speed = 5f; //player speed
    public float gravity = 8f; //player gravity
    public float jump = 5f; //player jump height velocity
    public float smooth = 2f;//smooth the rotation
    public float rotation = 0.3f;//rotation speed
    public const float maxDash = 3f;//distance of dash
    public float dashDistance = 8f;//speed of dash 
    public float groundDistance = 0.1f;
    public float stopSpeed = 0.1f;//slow down speed
    public float currentDash = maxDash;
    private float dashTimer = 0f;
    TrailRenderer dashTrail;

    //player objects and attack type
    public int typeOfAttack; //which attack is player using?
    public GameObject knife;
    public LayerMask groundLayer;
    public Transform pivot;
    public Animator animator;
    public Animation anim;
    public bool isMove = true;

    float animationSmoothTime = 0.1f;
    NavMeshAgent agent;
    int dashDelay = 1;
    int[] timer = new int[1];

    private CinemachineFreeLook playerCam;
    public CharacterController controller;//character controller
    private Vector3 playerVelocity;//player velocity
    private Vector3 movement;//player movement

    private bool walk;
    private bool inHand;
    private bool pickUpCheck;

    //bools used for state switching
    private bool canJump;
    private bool isGrounded;
    private bool canAttack;
    private bool canMove;
    private bool canDash;
    private bool atkAnimation;
    private bool triggered;
    Collider other;

    //references to Boss Health  And Music Canvas upon Trigger
    public GameObject FBoss;
    public GameObject BossHealth;
    public AudioSource audioBGM;
    public AudioSource audioBossMusic;
    public AudioSource BossIntro;
    public Animator musicAnim_1;
    public Animator musicAnim_2;
    public bool hasTriggeredBoss;

    //references to Notes in game
    public GameObject Note;
    public GameObject ChildNote;
    private bool InspectingNote;
    Animator note_Anim;
    public bool rangeOfNote;
    private float noteTimer;  // keeps track of time in note range
    private float timeWithinNote; //min amount needed in zone to inspect


    void Awake()
    {
        //initializing  initial state (Out of Combat)
        setCurrentState(playerState.outOfCombat);
        previousState = currentState;

        // references settings for boss health bar
        FBoss = GameObject.Find("FinalBossV2");
        BossHealth = FBoss.transform.GetChild(2).gameObject;
        hasTriggeredBoss = false;

        Note = GameObject.Find("Canvas");
        InspectingNote = false;
        timeWithinNote = 0.25f; 

    }

    // Start is called before the first frame update
    void Start()
    {
        musicAnim_2.SetTrigger("FadeOut");

        typeOfAttack = 0;
        controller = GetComponent<CharacterController>();
        playerCam = GetComponent<CinemachineFreeLook>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        //isMove = true;
        walk = false;
        inHand = true;
        pickUpCheck = true;
        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
        atkAnimation = true;
        canJump = true;
        canAttack = false;
        canMove = true;
        canDash = true;
        dashTrail = GetComponentInChildren<TrailRenderer>();
        dashTrail.emitting = false;
        if (GetComponent<CharacterController>().isGrounded)
        {
            isGrounded = true;
        } else
        {
            isGrounded = false;
        }
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //rangeOfNote = false; //if Character is within range of Notes for hover display

        //switch for player states
        switch(currentState)
        {
            case playerState.outOfCombat:
                previousState = currentState;
                canMove = true;
                canDash = true;
                canAttack = true;
                isGrounded = true;
                canJump = true;
                break;

            case playerState.inCombat:
                previousState = currentState;
                canMove = true;
                canAttack = true;
                isGrounded = true;
                canJump = false;
                break;

            case playerState.falling:
                canMove = true;
                canAttack = false;
                isGrounded = false;
                canJump = false;
                break;

            case playerState.pause:
                canMove = false;
                canAttack = false;
                canJump = false;
                canDash = false;
                if (GetComponent<CharacterController>().isGrounded)
                {
                    isGrounded = true;
                }
                else
                {
                    isGrounded = false;
                }
                break;

            case playerState.inspecting:
                canAttack = false;
                canJump = false;
                canDash = false;
                break;
        }

        if (isMove && canMove && atkAnimation)
        {
            //float horizontal = Input.GetAxis("Horizontal");
            //float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            //playerCam.m_XAxis.Value = Input.GetAxis("RightAxis X");
            //playerCam.m_YAxis.Value = Input.GetAxis("RightAxis Y");


            animator.SetFloat("InputX", horizontal);
            animator.SetFloat("InputY", vertical);

            //movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            movement = (transform.forward * vertical) + (transform.right * horizontal);
            if (movement.sqrMagnitude > 1f)
            {
                movement = movement.normalized;//normalize movement
            }
            movement *= speed;//sets speed of movement

            controller.Move(movement * Time.deltaTime);

            if (movement != Vector3.zero)
            {
                transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
                //Quaternion rot = Quaternion.LookRotation(new Vector3(movement.x, 0f, movement.z));
                //transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotation * Time.deltaTime);
            }

            if (controller.isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            characterAttack();
            characterWalk();
            characterJump();
            characterDash();
            characterPickUp();
        } else
        {
            Debug.Log("Stopped Movement!");
            Debug.Log("isMove: " + isMove);
            Debug.Log("canMove: " + canMove);
            controller.Move(new Vector3(0, 0, 0));
        }

        if (triggered && !other)
        {
            setCurrentState(playerState.outOfCombat);
            triggered = false;

        } 

        if(canDash == false)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer >= 0.08f)
            {
                canDash = true;
                dashTimer = 0f;
            }
        }
    }

    // used to deactive the Boss health bar animation once it is performed
    // this is used to fix the bug associated with health not depleting with active animator component.
    private IEnumerator bossHealthAnim ()
    {
        yield return new WaitForSeconds(4);
        Animator a = BossHealth.GetComponentInChildren<Animator>();
        a.enabled = false;

    }

    //Zone trigger to display Boss's Health Bar
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BossZone" && hasTriggeredBoss == false)
        {
            hasTriggeredBoss = true;
            BossHealth.SetActive(true);
            StartCoroutine(bossHealthAnim());
            StartCoroutine(BossMusic());


  
        }

        /*else if (other.gameObject.tag == "Health")
        {
            pickupSound.Play();
            recoverAmount = Random.Range(3.0f, 7.0f);
            playerHealth.healDamage(recoverAmount);
            Destroy(other.gameObject);
        }*/

        else if (other.gameObject.tag == "Note")
        {
            rangeOfNote = true;
            setCurrentState(playerState.inspecting);
            previousState = currentState;
            noteTimer = 0;
        }

    }

    private void OnTriggerStay(Collider range)
    {
        if (range.tag == "EnemyRange" )
        {
            //triggered = true;
            other = range;
            setCurrentState(playerState.inCombat);
            
        }


        //inspecting note 1
        if (range.name == "Note1")
        {
            noteTimer += Time.deltaTime;
            if (Input.GetButtonUp("Dash_New") && noteTimer >= timeWithinNote)
            {

                if (InspectingNote == false)
                {
                    ChildNote = Note.transform.GetChild(4).gameObject;
                    note_Anim = ChildNote.GetComponent<Animator>();
                    ChildNote.SetActive(true);
                    StartCoroutine(openInspectingNote());


                }
                else if (InspectingNote == true)
                {
                    note_Anim.SetTrigger("Shrink");
                    StartCoroutine(exitInspectingNote());
                    setCurrentState(playerState.outOfCombat);

                }
            }
        }
        //inspecting note 2
        else if (range.name == "Note2")
        {
            noteTimer += Time.deltaTime;
            if (Input.GetButtonUp("Dash_New") && noteTimer >= timeWithinNote)
            {
                if (InspectingNote == false)
                {
                    ChildNote = Note.transform.GetChild(5).gameObject;
                    note_Anim = ChildNote.GetComponent<Animator>();
                    ChildNote.SetActive(true);
                    StartCoroutine(openInspectingNote());


                }
                else if (InspectingNote == true)
                {
                    note_Anim.SetTrigger("Shrink");
                    StartCoroutine(exitInspectingNote());
                    setCurrentState(playerState.outOfCombat);

                }
            }
        }
        //inspecting note 3
        else if (range.name == "Note3")
        {
            noteTimer += Time.deltaTime;
            if (Input.GetButtonUp("Dash_New") && noteTimer > timeWithinNote)
            {
                if (InspectingNote == false)
                {
                    ChildNote = Note.transform.GetChild(6).gameObject;
                    note_Anim = ChildNote.GetComponent<Animator>();
                    ChildNote.SetActive(true);
                    StartCoroutine(openInspectingNote());


                }
                else if (InspectingNote == true)
                {
                    note_Anim.SetTrigger("Shrink");
                    StartCoroutine(exitInspectingNote());
                    setCurrentState(playerState.outOfCombat);

                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "EnemyRange")
        {
            triggered = false;
            setCurrentState(playerState.outOfCombat);
        }

        else if (other.gameObject.tag == "Note")
        {
            rangeOfNote = false;
            noteTimer = 0;
            setCurrentState(playerState.outOfCombat);

            if (InspectingNote == true)
            {
                note_Anim.SetTrigger("Shrink");
                StartCoroutine(exitInspectingNote());
            }
        }

    }

    //triggers opening note
    private IEnumerator openInspectingNote()
    {
        yield return new WaitForSeconds(1f);
        InspectingNote = true;
    }

    // triggers closing note
    private IEnumerator exitInspectingNote()
    {
        yield return new WaitForSeconds(1f);
        ChildNote.SetActive(false);
        InspectingNote = false;
    }

    // Que Boss music
    private IEnumerator BossMusic()
    {
        musicAnim_1.SetTrigger("FadeOut");
        BossIntro.Play();
        musicAnim_2.Play("FadeIn");
        audioBossMusic.Play();
        yield return new WaitForSeconds(3.0f);
        audioBGM.Stop();
    }

    //remove?
    IEnumerator deathTime()
    {
        yield return new WaitForSeconds(2f);
    }

    //character jump not used, but gravity is set on the player here 
    void characterJump()
    {
        //Debug.Log(isGrounded());
        if (isGrounded && Input.GetButtonDown("Jump") && canJump)//sphere cast
        {
            //playerVelocity.y = jump;
            previousState = currentState;
            setCurrentState(playerState.falling);
        }

        playerVelocity.y = playerVelocity.y - (gravity * Time.deltaTime);//add gravity to player's velocity
        controller.Move(playerVelocity * Time.deltaTime);//apply gravity to character with Move

        if (GetComponent<CharacterController>().isGrounded && isGrounded == false)
        {
            currentState = previousState;
        }
  
    }

    void characterWalk()
    {
        /*if (canMove)
        {
            if (Input.GetButton("Walk"))
            {
                walk = true;
                speed = 3;
                animator.SetBool("Walk", walk);
            }
            else
            {
                walk = false;
                speed = 5;
                animator.SetBool("Walk", walk);
            }
        }*/
    }

    void characterAttack()
    {

        Debug.Log("Attack animation finished?: " + atkAnimation);
        Debug.Log("Attack animation not finished?: " + !atkAnimation);

        if (canAttack && atkAnimation)
        {
            gm.playerAttacked = false;
            if (Input.GetButtonDown("Heavy Attack_New"))
            {
                gm.playerAttacked = true;
                typeOfAttack = 2;
                animator.SetTrigger("attack_overhead");
                atkAnimation = false;
                
            }

            if (Input.GetButtonDown("Stab Attack_New"))
            {
                gm.playerAttacked = true;
                typeOfAttack = 1;
                animator.SetTrigger("attack_stab");
                atkAnimation = false;
                
            }

            if (Input.GetButtonDown("Slash Attack_New"))
            {
                gm.playerAttacked = true;
                typeOfAttack = 3;
                animator.SetTrigger("attack_slash");
                atkAnimation = false;
                
            }
        }

        /*if (!anim.IsPlaying("Attack_Overhead"))
        {
            atkAnimation = true;
        }

        if (!anim.IsPlaying("Attack_Slash"))
        {
            atkAnimation = true;
        }
        if (!anim.IsPlaying("Attack_Stab"))
        {
            atkAnimation = true;
        }*/

        /*if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Overhead"))
        {
            atkAnimation = true;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Slash"))
        {
            atkAnimation = true;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Stab"))
        {
            atkAnimation = true;
        }*/
    }

    void OnAnimatorMove()
    {
        if (animator)
        {
            controller.Move(transform.forward * animator.GetFloat("AtkSpeed") * Time.deltaTime);
        }
    }

    public void AnimationStopEvent()
    {
        atkAnimation = true;
    }


    void characterDash()
    {

        
          
        if (canMove && canDash)
        {
            //bool delay = true;

            if (Input.GetButtonDown("Dash_New"))
            {
                currentDash = 0;
                
            }

            //front
            if (Input.GetAxisRaw("Vertical") > 0 && Input.GetButtonDown("Dash_New") && delay())
            {
                if (currentDash < maxDash)
                {
                    playerVelocity = transform.forward * dashDistance * 5;
                    //controller.Move(playerVelocity * Time.deltaTime);
                    //Vector3 start = transform.position;
                    //Vector3 finish = transform.position + playerVelocity;
                    //float percentage = currentLerpTime / lerpTime;
                    //percentage = Mathf.Sin(percentage * Mathf.PI * 0.5f);
                    //transform.position = Vector3.Lerp(start, finish, percentage);
                    currentDash += stopSpeed;
                    canDash = false;
                    dashTrail.emitting = true;
                    Invoke("StopTrail", 0.3f);
                }
                StartCoroutine("TimeEnumerator");
                
                /*playerVelocity = transform.forward * dashDistance * 5;
                Vector3 start = transform.position;
                Vector3 finish = transform.position + playerVelocity;
                float currentLerpTime = 0;
                float lerpTime = 3f;
                currentLerpTime += Time.deltaTime;
                if(currentLerpTime >= lerpTime)
                {
                    currentLerpTime = lerpTime;
                }
                float percentage = currentLerpTime / lerpTime;
                percentage = percentage * percentage * percentage * (percentage * (6f * percentage - 15f) + 10f);
                transform.position = Vector3.Lerp(start, finish, percentage);
                canDash = false;
                StartCoroutine("TimeEnumerator");*/
            }

            //back
            else if (Input.GetAxisRaw("Vertical") < 0 && Input.GetButtonDown("Dash_New") && delay())
            {
                /*if (currentDash < maxDash)
                {
                    playerVelocity = transform.forward * -1 * dashDistance * 5;
                    //Vector3 start = transform.position;
                    //Vector3 finish = transform.position + playerVelocity;

                    //float currentLerpTime = Time.time;
                    //float lerpTime = 1f;
                    //float percentage = currentLerpTime / lerpTime;
                    //percentage = Mathf.Sin(percentage * Mathf.PI * 0.5f);
                    //transform.position = Vector3.Lerp(start, finish, percentage);
                    currentDash += stopSpeed;
                    canDash = false;
                }
                StartCoroutine("TimeEnumerator");*/
            }

            //left
            else if (Input.GetAxisRaw("Horizontal") < 0 && Input.GetButtonDown("Dash_New") && delay())
            {
                if (currentDash < maxDash)
                {
                    playerVelocity = transform.right * -1 * dashDistance * 5;
                    //Vector3 start = transform.position;
                    //Vector3 finish = transform.position + playerVelocity;

                    //float currentLerpTime = Time.time;
                    //float lerpTime = 1f;
                    //float percentage = currentLerpTime / lerpTime;
                    //percentage = Mathf.Sin(percentage * Mathf.PI * 0.5f);
                    //transform.position = Vector3.Lerp(start, finish, percentage);
                    currentDash += stopSpeed;
                    canDash = false;
                    dashTrail.emitting = true;
                    Invoke("StopTrail", 0.3f);
                }
                StartCoroutine("TimeEnumerator");
            }

            //right
            else if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetButtonDown("Dash_New") && delay())
            {
                if (currentDash < maxDash)
                {
                    playerVelocity = transform.right * dashDistance * 5;
                    //Vector3 start = transform.position;
                    //Vector3 finish = transform.position + playerVelocity;

                    //float currentLerpTime = Time.time;
                    //float lerpTime = 1f;
                    //float percentage = currentLerpTime / lerpTime;
                    //percentage = Mathf.Sin(percentage * Mathf.PI * 0.5f);
                    //transform.position = Vector3.Lerp(start, finish, percentage);
                    currentDash += stopSpeed;
                    canDash = false;
                    dashTrail.emitting = true;
                    Invoke("StopTrail", 0.3f);
                }
               StartCoroutine("TimeEnumerator");
            }
            else
            {
                playerVelocity.x = 0;
                playerVelocity.z = 0;
            }
            controller.Move(playerVelocity * Time.deltaTime);//apply gravity to character with Move
        }
        

    }

    IEnumerator TimeEnumerator()
    {
        
        for(int i = 0; i < dashDelay; i++)
        {
            timer[0] = -1;
            yield return new WaitForSeconds(1);
            Debug.Log("dash delay: " + timer[0]++);
            
        }
        
        timer[0] = 0;
    }

    void StopTrail()
    {
        dashTrail.emitting = false;
    }

    private bool delay()
    {
        if(timer[0] == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void characterPickUp()
    {
        /*if (Input.GetButtonDown("Pickup"))
        {
            animator.SetTrigger("pickup");
        }*/
    }

    public void PickupEvent()
    {
        Debug.Log("pickup called");
        /*if (pickUpCheck == false)
        {
            inHand = true;
            animator.SetBool("handBool", inHand);
            Debug.Log("closed hand");

        }
        else
        {
            inHand = false;
            animator.SetBool("handBool", inHand);
            Debug.Log("open hand");
        }*/
    } 

    /*private IEnumerator OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Pickup") && pickUpCheck)
        {
            yield return new WaitForSeconds(3f);
        }

    }*/


    //player particle collisions
    private void OnParticleCollision(GameObject col)
    {
        //deal damage
        Debug.Log("Particle Hit!");
    }
}