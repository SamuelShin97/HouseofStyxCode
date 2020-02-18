using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.tag == "Player" && Input.GetButtonDown("Pickup"))
        {
            yield return new WaitForSeconds(3f);
            Debug.Log("weapon destroyed");
            Destroy(gameObject);
        }
    }
}
