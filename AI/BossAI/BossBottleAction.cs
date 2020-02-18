using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/BossBottle")]
public class BossBottleAction : Action
{
    float random;
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {
        controller.walking = false;
        controller.anim.SetBool("Walk", false);
        Debug.Log("IN BOSS BOTTLE ATTACK");
        if (controller.canMove)
        {
            
            if (!controller.isAttacking)
            {
                FaceTarget(controller);
                
                //40% chance to attack, since attacking is set to true a timer will start in StateController.cs
                random = Random.Range(0f, 10.0f);
                Debug.Log(random);
                if(random >= 6f && controller.canAttack)
                {
                    //Debug.Log("Throwing");
                    controller.anim.SetTrigger("Throw");
                    //controller.bottle.LaunchProjectile();
                    controller.isAttacking = true;
                }
                

            }
            else
            {
                FaceTarget(controller);

                if (controller.generalTimer(2f))
                {
                    controller.isAttacking = false;
                    controller.canAttack = false;
                    controller.canMove = true;

                    //controller.doneThrowing = true;
                }
                else
                {
                    if (controller.generalTime > 2f / 2 )
                    {
                        controller.bottle.LaunchProjectile();
                        Debug.Log("Launch bottle");
                    }
                }
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
