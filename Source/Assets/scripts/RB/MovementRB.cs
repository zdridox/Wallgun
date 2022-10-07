using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRB : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] float AirSpeed;
    [SerializeField] float WallrunSpeed;
    [SerializeField] float CrouchWalkSpeed;
    [SerializeField] float DashSpeed;
    float horizontal;
    float vertical;
    float DownRaycastLength;
    float UpRaycastLength;
    Vector3 MoveDir;
    Vector3 MoveDirCrouch;
    Rigidbody rb;
    [SerializeField] float JumpForce;
    [SerializeField] float JumpCooldown;
    [SerializeField] float DashCoolDown;
    bool CanJump = true;
    bool CanMove = true;
    [HideInInspector] public bool WallLeft;
    [HideInInspector] public bool WallRight;
    bool DashCour;
    bool CanDash = true;
    bool CanMultiSlide = true;
    bool DownForceAdded = false;
    bool CanMuSlideCr = true;

    [SerializeField] float GroundDrag;
    [SerializeField] float airDrag;
    [SerializeField] float WallRunDrag;
    [SerializeField] float CrouchDrag;
    [SerializeField] LayerMask Ground;
    [SerializeField] LayerMask Slide;
    [SerializeField] LayerMask Wall;
    [SerializeField] LayerMask BlockCrouch;
    bool isGround;
    bool isSliding;
    bool isWallrunning;
    bool SmthAbove;
    [SerializeField] Transform orientation;
    [SerializeField] Transform Camera;
    bool CanAdd = true;
    bool Cntrl;
    int Jumped;
    int Dashed;
    [SerializeField] int MultiJumps;
    [SerializeField] int Dashes;

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

        if(Input.GetKey(KeyCode.LeftControl) && isGround && CanAdd && CanMuSlideCr)
        {
            rb.velocity = rb.velocity * 1.8f;
            CanAdd = false;
            StartCoroutine(SlideCrouchCoolDown());
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
            if(!DashCour) CanDash = true;
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
        DashCour = true;
        yield return new WaitForSeconds(DashCoolDown);
        CanDash = true;
        DashCour = false;
    }

    IEnumerator SlideCrouchCoolDown()
    {
        CanMuSlideCr = false;
        yield return new WaitForSeconds(1.5f);
        CanMuSlideCr = true;
    }

    IEnumerator JumpCoolDownMaker()
    {
        yield return new WaitForSeconds(JumpCooldown);
        CanJump = true;
    }

    IEnumerator MultiSliding()
    {
        rb.velocity = rb.velocity * 1.5f;
        yield return new WaitForSeconds(0.2f);
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
