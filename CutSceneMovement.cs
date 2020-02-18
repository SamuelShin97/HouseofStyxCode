using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneMovement : MonoBehaviour
{
    public PlayableDirector director1;
    public PlayableDirector currentDirector;
    GameObject cutscene1;
    GameObject cutscene2;
    GameObject cutscene3;
    CharacterScriptv1 character;
    bool check = false;

    // Start is called before the first frame update
    void Start()
    {
        currentDirector = currentDirector.GetComponent<PlayableDirector>();
        director1 = director1.GetComponent<PlayableDirector>();
        character = GetComponent<CharacterScriptv1>();
        cutscene1 = GameObject.Find("Cutscene1Cameras");
        cutscene2 = GameObject.Find("Cutscene2Cameras");
        cutscene3 = GameObject.Find("Cutscene3Cameras");

        //currentDirector = director1;
    }

    // Update is called once per frame
    void Update()
    {
        checkState();
    }

    void checkState()
    {

        if (currentDirector.state == PlayState.Playing)
        {
            //Debug.Log("Is playing.");
            //Debug.Log(currentDirector.state);
            character.isMove = false;
        }
        else
        {
            //Debug.Log("Is not playing.");
            //Debug.Log(currentDirector.state);
            character.isMove = true;
            if(currentDirector.name == "Cutscene1Cameras")
            {
                cutscene1.SetActive(false);
            }
            if (currentDirector.name == "Cutscene2Cameras")
            {
                cutscene2.SetActive(false);
            }
            if (currentDirector.name == "Cutscene3Cameras")
            {
                cutscene3.SetActive(false);
            }
        }
    }
}
