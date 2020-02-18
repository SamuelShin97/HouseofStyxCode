using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/GruntDodge")]
public class GruntDodgeAction : Action
{
    float random;
    float timeToLerp = 2.0f;
    float timeStartedLerping;
    public override void Act(StateController controller)
    {
        Track(controller);
    }

    private void Track(StateController controller)
    {
        
        Debug.Log("dodged");
        //dodge
        //controller.gm.playerAttacked = false;
        if (controller.determinePosition == false)
        {
            controller.dir = Random.Range(0, 3);
        }
        
        //dodge left
        if (controller.dir == 0)
        {
            if (controller.determinePosition == false)
            {
                timeStartedLerping = Time.time;
                controller.startPosition = controller.transform.position;
                controller.toPosition = (controller.startPosition + controller.transform.right * -1 * controller.dashDistance);
                controller.determinePosition = true;
                controller.anim.SetTrigger("GruntDodgeLeft");
            }
            //float distance = Vector3.Distance(controller.toPosition, controller.transform.position);
            //controller.navMeshAgent.SetDestination(controller.toPosition);

            //controller.time += Time.deltaTime;
            //controller.normalizedTime = controller.time / 4f;
            float timeSinceStarted = Time.time - timeStartedLerping;
            float percentageComplete = timeSinceStarted / timeToLerp;
            controller.transform.position = Vector3.Lerp(controller.startPosition, controller.toPosition, percentageComplete);
            //Debug.Log("normalized time is " + controller.normalizedTime);
            if (percentageComplete >= 1.0f)
            {
                Debug.Log("end dodge");
                controller.dodge = false;
                controller.determinePosition = false;
                controller.time = 0;
                controller.normalizedTime = 0;
            }

        }

        //dodge back
        else if (controller.dir == 1)
        {
            if (controller.determinePosition == false)
            {
                timeStartedLerping = Time.time;
                controller.startPosition = controller.transform.position;
                controller.toPosition = (controller.startPosition + controller.transform.forward * -1 * controller.dashDistance);
                controller.determinePosition = true;
                controller.anim.SetTrigger("GruntDodgeBackward");
        }
            //float distance = Vector3.Distance(controller.toPosition, controller.transform.position);
            //controller.navMeshAgent.SetDestination(controller.toPosition);
                
            //controller.time += Time.deltaTime;
            //controller.normalizedTime = controller.time / 4f;
            float timeSinceStarted = Time.time - timeStartedLerping;
            float percentageComplete = timeSinceStarted / timeToLerp;
            controller.transform.position = Vector3.Lerp(controller.transform.position, controller.toPosition, percentageComplete);
            //Debug.Log("normalized time is " + controller.normalizedTime);
            if (percentageComplete >= 1.0f)
            {
                Debug.Log("end dodge");
                controller.dodge = false;
                controller.determinePosition = false;
                controller.time = 0;
                controller.normalizedTime = 0;
            }
        }

        //dodge right
        else if (controller.dir == 2)
        {
            if (controller.determinePosition == false)
            {
                timeStartedLerping = Time.time;
                controller.startPosition = controller.transform.position;
                controller.toPosition = (controller.startPosition + controller.transform.right * controller.dashDistance);
                controller.determinePosition = true;
                controller.anim.SetTrigger("GruntDodgeRight");
        }
            //float distance = Vector3.Distance(controller.toPosition, controller.transform.position);
            //controller.navMeshAgent.SetDestination(controller.toPosition);

            //controller.time += Time.deltaTime;
            //controller.normalizedTime = controller.time / 4f;
            float timeSinceStarted = Time.time - timeStartedLerping;
            float percentageComplete = timeSinceStarted / timeToLerp;
            controller.transform.position = Vector3.Lerp(controller.transform.position, controller.toPosition, percentageComplete);
            //Debug.Log("normalized time is " + controller.normalizedTime);
            if (percentageComplete >= 1.0f)
            {
                Debug.Log("end dodge");
                controller.dodge = false;
                controller.determinePosition = false;
                controller.time = 0;
                controller.normalizedTime = 0;
            }
        }

        /*//dodge forward
        if (controller.dir == 3)
        {
            if (controller.determinePosition == false)
            {
                controller.toPosition = (controller.transform.position + controller.transform.forward * controller.dashDistance);
                controller.determinePosition = true;
                controller.anim.SetTrigger("GruntDodgeUpward"); //call only once
        }
            //float distance = Vector3.Distance(controller.toPosition, controller.transform.position);
            //controller.navMeshAgent.SetDestination(controller.toPosition);
                
            controller.time += Time.deltaTime;
            controller.normalizedTime = controller.time / 4f;
            controller.transform.position = Vector3.Lerp(controller.transform.position, controller.toPosition, controller.normalizedTime);
                
            if (controller.transform.position == controller.toPosition)
            {
                Debug.Log("end dodge");
                controller.dodge = false;
                controller.determinePosition = false;
                controller.time = 0;
                controller.normalizedTime = 0;
            }
        }*/



    }

}
