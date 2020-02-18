using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Idle")]
public class IdleDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetInRange = PlayerReady(controller);
        return targetInRange;
    }

    private bool PlayerReady(StateController controller)
    {
        float distance = Vector3.Distance(controller.chaseTarget.position, controller.transform.position);

        //leave idle state if AI is in look range, if the AI is supposed to be close to the player,
        // or if the AI isnt close enough for the distance 2 range
        if (distance <= controller.lookRadius || controller.allowDistance1 || (controller.allowDistance2 && distance > controller.distance2max))
        {
            
            return true;
        }
        else
        {
            return false;
        }
    }
}
