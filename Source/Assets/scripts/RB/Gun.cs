using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Gun : MonoBehaviour
{
    public Transform BulletSpawn;
    public GameObject OGbullet;
    public float BulletSpeed;
    public bool AutoFire;
    [HideInInspector] public bool canShoot = true;
    [SerializeField] AudioClip ShootSound;
    [SerializeField] TMP_Text tmp;
    [SerializeField] float Amoo;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if( Amoo < 10)
        {
            switch (Amoo)
            {
                case (0):
                    tmp.text = "00";
                    break;
                case (1):
                    tmp.text = "01";
                    break;
                case (2):
                    tmp.text = "02";
                    break;
                case (3):
                    tmp.text = "03";
                    break;
                case (4):
                    tmp.text = "04";
                    break;
                case (5):
                    tmp.text = "05";
                    break;
                case (6):
                    tmp.text = "06";
                    break;
                case (7):
                    tmp.text = "07";
                    break;
                case (8):
                    tmp.text = "08";
                    break;
                case (9):
                    tmp.text = "09";
                    break;
            }
        } else
        {
            tmp.text = Amoo.ToString();
        }

        if(canShoot)
        {
            if (AutoFire)
            {
                if (Input.GetMouseButton(0) && Amoo > 0)
                {
                    var bullet = Instantiate(OGbullet, BulletSpawn.position, BulletSpawn.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = BulletSpawn.forward * BulletSpeed;
                    SoundManager.SMInstance.PlaySound(ShootSound);
                    Amoo--;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0) && Amoo > 0)
                {
                    var bullet = Instantiate(OGbullet, BulletSpawn.position, BulletSpawn.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = BulletSpawn.forward * BulletSpeed;
                    SoundManager.SMInstance.PlaySound(ShootSound);
                    Amoo--;
                }
            }
        }
    }
}
