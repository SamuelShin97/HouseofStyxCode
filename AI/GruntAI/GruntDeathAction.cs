using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/GruntDeath")]
public class GruntDeathAction : Action
{
    float random;
    public override void Act(StateController controller)
    {
        Death(controller);
    }

    private void Death(StateController controller)
    {
        Destroy(controller.gameObject);
        //controller.gameObject.SetActive(false);
        //controller.kill = true;
        
        
    }

    
}
