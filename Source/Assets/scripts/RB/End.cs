using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Spawn;
    [SerializeField] MovementRB MVRB;
    [SerializeField] CamRB CRB;
    [SerializeField] Gun Gun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");          
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            Player.transform.position = Spawn.transform.position;
            Menu.SetActive(true);
            MVRB.Movement = false;
            CRB.Cursorr = true;
            CRB.CanLook = false;
            Gun.canShoot = false;
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
