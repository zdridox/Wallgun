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
    float camTilt;
    public MovementRB mvrb;
    bool CallerR;
    bool CallerL;
    bool CallerRmR;
    bool CallerRmL;
    public float tiltspeed;
    bool tiltL;
    bool tiltR;


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

        transform.rotation = Quaternion.Euler(xR, yR, camTilt);
        orientation.rotation = Quaternion.Euler(0, yR, 0);
        PlayerModel.rotation = Quaternion.Euler(0, yR, 0);

        if(mvrb.WallLeft && !mvrb.isGround && Input.GetKey(KeyCode.A))
        {
            CallerL = true;
        }  else
        {
            if(tiltL)
            {
                CallerRmL = true;
                tiltL = false;
                CallerL = false;
            }
        }


        if(mvrb.WallRight && !mvrb.isGround && Input.GetKey(KeyCode.D))
        {
            CallerR = true;
        } else
        {
            if(tiltR)
            {
                CallerRmR = true;
                tiltR = false;
                CallerR = false;
            }
        }


        if (CallerR) CallTiltR();
        if (CallerL) CallTiltL();
        if (CallerRmR) CallremoveR();
        if (CallerRmL) CallremoveL();

        //Debug.Log(camTilt + "," + CallerL + "," + CallerR + "," + CallerRmL + "," + CallerRmR);
    }


    void CallTiltL()
    {
        tiltL = true;
        if (camTilt > -10 && !tiltR)
        {
            camTilt -= Time.deltaTime * tiltspeed;
        } else
        {
            camTilt = -10;
        }

    }


    void CallTiltR()
    {
        tiltR = true;
        if (camTilt < 10 && !tiltL)
        {
            camTilt += Time.deltaTime * tiltspeed;
        } else
        {
            camTilt = 10;
        }

    }

    void CallremoveL()
    {
        if (camTilt < 0 && camTilt != 0)
        {
            camTilt += Time.deltaTime * tiltspeed;
        } else
        {
            CallerRmL = false;
            camTilt = 0;
        }
    }

    void CallremoveR()
    {
        if (camTilt > 0 && camTilt != 0)
        {
            camTilt -= Time.deltaTime * tiltspeed;
        } else
        {
            CallerRmR = false;
            camTilt = 0;
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

