using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/BossAttack")]
public class BossAttackAction : Action
{
    float random;
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {


        if (!controller.isAttacking)
        {
            controller.walking = false;
            controller.anim.SetBool("Walk", false);
            FaceTarget(controller);

            //40% chance to attack, since attacking is set to true a timer will start in StateController.cs
            random = Random.Range(0f, 10.0f);
            if ((random >= 2f && controller.generalTimer2(1f)) || controller.firstAttack)
            {
                Debug.Log("attacking");
                controller.vertical = 0;
                random = Random.Range(0f, 10.0f);
                float distance = Vector3.Distance(controller.chaseTarget.position, controller.transform.position);
                if(distance >= (controller.navMeshAgent.stoppingDistance - 1))
                {
                    if(random >= 5f)
                    {
                        controller.anim.SetTrigger("Slash");
                        Debug.Log("Slash");
                    }
                    else
                    {
                        controller.anim.SetTrigger("SweepFinish");
                    }
                    
                }
                else
                {
                    
                    if(random >= 5f)
                    {
                        controller.anim.SetTrigger("Sweep");
                        Debug.Log("Sweep");
                    }
                    else
                    {
                        controller.anim.SetTrigger("Punch");
                    }
                    
                }
                
                controller.playAnim = false;
                controller.canMove = false;
                controller.isAttacking = true;
                controller.firstAttack = false;
            }


        }
        else
        {
            //FaceTarget(controller);

            if (controller.generalTimer(2f))
            {
                controller.isAttacking = false;
                controller.canAttack = false;
                controller.canMove = true;

            }
            else
            {
                /*
                    if (controller.generalTime > 2f / 2 )
                    {
                        controller.bottle.LaunchProjectile();
                        //Debug.Log("Launch bottle");
                    }
                    */
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
