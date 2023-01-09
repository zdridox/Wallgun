using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject Pause;
    [SerializeField] MovementRB MVRB;
    [SerializeField] Ammo Ammo;
    [SerializeField] CamRB CRB;
    [SerializeField] Gun Gun;
    bool MenuActive;
    public bool MenuCanOnOff = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && MenuCanOnOff)
        {
            if(!MenuActive)
            {
                Pause.SetActive(true);
                MenuActive = true;
                MVRB.Movement = false;
                CRB.Cursorr = true;
                CRB.CanLook = false;
                Gun.canShoot = false;
                Ammo.CanDoAmmoStuff = false;
            } else
            {
                Pause.SetActive(false);
                MenuActive = false;
                MVRB.Movement = true;
                CRB.Cursorr = false;
                CRB.CanLook = true;
                Gun.canShoot = true;
                Ammo.CanDoAmmoStuff = true;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            Menu.SetActive(true);
            MVRB.Movement = false;
            CRB.Cursorr = true;
            CRB.CanLook = false;
            Gun.canShoot = false;
            Ammo.CanDoAmmoStuff = false;
            MenuCanOnOff = false;
        }
    }
}


//                                                    
//                                             )\   /| 
//                                         .-/ '-|_/ |
//                       __ __,-' (   / \/          
//                   .-'"  "'-..__,-'""          -o.`-._   
//                  /                                   '/
//          *--._ ./                                 _.-- 
//                |                              _.-' 
//                :                           .-/
//                 \                       )_ /
//                  \                _)   / \(
//                    `.   / -.___.-- - '(  /   \\
//                     (  /   \\       \(L\
//                      \(L\       \\
//                       \\              \\
//                        L\              L\
