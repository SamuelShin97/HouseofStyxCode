using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/ScreamerMovement")]
public class ScreamerMovementDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetInRange = Look(controller);
        return targetInRange;
    }

    private bool Look(StateController controller)
    {

        if (controller.teleported)
        {
            //if the screamer has teleported then go to attack action
            controller.teleported = false;
            //make time before next teleport a little random
            controller.screamerTimerTime = Random.Range(8f, 17f);
            controller.oldHealth = controller.health._health;
            return true;
        }
        else
        {
            return false;
            
        }
    }
}
