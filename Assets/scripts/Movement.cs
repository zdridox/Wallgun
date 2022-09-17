using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public float Speed = 10f;
    public float gravity = -9.81f;
    float Ugravity;
    public float Jheight = 3f;
    int jumped;
    int Dashed;
    float Uspeed;

    Vector3 Vel;
    public Transform Cam;

    public Transform Gcheck;
    float Gdist = 0.4f;
    public LayerMask Gmask;
    public LayerMask Wmask;
    bool isGround;

    public float dashSpeed = 80f;
    public float dashTime = 0.2f;

    public float WallrunSpeed, MaxWallSpeed, MaxWalltime;
    bool Wallright, Wallleft;
    bool Wallrun;
    float camTilt, MaxCamTilt;

    // Start is called before the first frame update
    void Start()
    {
        Uspeed = Speed;
        Ugravity = gravity;
    }

    // Update is called once per frame
    void Update()
    {

        CheckForWall();
        WallrunInput();

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && jumped < 1)
        {
            Jump();
        }

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




        controller.Move(move * Uspeed * Time.deltaTime);

        Vel.y += Ugravity * Time.deltaTime;
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


    void WallrunInput()
    {
        if (Input.GetKey(KeyCode.D) && Wallright) StartWallR();
        if (Input.GetKey(KeyCode.A) && Wallleft) StartWallR();

    }

    void Jump()
    {
            jumped++;
            Vel.y = Mathf.Sqrt(Jheight * -2f * Ugravity);
        
    }

    void StartWallR()
    {
        Ugravity = 0f;
        Wallrun = true;

        if (controller.velocity.magnitude <= MaxWallSpeed)
        {
            controller.Move(transform.forward * WallrunSpeed * Time.deltaTime);

            if(Wallright)
            {
                controller.Move(transform.right * WallrunSpeed / 5 * Time.deltaTime);
            } else
            {
                controller.Move(-transform.right * WallrunSpeed / 5 * Time.deltaTime);
            }
        }
    }

    void StopWallR()
    {
        Ugravity = gravity;
        Wallrun = false;
    }

    void CheckForWall()
    {
        Wallright = Physics.Raycast(transform.position, transform.right, 1f, Wmask);
        Wallleft = Physics.Raycast(transform.position, -transform.right, 1f, Wmask);

        if (!Wallleft && !Wallright)
        {
            StopWallR();
        }

        if(Wallleft || Wallright)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                jumped = 0;
                Dashed = 0;
                Jump();

            }
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

