using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using Complete;

public class StateController : MonoBehaviour {

    //state controller variables
    public State currentState;
	public State remainState;
    [HideInInspector] public bool playAnim;                         //bool to play attack animation in attack action
    //[HideInInspector] public bool exitIdle;                        
    [HideInInspector] public Animator anim;                         //AIs animator reference
    public float vertical;                                          //vertical value for the movement speed of the navmesh
    public float lookRadius = 25f;                                  //AIs farthest sight range for player
    public float minRadius = 1f;                                    //AIs stopping distance for the player
    public float distance2min = 3f;                                 //the minimum distance for the AI have to keep their distance from the player
    public float distance2max = 4f;                                 //the maximim distance for the AI have to keep their distance from the player
    public float distanceToPlayer;                                  //distance to player
    [HideInInspector] public BS_Main_Health health;                 //AIs health variable
    [HideInInspector] public float timerTime = 2f;                         //variable for a timer limit of 2
    [HideInInspector] public float time;                                   //variable to keep track of current timer time

    [HideInInspector] GameObject player;                            // Reference to the player GameObject.
    [HideInInspector] public BS_Main_Health playerHealth;           // Reference to the player's health.

    [HideInInspector] GameObject gameManager;                       //reference to the game manager game object
    [HideInInspector] public GameManager gm;                        //reference to the game manager script

    [HideInInspector] public bool keepDistance;                     //bool for when there are too many enemies around the AI
     public bool firstAttack;


    //attack state variables
    [HideInInspector] public bool canMove;                          //AI can move after an attack
    [HideInInspector] public bool isAttacking;                      //AI is playing attack animation
    [HideInInspector] public bool canAttack;                        //Ai can attack (not currently attacking or during cooldown)
    [HideInInspector] public bool strafe;                           //cariable to go to the strafe state
    [HideInInspector] public bool tookDamage;                       //AI took damage

    //strafe state variables
    [HideInInspector] public bool finishedStrafe;                   //AI is done strafing and can return to chasing the player
    [HideInInspector] public bool strafeTimeUp;                     //done strafing and can go to next state
    [HideInInspector] public Vector3 strafeTarget;                  //location either to the left of right of the AI that they can strafe to
    [HideInInspector] public float strafeTimerLimit = 3f;

    public bool allowDistance1;                                     //AI allowed in attacking distance of the player
    public bool allowDistance2;                                     //AI allowed to surround the player from a small distance
    [HideInInspector] public NavMeshAgent navMeshAgent;             //reference the the AIs navmesh agent
    [HideInInspector] public Transform chaseTarget;                 //transform of the player
    [HideInInspector] public bool fromCombat;                       //bool for when the player leaves the AIs attack range

    //screamer variables
    [HideInInspector] public List<Transform> screamPointsInRange;   //list of points the screamer can teleport to
    [HideInInspector] public bool tpTime;                           //bool to tell the screamer to go to the teleport state
    [HideInInspector] public bool teleported;                       //after teleporting
    public ParticleSystem scream;                                   //particles for scream attack
    public ParticleSystem scream1;
    public ParticleSystem scream2;
    public ParticleSystem scream3;
    public Transform[] screamPoints = new Transform[5];             //points in arena where screamer can teleport
    public float screamerTimerTime = 10f;                           //time before screamer teleports
    public float screamerTime;                                      //variable to count until it is time to teleport
    //[HideInInspector] public float screamerAttackTimerTime = 20f;
    //[HideInInspector] public float screamerAttackTime;
    //[HideInInspector] public bool screamerCanAttack;                
    [HideInInspector] public bool screamerNoDamage;                 //bool for if the screamer hasnt damaged the player recently
    [HideInInspector] public int screamerPlayerNotDamagedCounter;   //counter for how many attacks didnt hit player 

