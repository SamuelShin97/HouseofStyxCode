using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Chase")]
public class ChaseDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetInRange = Look(controller);
        return targetInRange;
    }

    private bool Look(StateController controller)
    {
        float distance = Vector3.Distance(controller.chaseTarget.position, controller.transform.position);

        //if the distance between the player and AI is less than the min/stopping distance then go to combat state 
        //else remain in current state
        if (distance <= controller.navMeshAgent.stoppingDistance)
        {
            controller.firstAttack = true;
            return true;
        }
        else
        {

            //check if from combat so the AI can perform a charge attack to get back into attack distance


            //NOT IMPLEMENTED YET (other parts in grunt attack action


            if (controller.fromCombat)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
