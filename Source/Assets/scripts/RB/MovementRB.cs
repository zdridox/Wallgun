using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRB : MonoBehaviour
{
    public float Speed;
    public float AirSpeed;
    public float WallrunSpeed;
    public float CrouchWalkSpeed;
    public float DashSpeed;
    float horizontal;
    float vertical;
    float DownRaycastLength;
    float UpRaycastLength;
    Vector3 MoveDir;
    Vector3 MoveDirCrouch;
    Rigidbody rb;
    public float JumpForce;
    public float JumpCooldown;
    public float DashCoolDown;
    bool CanJump = true;
    bool CanMove = true;
    bool WallLeft;
    bool WallRight;
    bool CanDash = true;
    bool CanMultiSlide = true;
    bool DownForceAdded = false;

    public float GroundDrag;
    public float airDrag;
    public float WallRunDrag;
    public float CrouchDrag;
    public LayerMask Ground;
    public LayerMask Slide;
    public LayerMask Wall;
    public LayerMask BlockCrouch;
    bool isGround;
    bool isSliding;
    bool isWallrunning;
    bool SmthAbove;
    public Transform orientation;
    public Transform Camera;
    bool CanAdd = true;
    bool Cntrl;
    int Jumped;
    int Dashed;
    public int MultiJumps;
    public int Dashes;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        DownRaycastLength = 2.2f;
        UpRaycastLength = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {


        isGround = Physics.Raycast(orientation.transform.position, Vector3.down, DownRaycastLength, Ground);
        isSliding = Physics.Raycast(orientation.transform.position, Vector3.down, DownRaycastLength + 0.3f, Slide);
        SmthAbove = Physics.Raycast(orientation.transform.position, Vector3.up, UpRaycastLength, BlockCrouch);
        WallLeft = Physics.Raycast(orientation.transform.position, -orientation.transform.right, 2f, Wall);
        WallRight = Physics.Raycast(orientation.transform.position, orientation.transform.right, 2f, Wall);
        Debug.DrawRay(orientation.transform.position, -transform.up * DownRaycastLength, Color.blue);
        Debug.DrawRay(orientation.transform.position, -orientation.transform.right * 2f, Color.blue);
        Debug.DrawRay(orientation.transform.position, orientation.transform.right * 2f, Color.blue);
        Debug.DrawRay(orientation.transform.position, Vector3.up * UpRaycastLength, Color.blue);

        Inputt();

        if(Input.GetKey(KeyCode.LeftControl))
        {
            CanMove = false;
            Cntrl = true;
            MoveCrouch();
            transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
            if(isGround && !DownForceAdded) rb.AddForce(Vector3.down * 1.6f, ForceMode.Impulse);
            DownForceAdded = true;
            DownRaycastLength = 1.1f;
            UpRaycastLength = 1.4f;
        } else
        {
            CanMove = true;
            Cntrl = false;
            DownForceAdded = false;
            if(!SmthAbove) transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
            DownRaycastLength = 2.2f;
            UpRaycastLength = 2.5f;
        }

        Drag();

        if(Input.GetKey(KeyCode.LeftControl) && isGround && CanAdd)
        {
            rb.velocity = rb.velocity * 1.8f;
            CanAdd = false;
        }  else
        {
            if(isGround && !CanAdd && !Cntrl)
            {
                CanAdd = true;
            }
        }

        if(Input.GetKey(KeyCode.LeftControl) && CanMultiSlide && isSliding)
        {
            CanMultiSlide = false;
            StartCoroutine(MultiSliding());
        }

        if((WallLeft || WallRight) && !isGround) {

            if (!isWallrunning) StartWallrun(); 
            
               
        } else
        {
            if (isWallrunning) StopWallrun();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && CanDash && Dashed < Dashes)
        {
            rb.AddForce(Camera.forward * DashSpeed, ForceMode.Impulse);
            StartCoroutine(DashCooolDooown());
            Dashed++;
        }

        if (isGround)
        {
            Dashed = 0;
            Jumped = 0;
        } 

        if(WallLeft || WallRight)
        {
            CanDash = false;
            CanJump = false;
        } else
        {
            CanJump = true;
            CanDash = true;
        }
    }

    private void FixedUpdate()
    {
        if(CanMove) movePlayer();
        if (isWallrunning) WallRunMovement();
    }

    void Inputt()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space) && CanJump && Jumped < MultiJumps - 1)
        {
            Jump();
            StartCoroutine(JumpCoolDownMaker());
        }

        if (WallLeft && Input.GetKeyDown(KeyCode.Space))
        {
            OutOfWallLeft();
        }

        if (WallRight && Input.GetKeyDown(KeyCode.Space))
        {
            OutOfWallRight();
        }
    }

    void movePlayer()
    {
        MoveDir = orientation.forward * vertical + orientation.right * horizontal;
        if(isGround) rb.AddForce(MoveDir.normalized * Speed, ForceMode.Force);
        if (!isGround) rb.AddForce(MoveDir.normalized * AirSpeed, ForceMode.Force);
    }
    void MoveCrouch()
    {
        MoveDirCrouch = orientation.right * horizontal;
        rb.AddForce(MoveDirCrouch.normalized * CrouchWalkSpeed, ForceMode.Force);
    }

    void Drag()
    {
        if (isGround && !isWallrunning)
        {
            rb.drag = GroundDrag;
        }
        else
        {
            if(!isGround && !isWallrunning)
            {
                rb.drag = airDrag;
            }
        }

        if (!CanMove && !isWallrunning) rb.drag = CrouchDrag; else if (CanMove && isGround && !isWallrunning) rb.drag = GroundDrag;
        if (isWallrunning) rb.drag = WallRunDrag;
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
        Jumped++;
    }

    IEnumerator DashCooolDooown()
    {
        CanDash = false;
        yield return new WaitForSeconds(DashCoolDown);
        CanDash = true;
    }

    IEnumerator JumpCoolDownMaker()
    {
        yield return new WaitForSeconds(JumpCooldown);
        CanJump = true;
    }

    IEnumerator MultiSliding()
    {
        rb.velocity = rb.velocity * 1.3f;
        yield return new WaitForSeconds(0.3f);
        CanMultiSlide = true;
    }

    void StartWallrun()
    {
        isWallrunning = true;
    }

    void StopWallrun()
    {
        isWallrunning = false;
        rb.useGravity = true;
    }

    void WallRunMovement()
    {
        rb.useGravity = false;
        rb.AddForce(Camera.transform.forward * WallrunSpeed, ForceMode.Force);
        if (WallLeft) rb.AddForce(-orientation.transform.right * 20f, ForceMode.Force);
        if (WallRight) rb.AddForce(orientation.transform.right * 20f, ForceMode.Force);

    }

    void OutOfWallLeft()
    {
        rb.AddForce(orientation.right * 15, ForceMode.Impulse);
        rb.AddForce(orientation.up * 12, ForceMode.Impulse);
        Jumped = 0;
    }

    void OutOfWallRight()
    {
        rb.AddForce(-orientation.right * 15, ForceMode.Impulse);
        rb.AddForce(orientation.up * 12, ForceMode.Impulse);
        Jumped = 0;
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
