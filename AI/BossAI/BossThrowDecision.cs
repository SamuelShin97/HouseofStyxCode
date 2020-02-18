using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/BossThrow")]
public class BossThrowDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetInRange = Look(controller);
        return targetInRange;
    }

    private bool Look(StateController controller)
    {
        
        float currentHealth = controller.health._health;
        if ((controller.oldHealth - currentHealth) >= (controller.startingHealth/4) && !controller.isAttacking)
        {
            controller.oldHealth = currentHealth;

            controller.boxCollider.enabled = false;

            //spawn enemies and particles
            Debug.Log("before spawn enemies");
            controller.Invoke("SpawnEnemies", 5f);

            for(int i = 0; i < controller.spawnPoints.Length; i++)
            {
                Instantiate(controller.spawningParticle, controller.spawnPoints[i].position, Quaternion.identity);
                Instantiate(controller.spawningParticle2, controller.spawnPoints[i].position, Quaternion.identity);

            }
            
            //if boss losses more than 5 health
            return true;
        }
        else
        {
            return false;
            
        }
    }

    
}
