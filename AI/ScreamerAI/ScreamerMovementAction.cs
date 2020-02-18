using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

[CreateAssetMenu (menuName = "PluggableAI/Actions/ScreamerMovement")]
public class ScreamerMovementAction : Action
{
    float random;

    public override void Act(StateController controller)
    {
        Track(controller);
    }

    private void Track(StateController controller)
    {

        Debug.Log("screamer tp");
        //if the screamer hasnt damaged the player in 3 consecutive attacks, then tp to the player
        if(controller.screamerPlayerNotDamagedCounter > 3)
        {
            //tp near player

            //get vector from player (chaseTarget) to the screamer
            Vector3 vectorToScreamer = controller.transform.position - controller.chaseTarget.position;
            //normalize
            vectorToScreamer.Normalize();
            //get position near player to teleport to (2 units from player)
            controller.transform.position = controller.chaseTarget.position + (2 * vectorToScreamer);
            //move the screamer up since the position is in the ground
            controller.transform.Translate(Vector3.up * 0.76479f);
            controller.sa.Teleport();
            controller.screamerPlayerNotDamagedCounter = 0;
            controller.teleported = true;
        }
        else
        {
            //teleport to random point in the arena
            int count = controller.screamPointsInRange.Count;

            
            int random = Random.Range(0, 4);

            float distance = Vector3.Distance(controller.screamPoints[random].position, controller.chaseTarget.position);
            while(controller.transform.position == controller.screamPoints[random].position || distance < 3)
            {
                random = Random.Range(0, 4);
                distance = Vector3.Distance(controller.screamPoints[random].position, controller.chaseTarget.position);
            }

            controller.transform.position = controller.screamPoints[random].position;
            controller.sa.Teleport();
            controller.teleported = true;
        }
        
    }

    //get direction to target, get rotation to point to target, then update rotation
    void FaceTarget(StateController controller)
    {
        
        Vector3 direction = (controller.chaseTarget.position - controller.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

}