    //boss variables
    [HideInInspector] public bool doneThrowing;                     //after bosses bottle throw
    [HideInInspector] public bool lostEnoughHealth;                 //if the boss has lost enough health to change state
    [HideInInspector] public bool doneSpawning;                     //if the boss has lost enough health to change state
    [HideInInspector] public bool walking;                          //bool for to set for when the boss is walking (for particles)
    [HideInInspector] public float oldHealth;                       //the amount of health when checked or changed state last
    [HideInInspector] public float startingHealth;                  //the amount of health the AI started with 
    [HideInInspector] public BottleProjectile bottle;               //bottle game object
    //[HideInInspector] public List<GameObject> bossMinions;          //list of enemies boss has spawned
    [HideInInspector] public BoxCollider boxCollider;               //box collider for boss
    public Transform bottleThrowPosition;                           //where to spawn bottle
    public Transform throwPosition;                                 //position where boss throws bottles
    public GameObject bossGrunt;                                    //boss grunt prefab that can be spawned
    public ParticleSystem spawningParticle;                         //particles for when the grunt spawns
    public ParticleSystem spawningParticle2;
    public Transform centerOfBossArena;                             //center of boss fight arena so a spawn can move closser if it is blocked
    public Transform[] spawnPoints = new Transform[4];              //points in arena where grunts cant spawn past

    //variables for a general timer that can be used in multiple actions
    [HideInInspector] public float generalTime;                     //counters for general timers
     public float generalTime2;

    //call audio
    public ScreamerAudio sa;                                        //script with funtions to play audio for the screamer


    [HideInInspector] public bool isScreamer;                       //bools to tell what the enemy type is 
    [HideInInspector] public bool isGrunt;
    [HideInInspector] public bool isBoss;


    [HideInInspector] public string enemyType;

    [HideInInspector] public float timePassed = 0f;
    [HideInInspector] public float normalizedTime = 0f;
    [HideInInspector] public bool determinePosition = false;
    [HideInInspector] public Vector3 startPosition = Vector3.zero;
    [HideInInspector] public Vector3 toPosition = Vector3.zero;
    [HideInInspector] public int dir = -1;
    [HideInInspector] public float rng = -1f;
    [HideInInspector] public float dashDistance = 8f;//speed of dash 
    public bool dodge = false;

