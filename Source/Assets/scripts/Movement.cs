using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public float Speed = 10f;
    public float gravity = -9.81f;
    float Ugravity;
    public float Jheight = 4f;
    int jumped;
    int Dashed;
    bool DDJres = true;
    float Uspeed;
    bool AbleToWallRun = true;
    bool AbleToMove = true;
    bool CanUnCrouch = true;

    Vector3 Vel;
    public Transform Cam;

    float Gdist = 0.4f;
    public LayerMask Gmask;
    public LayerMask Wmask;
    public LayerMask UCmask;
    bool isGround;

    public float dashSpeed = 80f;
    public float dashTime = 0.2f;

    public float WallrunSpeed, MaxWallSpeed;
    public bool Wallright, Wallleft;
    bool JumpWallright, JumpWallleft;
    bool Wallrun = false;

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
        DrawRays();
        Crouch();

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && jumped < 2 && !CanUnCrouch)
        {
            Jump();
        }

        Vector3 move = transform.right * x + transform.forward * z;

        isGround = Physics.Raycast(transform.position, -transform.up, 2f, Gmask);


        if(isGround && DDJres)
        {
            Dashed = 0;
            jumped = 0;
            StartCoroutine(DDJress());
        }

        if(AbleToMove)
        {
            controller.Move(move * Uspeed * Time.deltaTime);
        }


        Vel.y += Ugravity * Time.deltaTime;
        controller.Move(Vel * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftShift) && Dashed == 0)
        {
            Dashed++;
            StartCoroutine(Dash());
            
        }


    }

    void DrawRays()
    {
        Debug.DrawRay(Cam.transform.position, Cam.transform.forward * 5, Color.cyan); // crosshair


        if (isGround)
        {
            Debug.DrawRay(transform.position, -transform.up * 2, Color.green);  // ground
        }
        else
        {
            Debug.DrawRay(transform.position, -transform.up * 2, Color.blue);  // ground
        }

        if (Wallleft)
        {
            Debug.DrawRay(transform.position, -transform.right * 1, Color.green); // left
            
        }
        else
        {
            Debug.DrawRay(transform.position, -transform.right * 1, Color.blue); // left
            
        }

        if (Wallright)
        {
            Debug.DrawRay(transform.position, transform.right * 1, Color.green); // right
            
        }
        else
        {
            Debug.DrawRay(transform.position, transform.right * 1, Color.blue); // right
            
        }

        if(JumpWallleft)
        {
            Debug.DrawRay(transform.position + transform.up * 0.2f, -transform.right * 3f, Color.green);
        } else
        {
            Debug.DrawRay(transform.position + transform.up * 0.2f, -transform.right * 3f, Color.blue);
        }

        if(JumpWallright)
        {
            Debug.DrawRay(transform.position + transform.up * 0.2f, transform.right * 3f, Color.green);
        } else
        {
            Debug.DrawRay(transform.position + transform.up * 0.2f, transform.right * 3f, Color.blue);
        }

        if(!CanUnCrouch)
        {
            Debug.DrawRay(transform.position, transform.up * 2.7f, Color.blue);
        } else
        {
            Debug.DrawRay(transform.position, transform.up * 2.7f, Color.green);
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

    IEnumerator MovePause()
    {
        AbleToMove = false;
        AbleToWallRun = false;
        yield return new WaitForSeconds(0.05f);
        AbleToMove = true;
        AbleToWallRun = true;


    }

    void Crouch()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            controller.height = 1f;
        } else
        {
            if(!CanUnCrouch)
            {
                controller.height = 3f;
            }
        }
    }

    IEnumerator OutofWallLeft()
    {
        float s2Time = Time.time;
        while(Time.time < s2Time + 0.3f)
        {
            controller.Move(transform.up * 6 * Time.deltaTime);
            controller.Move(transform.right * 6 * Time.deltaTime);
            controller.Move(transform.forward * Uspeed * Time.deltaTime);
            StopWallR();
            jumped = 1;
            Dashed = 0;
            yield return null;
        }
    }

    IEnumerator OutofWallRight()
    {
        float s2Time = Time.time;
        while (Time.time < s2Time + 0.3f)
        {
            controller.Move(transform.up * 6 * Time.deltaTime);
            controller.Move(-transform.right * 6 * Time.deltaTime);
            controller.Move(transform.forward * Uspeed * Time.deltaTime);
            StopWallR();
            jumped = 1;
            Dashed = 0;
            yield return null;
        }
    }

    IEnumerator DDJress()
    {
        DDJres = false;
        yield return new WaitForSeconds(0.5f);
        DDJres = true;
    }

    void WallrunInput()
    {
        if (Input.GetKey(KeyCode.D) && Wallright && AbleToWallRun) StartWallR();
        if (Input.GetKey(KeyCode.A) && Wallleft && AbleToWallRun) StartWallR();

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
                controller.Move(Cam.transform.forward * WallrunSpeed * Time.deltaTime);

                if (Wallright)
                {
                    controller.Move(transform.right * WallrunSpeed / 5 * Time.deltaTime);
                }
                else
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
            JumpWallleft = Physics.Raycast(transform.position + transform.up * 0.2f, -transform.right, 3f, Wmask);
            JumpWallright = Physics.Raycast(transform.position + transform.up * 0.2f, transform.right, 3f, Wmask);
            CanUnCrouch = Physics.Raycast(transform.position, transform.up, 2.7f, UCmask);

            if (Input.GetKeyDown(KeyCode.Space))
            {

                if (JumpWallleft && !isGround) { StartCoroutine(OutofWallLeft()); StartCoroutine(MovePause()); }
                if (JumpWallright && !isGround) { StartCoroutine(OutofWallRight()); StartCoroutine(MovePause()); }


            }


        if (!Wallleft && !Wallright)
        {
            StopWallR();
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

