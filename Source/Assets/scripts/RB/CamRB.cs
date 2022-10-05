using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRB : MonoBehaviour
{
    public float sens;
    public Transform orientation;
    public Transform PlayerModel;
    float xR;
    float yR;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float mY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;
        yR += mX;
        xR -= mY;
        xR = Mathf.Clamp(xR, -90f, 90f);

        transform.rotation = Quaternion.Euler(xR, yR, 0);
        orientation.rotation = Quaternion.Euler(0, yR, 0);
        PlayerModel.rotation = Quaternion.Euler(0, yR, 0);

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

