using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/ToIdle")]
public class ToIdleDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetInRange = Ready(controller);
        return targetInRange;
    }

    private bool Ready(StateController controller)
    {
        controller.distanceToPlayer = Vector3.Distance(controller.chaseTarget.position, controller.transform.position);

        //if the AI has reached the distance for keeping their distance and there is no room next to player go
        if (controller.distanceToPlayer <= controller.distance2max && controller.allowDistance2)
        {
            controller.navMeshAgent.Stop();
            return true;
        }
        else
        {
            return false;
        }
    }
}
