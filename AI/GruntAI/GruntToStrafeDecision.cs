using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/GruntToStrafe")]
public class GruntToStrafeDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetInRange = Look(controller);
        return targetInRange;
    }

    private bool Look(StateController controller)
    {

        controller.distanceToPlayer = Vector3.Distance(controller.chaseTarget.position, controller.transform.position);

        //strafe if told from another state or if the AI is closer to the player than it should be
        if (controller.strafe && !controller.isAttacking || (!controller.isAttacking && controller.allowDistance2 && controller.distanceToPlayer < controller.distance2min))
        {
            controller.navMeshAgent.Resume();
            //if the AI is able to strafe
            controller.strafe = false;

            //if the AI should keep their distance strafe to the right or left
            //else strafe a little forward also
            if (controller.allowDistance2)
            {
                //get position to strafe to
                if (Random.Range(0.0f, 10.0f) > 5f)
                {
                    controller.strafeTarget = controller.transform.TransformPoint((Vector3.right) * 3);
                }
                else
                {
                    controller.strafeTarget = controller.transform.TransformPoint((Vector3.left) * 3);
                }
            }
            else
            {
                //get position to strafe to
                if (Random.Range(0.0f, 10.0f) > 5f)
                {
                    controller.strafeTarget = controller.transform.TransformPoint((Vector3.right + Vector3.forward) * 4);
                }
                else
                {
                    controller.strafeTarget = controller.transform.TransformPoint((Vector3.left + Vector3.forward) * 4);
                }
            }
            return true;
        }
        else
        {
            return false;
            
        }
    }
}
