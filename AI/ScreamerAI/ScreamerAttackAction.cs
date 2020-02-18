using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/ScreamerAttack")]
public class ScreamerAttackAction : Action
{
    float random;
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {

        //if not attacking, check if the AI should attack
        if (!controller.isAttacking)
        {

            //if the AI took damage (about 1/4 of health), teleport
            float currentHealth = controller.health._health;
            if ((controller.oldHealth - currentHealth) >= controller.startingHealth/4)
            {
                controller.tpTime = true;
            }


            FaceTarget(controller);

            //timer to teleport if not attacked
            controller.screamerTime += Time.deltaTime;
            if (controller.screamerTime >= controller.screamerTimerTime)
            {

                controller.screamerTime = 0;
                controller.scream.Stop();
                controller.scream1.Stop();
                controller.scream2.Stop();
                controller.scream3.Stop();
                controller.tpTime = true;

            }


            //40% chance to attack, since attacking is set to true a timer will start in StateController.cs
            random = Random.Range(0f, 10.0f);
            if (controller.generalTimer(4f) && random >= 4.0f)
            {
                controller.sa.ScreamAttack();
                controller.scream.Play();
                controller.scream1.Play();
                controller.scream2.Play();
                controller.scream3.Play();
                controller.isAttacking = true;
                //controller.screamerCanAttack = false;
            }
        }
        else
        {
            //if attacking

            //if the player is withing the scream wave range then damage the player
            float distance = Vector3.Distance(controller.chaseTarget.position, controller.transform.position);

            if (distance < controller.minRadius)
            {
                //damage player very little and maybe slow down
                // damage/distance
                controller.playerHealth.ApplyDamage(0.15f / distance, controller.enemyType);
            }
            else
            {

                //if the player is not in range keep track of how many cosecutive attacks miss
                if (!controller.screamerNoDamage)
                {
                    //if the screamer did not hit the player, increase counter that can send screamer to player (checked in MovementAction)
                    controller.screamerPlayerNotDamagedCounter++;
                    controller.screamerNoDamage = true;
                }
            }
            FaceTarget(controller);

            //after the timer for attacking is over, revert booleans and stop particles
            if (controller.generalTimer(3f))
            {
                controller.isAttacking = false;
                controller.canAttack = false;
                controller.canMove = false;

                controller.scream.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                controller.scream1.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                controller.scream2.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                controller.scream3.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
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
