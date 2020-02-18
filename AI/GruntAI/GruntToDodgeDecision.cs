using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/GruntToDodge")]
public class GruntToDodgeDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetInRange = Look(controller);
        return targetInRange;
    }

    private bool Look(StateController controller)
    {



        if (controller.dodge)
        {
            //if grunt is going to dodge
            //controller.navMeshAgent.speed = 10f;
            return true;
        }
        else
        {
            //else stay in current state
            return false;
        }

    }
    
}
