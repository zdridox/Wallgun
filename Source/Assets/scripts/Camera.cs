using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Camera : MonoBehaviour
{

    public float sens = 100f;
    public Transform Pbody;
    float xRotation;
    public Movement Movement;
    float Tilt;
    float RotSpeed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Movement.Wallleft)
        {
            Tilt = -10;
        } else
        {
            if (Movement.Wallright)
            {
                Tilt = 10;
            }
        }
        
        if(!Movement.Wallright && !Movement.Wallleft)
        {
            Tilt = 0;
        }

        float mX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float mY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

        xRotation -= mY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, Tilt);
        Pbody.Rotate(Vector3.up * mX);

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
