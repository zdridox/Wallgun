using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonckGun : MonoBehaviour
{
    [SerializeField] Transform Cam;
    [SerializeField] Rigidbody PlayerRB;
    [SerializeField] float knockPower;
    [SerializeField] float shotTimeout;
    bool canKnock = true;
    public Ammo Ammo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && canKnock && !Ammo.AmmoZero)
        {
            PlayerRB.AddForce(-Cam.transform.forward * knockPower, ForceMode.Impulse);
            Ammo.RealAmmo -= 1;
            StartCoroutine(KnockTimeout());
           
        }
    }

    IEnumerator KnockTimeout()
    {
        canKnock = false;
        yield return new WaitForSeconds(shotTimeout);
        canKnock = true;
    }
}
