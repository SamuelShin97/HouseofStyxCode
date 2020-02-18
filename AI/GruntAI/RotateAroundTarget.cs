using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/RotateAroundTarget")]
public class RotateAroundTarget : Action
{
    float random;
    public override void Act(StateController controller)
    {
        Track(controller);
    }

    private void Track(StateController controller)
    {

        if (controller.canMove)
        {
            if (controller.fromCombat)
            {
                //charge attack (increase speed till close to enemy or change to tracking state with increased speed and animation)
                controller.fromCombat = false; //after animation is complete
            }
            if (!controller.isAttacking)
            {
                Debug.Log("rotate");
                //controller.navMeshAgent.SetDestination(controller.transform.position + new Vector3(0f, 0f, -2f));
                controller.transform.RotateAround(controller.chaseTarget.position, Vector3.up, 20 * Time.deltaTime);
            }
          
        }

        
       
    }

    

}
