using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform BulletSpawn;
    public GameObject OGbullet;
    public float BulletSpeed;
    public bool AutoFire;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(AutoFire)
        {
            if (Input.GetMouseButton(0))
            {
                var bullet = Instantiate(OGbullet, BulletSpawn.position, BulletSpawn.rotation);
                bullet.GetComponent<Rigidbody>().velocity = BulletSpawn.forward * BulletSpeed;
            }
        } else
        {
            if (Input.GetMouseButtonDown(0))
            {
                var bullet = Instantiate(OGbullet, BulletSpawn.position, BulletSpawn.rotation);
                bullet.GetComponent<Rigidbody>().velocity = BulletSpawn.forward * BulletSpeed;
            }
        }
    }
}
