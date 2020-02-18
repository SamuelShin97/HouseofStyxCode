using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/BossToBottle")]
public class BossToBottleDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetInRange = Look(controller);
        return targetInRange;
    }

    private bool Look(StateController controller)
    {
        float distance = Vector3.Distance(controller.throwPosition.position, controller.transform.position);

        if (distance <= controller.navMeshAgent.stoppingDistance)
        {
            //if boss reached the bottle throwing position, then change to bottle throwing state
            controller.anim.speed = 1f;
            controller.navMeshAgent.speed = 3.5f;
            controller.oldHealth = controller.health._health;
            return true;
        }
        else
        {
            return false;
            
        }
    }
}
