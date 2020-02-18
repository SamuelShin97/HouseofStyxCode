using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool playerAttacked;
    public static float playerHealth;
    public List<GameObject> attackers;
    public List<GameObject> allEnemies;
    public List<GameObject> screamerPoints;
    public List<GameObject> distanceTier1;
    public List<GameObject> distanceTier2;
    public List<GameObject> bossEnemiesSpawned;
    public int attackerLimit;
    public int attackerCount;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        playerAttacked = false;
        playerHealth = 100f;
        attackerLimit = 2;

        //gets the game objects for all the enemies and all the screamer teleport points
        /*foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {

            allEnemies.Add(enemy);
        }*/

        player = GameObject.FindGameObjectWithTag("Player");

         foreach(GameObject point in GameObject.FindGameObjectsWithTag("ScreamerPoints")) {
 
             screamerPoints.Add(point);
         }
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void slowTime(float duration, float speed)
    {  //1 = normal speed, 2 = double speed, 0 = standstill. 0.5f = half speed etc
        Time.timeScale = speed;
        StartCoroutine(resumeNormalSpeed(duration));
}

    private IEnumerator resumeNormalSpeed(float duration)
    {
        yield return new WaitForSeconds(duration);
        Time.timeScale = 1f;
    }


    //keep track of enemies that want to attack the player and make sure it isnt above the max
    public void requestAttack(GameObject requestor)
    {
        attackers.RemoveAll(item => item == null);

        if(attackers.Count <= attackerLimit)
        {
            if (!attackers.Contains(requestor))
            {
                //Debug.Log("added enemy: " + requestor);
                attackers.Add(requestor);
            }
            requestor.SendMessage("onAllowAttack");
        }
    }

    //function for the AI to call when done attacking to remove from the list
    public void onCancelAttack(GameObject requestor)
    {
        attackers.Remove(requestor);
    }

    //Called by the AI when in range closest to the player
    //makes sure there is a max number of enemies crowding the player
    public void requestDistanceTier1(GameObject requestor)
    {
        distanceTier1.RemoveAll(item => item == null);

        if (distanceTier1.Count < 3)
        {
            if (!attackers.Contains(requestor))
            {
                //Debug.Log("added enemy to distance1: " + requestor);
                distanceTier1.Add(requestor);
                if (distanceTier2.Contains(requestor))
                {
                    onCancelDistance2(requestor);
                }
            }
            requestor.SendMessage("onAllowDistance1");
        }
        else if (distanceTier2.Contains(requestor))
        {
            float distanceFromPlayer = Vector3.Distance(requestor.transform.position, player.transform.position);
            for (int i = 0; i < distanceTier1.Count; i++)
            {
                float dist = 0f;
                GameObject enemy = distanceTier1[i];
                dist = Vector3.Distance(enemy.transform.position, player.transform.position);
                if (distanceFromPlayer < dist)
                {
                    onCancelDistance1(enemy);
                    distanceTier1.Add(requestor);
                    onCancelDistance2(requestor);
                    requestDistanceTier2(enemy);
                    requestor.SendMessage("onAllowDistance1");
                    break;
                }
            }   
        }
        




    }
    //called after the enemy leaves range closest to player
    public void onCancelDistance1(GameObject requestor)
    {
        distanceTier1.Remove(requestor);
    }


    //Called by the AI when in range behind closest to the player
    //makes sure there is a max number of enemies crowding the player
    public void requestDistanceTier2(GameObject requestor)
    {
        distanceTier2.RemoveAll(item => item == null);

        //if (distanceTier2.Count <= 6)
        //{
            if (!distanceTier2.Contains(requestor))
            {
                //Debug.Log("added enemy to distance2: " + requestor);
                distanceTier2.Add(requestor);
                if (distanceTier1.Contains(requestor))
                {
                    onCancelDistance1(requestor);
                }

            }
            requestor.SendMessage("onAllowDistance2");
        //}
    }

    //called after the enemy leaves range behind closest to player
    public void onCancelDistance2(GameObject requestor)
    {
        distanceTier2.Remove(requestor);
    }

    //check number of enemies boss spawned
    public int bossSpawnedEnemiesCount()
    {
        bossEnemiesSpawned.RemoveAll(item => item == null);
        return bossEnemiesSpawned.Count;
    }

    //add enemies spawned by boss into list
    public void bossAddSpawnedEnemy(GameObject requestor)
    {
        bossEnemiesSpawned.Add(requestor);
    }

    //remove enemies spawned by boss in list
    public void bossRemoveSpawnedEnemy(GameObject requestor)
    {
        bossEnemiesSpawned.Remove(requestor);
    }
}
