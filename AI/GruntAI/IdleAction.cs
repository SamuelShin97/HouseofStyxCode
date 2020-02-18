using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Idle")]
public class IdleAction : Action
{
    public override void Act(StateController controller)
    {
        Nothing(controller);
    }

    private void Nothing(StateController controller)
    {
        //add in idle animation

        //if the AI should keep their distance
        //check for availability in closser distance tier and have a chance to strafe
        if (controller.allowDistance2)
        {
            controller.FaceTarget(controller);
            controller.gm.requestDistanceTier1(controller.gameObject);

            float random = Random.Range(0f, 10.0f);
            if (controller.generalTimer(5f) && random >= 5f)
            {
                controller.strafe = true;
            }
        }
    }
    
}




