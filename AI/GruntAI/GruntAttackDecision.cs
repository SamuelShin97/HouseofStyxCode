using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/GruntAttack")]
public class GruntAttackDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetInRange = Look(controller);
        return targetInRange;
    }

    private bool Look(StateController controller)
    {
        float distance = Vector3.Distance(controller.chaseTarget.position, controller.transform.position);

        if (distance <= controller.navMeshAgent.stoppingDistance)
        {
            //if the player is still in range stay in combat state
            return false;
        }
        else
        {
            // if not in the middle of attacking
            if (!controller.isAttacking)
            {
                //else go to chase state to get back in range
                return true;
            }
            else
            {
                return false;
            }
            
            
        }
    }
}
