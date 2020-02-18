using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Death")]
public class DeathDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetInRange = Look(controller);
        return targetInRange;
    }

    private bool Look(StateController controller)
    {
        

        //if AI's health is less than 0 go to death state
        if (controller.health._health <= 0)
        {
        
            return true;
        }
        else
        {

            return false;
            
        }
    }
}
