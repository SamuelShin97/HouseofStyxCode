using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/GruntAttack")]
public class GruntAttackAction : Action
{
    float random;
    string[] atkNames = new string[] {"GruntAttack", "GruntComboAttack2", "GruntComboAttack3", "Grunt360Attack", "GruntJumpAttack"};
    string jumpAttack = "GruntJumpAttack";
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {

        //if (controller.canMove)
        //{

        controller.anim.SetBool("Walk", false);


        if (controller.fromCombat)
        {

            // NOT IMPLEMENTED YET


            //charge attack (increase speed till close to enemy or change to tracking state with increased speed and animation)
            controller.fromCombat = false; //after animation is complete
        }

        if (!controller.isAttacking)
        {
            //face the player and check if the AI can attack
            FaceTarget(controller);
            controller.gm.requestAttack(controller.gameObject);
            if (controller.gm.playerAttacked)
            {
                controller.gm.playerAttacked = false;
                int rng = Random.Range(0, 10);
                if (rng >= 5)
                {
                    controller.dodge = true;
                }
                
            }

            //40% chance to attack when 2f timer is up
            //since attacking is set to true a timer will start in StateController.cs

            random = Random.Range(0f, 10.0f);
            if ((controller.generalTimer(1f) && random >= 4f && controller.canAttack && controller.allowDistance1) || (controller.canAttack && controller.firstAttack && random >= 5f))
            {
                //controller.vertical = 0;
                //controller.anim.CrossFade("oh_attack_2", 0.2f);
                controller.firstAttack = false;
                //controller.anim.SetTrigger("attack_slash");
                //controller.anim.SetTrigger("GruntAttack");
                string rng = atkNames[Random.Range(0, atkNames.Length)];
                controller.anim.SetTrigger(rng);
                //controller.anim.SetTrigger(jumpAttack);
                controller.playAnim = false;
                controller.canMove = false;
                controller.isAttacking = true;
            }
            
                //after the timer
                
                
             

                    /*Debug.Log("dodged");
                    //dodge
                    if (controller.determinePosition == false)
                    {
                        controller.rng = Random.Range(0f, 1000f);
                    }

                    if (controller.rng < 2f && controller.gm.playerAttacked)
                    {
                        controller.gm.playerAttacked = false;
                        if (controller.determinePosition == false)
                        {
                            controller.dir = Random.Range(0, 4);
                        }


                        if (controller.dir == 0)
                        {
                            if (controller.determinePosition == false)
                            {
                                controller.toPosition = (controller.transform.position + controller.transform.forward * controller.dashDistance);
                                controller.determinePosition = true;
                            }
                            controller.time += Time.deltaTime;
                            controller.normalizedTime = controller.time / 6f;
                            controller.transform.position = Vector3.Lerp(controller.transform.position, controller.toPosition, controller.normalizedTime);
                            if (controller.transform.position == controller.toPosition)
                            {
                                controller.determinePosition = false;
                                controller.time = 0;
                                controller.normalizedTime = 0;
                            }
                        }
                        else if (controller.dir == 1)
                        {
                            if (controller.determinePosition == false)
                            {
                                controller.toPosition = (controller.transform.position + controller.transform.forward * -1 * controller.dashDistance);
                                controller.determinePosition = true;
                            }
                            controller.time += Time.deltaTime;
                            controller.normalizedTime = controller.time / 6f;
                            controller.transform.position = Vector3.Lerp(controller.transform.position, controller.toPosition, controller.normalizedTime);
                            if (controller.transform.position == controller.toPosition)
                            {
                                controller.determinePosition = false;
                                controller.time = 0;
                                controller.normalizedTime = 0;
                            }
                        }
                        else if (controller.dir == 2)
                        {
                            if (controller.determinePosition == false)
                            {
                                controller.toPosition = (controller.transform.position + controller.transform.right * controller.dashDistance);
                                controller.determinePosition = true;
                            }
                            controller.time += Time.deltaTime;
                            controller.normalizedTime = controller.time / 6f;
                            controller.transform.position = Vector3.Lerp(controller.transform.position, controller.toPosition, controller.normalizedTime);
                            if (controller.transform.position == controller.toPosition)
                            {
                                controller.determinePosition = false;
                                controller.time = 0;
                                controller.normalizedTime = 0;
                            }
                        }
                        else if (controller.dir == 3)
                        {
                            if (controller.determinePosition == false)
                            {
                                controller.toPosition = (controller.transform.position + controller.transform.right * -1 * controller.dashDistance);
                                controller.determinePosition = true;
                            }
                            controller.time += Time.deltaTime;
                            controller.normalizedTime = controller.time / 6f;
                            controller.transform.position = Vector3.Lerp(controller.transform.position, controller.toPosition, controller.normalizedTime);
                            if (controller.transform.position == controller.toPosition)
                            {
                                controller.determinePosition = false;
                                controller.time = 0;
                                controller.normalizedTime = 0;
                            }
                        }
                    }*/
                





            



        }
        else
        {
            //if the AI is attacking
            FaceTarget(controller);

            //after a 2f timer for the attack animation, revert booleans and remove self from attacking array in game manager
            if (controller.generalTimer(2f))
            {
                controller.isAttacking = false;
                controller.canAttack = false;
                controller.canMove = true;
                controller.gm.onCancelAttack(controller.gameObject);
            }

        }

        //}

    }

    //get direction to target, get rotation to point to target, then update rotation
    void FaceTarget(StateController controller)
    {

        Vector3 direction = (controller.chaseTarget.position - controller.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

}
