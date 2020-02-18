using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/GruntFromDodge")]
public class GruntFromDodgeDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetInRange = Look(controller);
        return targetInRange;
    }

    private bool Look(StateController controller)
    {



        

        if (!controller.dodge)
        {
            //if grunt is done dodging and going back to chase state
            //controller.navMeshAgent.speed = 3;
            return true;
        }
        else
        {
            //else stay in current state
            return false;
        }

    }
    
}
