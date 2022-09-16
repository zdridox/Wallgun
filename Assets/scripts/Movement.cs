using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public CharacterController controller;
    public float Speed = 10f;
    public float gravity = -9.81f;
    public float Jheight = 3f;
    int jumped;
    int Dashed;
    float Uspeed;

    Vector3 Vel;
    public Transform Cam;

    public Transform Gcheck;
    float Gdist = 0.4f;
    public LayerMask Gmask;
    bool isGround;

    public float dashSpeed = 80f;
    public float dashTime = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        Uspeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Vector3 move = transform.right * x + transform.forward * z;

        isGround = Physics.CheckSphere(Gcheck.position, Gdist, Gmask);

        if(isGround && Vel.y < 0)
        {
            Vel.y = -2f;
        }

        if(isGround)
        {
            jumped = 0;
            Dashed = 0;
        }

        if(Input.GetButtonDown("Jump") && jumped < 1)
        {
            jumped++;
            Vel.y = Mathf.Sqrt(Jheight * -2f * gravity);
        }


        controller.Move(move * Uspeed * Time.deltaTime);

        Vel.y += gravity * Time.deltaTime;
        controller.Move(Vel * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftShift) && Dashed == 0)
        {
            Dashed++;
            StartCoroutine(Dash());
            
        }
    }


    IEnumerator Dash()
    {
        float sTime = Time.time;
        while(Time.time < sTime + dashTime)
        {
            controller.Move(Cam.transform.forward * dashSpeed * Time.deltaTime);
            yield return null;
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

