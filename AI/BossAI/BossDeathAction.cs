using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu (menuName = "PluggableAI/Actions/BossDeath")]
public class BossDeathAction : Action
{
    public AudioSource bossDeath;
    float random;

    void Start()
    {
        bossDeath = GetComponent<AudioSource>();
    }

    private T GetComponent<T>()
    {
        throw new NotImplementedException();
    }

    public override void Act(StateController controller)
    {
        Death(controller);
    }

    private void Death(StateController controller)
    {
        controller.anim.SetTrigger("Defeat");
        bossDeath.Play(0);
        Destroy(controller.gameObject, 3);
        SceneManager.LoadScene("FinalCutScene");
        
    }

    
}
