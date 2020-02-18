using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/BossChase")]
public class BossChaseDecision : Decision
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
            Debug.Log("stoping distance reached");
            //if the player is in range go to combat state
            return true;
        }
        else
        {
            return false;
            
        }
    }
}