    void Awake()
    {
        //get references and set default values

        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gm = gameManager.GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<BS_Main_Health>();


        anim = GetComponentInChildren<Animator>();

        chaseTarget = player.transform;
        fromCombat = false;
        canMove = true;
        isAttacking = false;
        firstAttack = true;

        health = gameObject.GetComponent<BS_Main_Health>();

        doneThrowing = false;

        keepDistance = false;

        startingHealth = health._health;
        oldHealth = startingHealth;

        if (gameObject.name.Contains("Screamer"))
        {
            isScreamer = true;
            enemyType = "Screamer";
            timerTime = 4f;
            
        }
        else
        if (gameObject.name.Contains("Boss"))
        {
            isBoss = true;
            enemyType = "Boss";
            bottle = GetComponentInChildren<BottleProjectile>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            boxCollider = GetComponent<BoxCollider>();
            
        }
        else if(gameObject.name.Contains("Grunt"))
        {
            isGrunt = true;
            enemyType = "Grunt";
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        //call audio
        sa = GetComponent<ScreamerAudio>();
    }

    
    void Update()
    {
        //update current state
        currentState.UpdateState(this);
        
        //update grunt animation speed
        if (isGrunt)
        {
            //Debug.Log("name contains grunt " + this.gameObject);
            vertical = (navMeshAgent.velocity.magnitude / navMeshAgent.speed) / 2;
            anim.SetFloat("InputY", vertical);

            
            /*if (determinePosition == false)
            {
                rng = Random.Range(0f, 1000f);
            }
            
            if (rng < 2f) //and grunt is in range, idk the check here because they dash before player initiates combat
            {
                if (determinePosition == false)
                {
                    dir = Random.Range(0, 4);
                }
                
                
                if (dir == 0)
                {
                    if (determinePosition == false)
                    {
                        toPosition = (transform.position + transform.forward * dashDistance);
                        determinePosition = true;
                    }
                    time += Time.deltaTime;
                    normalizedTime = time / 6f;
                    transform.position = Vector3.Lerp(transform.position, toPosition, normalizedTime);
                    if (transform.position == toPosition)
                    {
                        determinePosition = false;
                        time = 0;
                        normalizedTime = 0;
                    }
                }
                else if (dir == 1)
                {
                    if (determinePosition == false)
                    {
                        toPosition = (transform.position + transform.forward * -1 * dashDistance);
                        determinePosition = true;
                    }
                    time += Time.deltaTime;
                    normalizedTime = time / 6f;
                    transform.position = Vector3.Lerp(transform.position, toPosition, normalizedTime);
                    if (transform.position == toPosition)
                    {
                        determinePosition = false;
                        time = 0;
                        normalizedTime = 0;
                    }
                }
                else if (dir == 2)
                {
                    if (determinePosition == false)
                    {
                        toPosition = (transform.position + transform.right * dashDistance);
                        determinePosition = true;
                    }
                    time += Time.deltaTime;
                    normalizedTime = time / 6f;
                    transform.position = Vector3.Lerp(transform.position, toPosition, normalizedTime);
                    if (transform.position == toPosition)
                    {
                        determinePosition = false;
                        time = 0;
                        normalizedTime = 0;
                    }
                }
                else if (dir == 3)
                {
                    if (determinePosition == false)
                    {
                        toPosition = (transform.position + transform.right * -1 * dashDistance);
                        determinePosition = true;
                    }
                    time += Time.deltaTime;
                    normalizedTime = time / 6f;
                    transform.position = Vector3.Lerp(transform.position, toPosition, normalizedTime);
                    if (transform.position == toPosition)
                    {
                        determinePosition = false;
                        time = 0;
                        normalizedTime = 0;
                    }
                }
            }
            */
        }

        if (isBoss)
        {
            Debug.Log("walking: " + walking);
        }
 
    }

    //if the next state is new then change current state
    public void TransitionToState(State nextState)
	{
		//if the next state isnt current state, change state
		if(nextState != remainState)
		{
			currentState = nextState;
		}
	}

    //draw gizmos based on current state
    void OnDrawGizmosSelected()
    {
        if (currentState.sceneGizmoColor != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.DrawWireSphere(transform.position, minRadius);
    }

    //general timers that can be used throughout states
    public bool generalTimer(float timeLimit)
    {

        generalTime += Time.deltaTime;
        if (generalTime >= timeLimit)
        {
            generalTime = 0;
            return true;
        }
        return false;
    }

    public bool generalTimer2(float timeLimit)
    {

        generalTime2 += Time.deltaTime;
        if (generalTime2 >= timeLimit)
        {
            generalTime2 = 0;
            return true;
        }
        return false;
    }

    //funtion called by game manager, determining if AI can attack
    public void onAllowAttack()
    {
        canAttack = true;
    }

    //function called by game manager, determining if the AI can be in attack range
    public void onAllowDistance1()
    {
        allowDistance1 = true;
        allowDistance2 = false;
        //Debug.Log("allowDistance1");
    }

    //function called by game manager, determining if the AI should keep their distance from the player
    public void onAllowDistance2()
    {
        allowDistance2 = true;
        allowDistance1 = false;
        //Debug.Log("allowDistance2");
    }

    //function to face the player
    public void FaceTarget(StateController controller)
    {

        Vector3 direction = (controller.chaseTarget.position - controller.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    //funtion to spawn grunts for the boss, used in BossThrowDecision.cs
    void SpawnEnemies()
    {
        //Spawn enemies at each of the 4 spawn points
        for(int i = 0; i < 4; i++)
        {
            createEnemy(i);
            createEnemy(i);
        }

        doneSpawning = true;
    }

    //create enemy at position in spawnPoints array and add to spawned enemies array to check how many are left in BossThrowAction.cs
    void createEnemy(int position)
    {
        GameObject enemySpawned = Instantiate(bossGrunt);
        enemySpawned.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(spawnPoints[position].position);
        gm.bossAddSpawnedEnemy(enemySpawned);
    }
}