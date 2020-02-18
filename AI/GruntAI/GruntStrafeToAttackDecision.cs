using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/GruntStrafeToAttack")]
public class GruntStrafeToAttackDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetInRange = Look(controller);
        return targetInRange;
    }

    private bool Look(StateController controller)
    {



        float distance = Vector3.Distance(controller.strafeTarget, controller.transform.position);

        if (distance <= 2 || controller.strafeTimeUp)
        {
            
            controller.strafeTimeUp = false;
            //if almost at strafe target, change state to chase target
            return true;
        }
        else
        {
            //else stay in current state
            return false;
        }

    }
    
}
