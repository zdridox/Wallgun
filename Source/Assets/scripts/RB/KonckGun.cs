using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonckGun : MonoBehaviour
{
    [SerializeField] Transform Cam;
    [SerializeField] Rigidbody PlayerRB;
    [SerializeField] float knockPower;
    [SerializeField] float shotTimeout;
    [SerializeField] AudioClip Koncksound;
    [SerializeField] KnockGunTimeout kgto;
    bool canKnock = true;
    public Ammo Ammo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && kgto.CanKnockTO && Ammo.RealAmmo > 0)
        {
            PlayerRB.AddForce(-Cam.transform.forward * knockPower, ForceMode.Impulse);
            SoundManager.SMInstance.PlaySound(Koncksound);
            Ammo.RealAmmo -= 1;
            kgto.CallKGTimeout();
           
        }
    }


}
