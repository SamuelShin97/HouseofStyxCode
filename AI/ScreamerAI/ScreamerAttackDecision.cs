using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/ScreamerAttack")]
public class ScreamerAttackDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetInRange = Look(controller);
        return targetInRange;
    }

    private bool Look(StateController controller)
    {

        if (controller.tpTime && !controller.isAttacking)
        {
            controller.tpTime = false;
            controller.screamerNoDamage = false;
            //if the player is attacked or done attacking, go to teleport action
            return true;
        }
        else
        {
            return false;
            
        }
    }
}
