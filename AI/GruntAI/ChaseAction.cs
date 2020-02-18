using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Action
{
    public override void Act(StateController controller)
    {
        Track(controller);
    }

    private void Track(StateController controller)
    {
        //if the AI isnt in the middle of an attack then walk towards the player
        if (!controller.isAttacking)
        {
            //set the players current position as the destination
            controller.navMeshAgent.SetDestination(controller.chaseTarget.position);
            controller.navMeshAgent.Resume();
            
            //set animation controller values for specific AI enemies
            if (controller.isGrunt)
            {
                controller.anim.SetBool("Walk", true);

                //if AI is in the distance 2 then check distance 1 availabilty 
                if (!controller.allowDistance1 && controller.allowDistance2)
                {
                    controller.gm.requestDistanceTier1(controller.gameObject);

                    //if AI is in no distance tier then add them to distance 2
                }
                else if (!controller.allowDistance2 && !controller.allowDistance1)
                {
                    controller.gm.requestDistanceTier2(controller.gameObject);
                }
            }
            else if(controller.isBoss)
            {
                //make boss walk again
                controller.anim.SetBool("Walk", true);
                controller.walking = true;
            }
            
        }
        /*
         have random speed ups using a timer to change navmesh speed or could use coroutine and waitforseconds
         if(rng value && not currently running)
            Dash(time)

        function Dash (runTime : float) {
            var timer = 0.0;
             while (timer < runTime) {
                // controller.navMesh.speed = fast
                timer += Time.deltaTime; 
            }
            controller.navMesh.speed = normal
        }
         */
    }
}
