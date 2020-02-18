using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/GruntStrafe")]
public class GruntStrafeAction : Action
{
    float random;
    public override void Act(StateController controller)
    {
        Track(controller);
    }

    private void Track(StateController controller)
    {

        controller.FaceTarget(controller);

        //set navmesh destination to the target calculated in the to strafe decision
        controller.navMeshAgent.SetDestination(controller.strafeTarget);
        controller.anim.SetBool("Walk", true);
        float horizontal = (controller.navMeshAgent.velocity.magnitude / controller.navMeshAgent.speed) / 2;
        
        //once the timer is done, go to chase state
        if (controller.generalTimer(controller.strafeTimerLimit))
        {
            controller.strafeTimeUp = true;
        }

    }

}
