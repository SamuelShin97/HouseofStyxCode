using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleProjectile : MonoBehaviour
{
    public GameObject bottlePrefab;
    public Vector3 initialPos;
    public Vector3 initialVelocity;
    public Transform boss;
    public Transform player;

    float fireRate;
    float nextFire;
    Rigidbody rb;
    GameObject instance;

    private float startTime;
    private float bottleLife;

    public float posY;
    public float posXZ;
    public float velocityXZ;
    public float velocityY;

    public void Start()
    {
        fireRate = 4f;
        bottleLife = 5f;
        nextFire = Time.time;

    }

    public void ThrowEvent()
    {
        Debug.Log("Launch Bottle");
        LaunchProjectile();
    }

    public void LaunchProjectile()
    {
        Vector3 bottleLaunch = CalcVelocity(player.position, boss.position, 0.85f);

        //if (Time.time > nextFire)
        //{
            float angle;
            Vector3 axis;
            Quaternion rotRand = Random.rotation;
            rotRand.ToAngleAxis(out angle, out axis);
            Quaternion rot = Quaternion.AngleAxis(angle, axis);
            GameObject bottle = Instantiate(bottlePrefab, boss.position, rot);//instantiate prefab
            //bottle.transform.Rotate(0, 50 * Time.deltaTime, 50 * Time.deltaTime);
            instance = bottle;
            rb = bottle.GetComponent<Rigidbody>();
            rb.velocity = bottleLaunch;
            nextFire = Time.time + fireRate;
            Destroy(bottle, bottleLife);//for whatever case bottle does not collide
        //}
    }

    //velocity and arc projictile is being thrown
    Vector3 CalcVelocity(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distXZ = distance;
        distXZ.y = 0f;

        posY = distance.y;
        posXZ = distXZ.magnitude;
        velocityXZ = posXZ / time;
        velocityY = posY / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distXZ.normalized;
        result *= velocityXZ;
        result.y *= velocityY;

        return result;
    }
}
