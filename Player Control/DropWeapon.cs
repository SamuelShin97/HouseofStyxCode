using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWeapon : MonoBehaviour
{
    public GameObject prefabDrop;
    public GameObject weapon;
    public GameObject player;
    public UI playerStats;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.transform.root.tag == "Player")
        {
            playerStats = player.GetComponent<UI>();
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Drop") && (gameObject.transform.root.tag == "Player"))
        {
            Debug.Log("weapon drop");
            Instantiate(prefabDrop, transform.position, transform.rotation);
            weapon.SetActive(false);
            //playerStats.aw = CharacterScriptv1.activeWeapon.nothing;
        }
    }
}
