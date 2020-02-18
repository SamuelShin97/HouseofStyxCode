using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/BottlePosition")]
public class BottlePositionAction : Action
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

            controller.anim.SetBool("Walk", true);
            //set the players current position as the destination
            controller.navMeshAgent.SetDestination(controller.throwPosition.position);
            controller.navMeshAgent.Resume();
            
            controller.anim.speed = 1.5f;
            controller.navMeshAgent.speed = 6f;

            controller.walking = true;
        }
        
    }
}
