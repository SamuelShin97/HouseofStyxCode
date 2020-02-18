using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/BossFromThrow")]
public class BossFromThrowDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetInRange = Look(controller);
        return targetInRange;
    }

    private bool Look(StateController controller)
    {
        if (controller.doneThrowing)
        {
            controller.doneThrowing = false;
            controller.boxCollider.enabled = true;
            return true;
        }
        else
        {
            return false;
        }

        
    }
}
