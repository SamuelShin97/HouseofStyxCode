using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/BossAttack")]
public class BossAttackDecision : Decision
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
            //if the player is in range stay in attack state
            return false;
        }
        else
        {
            //if not in the middle on attacking, go to chase state
            if (!controller.isAttacking)
            {
                return true;
            }
            else
            {
                return false;
            }
            
            
        }
    }
}
