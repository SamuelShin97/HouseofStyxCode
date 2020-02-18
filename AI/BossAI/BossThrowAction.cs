using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/BossThrow")]
public class BossThrowAction : Action
{
    float random;

    public override void Act(StateController controller)
    {
        Track(controller);
    }

    private void Track(StateController controller)
    {
        controller.walking = false;
        controller.anim.SetBool("Walk", false);
        if (!controller.isAttacking)
        {
            FaceTarget(controller);

            //40% chance to attack, since attacking is set to true a timer will start in StateController.cs
            random = Random.Range(0f, 10.0f);
            //Debug.Log(random);
            if (controller.generalTimer(2f) && random >= 3f)
            {
                Debug.Log("boss throwing");
                controller.anim.SetTrigger("Throw");
                
                controller.playAnim = false;
                controller.isAttacking = true;
            }



        }
        else
        {
            FaceTarget(controller);

            if(controller.generalTimer2(1f)){
                controller.bottle.LaunchProjectile();
                controller.isAttacking = false;
            }
            //get position to the 315 degrees
        }
        float currentHealth = controller.health._health;
        //if the player killed all the enemies the boss spawned then change state and go attack the player
        if ((controller.gm.bossSpawnedEnemiesCount() <= 0 && controller.doneSpawning)|| (controller.oldHealth - currentHealth) >= 5 || currentHealth < controller.startingHealth/6)
        {
            Debug.Log("done throwing");
            controller.doneSpawning = false;
            controller.isAttacking = false;
            controller.doneThrowing = true;
        }
    }

    //get direction to target, get rotation to point to target, then update rotation
    void FaceTarget(StateController controller)
    {

        Vector3 direction = (controller.chaseTarget.position - controller.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
