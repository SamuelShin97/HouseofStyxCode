using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHoverText : MonoBehaviour
{
    public CharacterScriptv1 player;
    private GameObject textDisplay;

    void Start()
    {
        textDisplay = transform.GetChild(0).gameObject;
        
    }
    // Update is called once per frame
    void Update()
    {
        // for Note 
        if (player.rangeOfNote == true)
        {
            textDisplay.SetActive(true);
            Camera camera = Camera.main;
            textDisplay.transform.LookAt(textDisplay.transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
        }
        else if (player.rangeOfNote == false)
        {
            textDisplay.SetActive(false);
        }

    }
}
