using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRB : MonoBehaviour
{
    public bool Movement = true;
    public float Gravity = -35;
    public float WallRunGravity = -15;
    float uGravity;
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
    bool canMovedir = true;
    bool CanMultiSlide = true;
    bool DownForceAdded = false;
    bool CanMuSlideCr = true;
    bool isCROUCHED;

    [SerializeField] float GroundDrag;
    [SerializeField] float airDrag;
    [SerializeField] float WallRunDrag;
    [SerializeField] float CrouchDrag;
    [SerializeField] float OutWallUp, OutWallSide;
    [SerializeField] LayerMask Ground;
    [SerializeField] LayerMask Slide;
    [SerializeField] LayerMask Wall;
    [SerializeField] LayerMask BlockCrouch;
    [HideInInspector] public bool isGround;
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
    [SerializeField] AudioClip dashSound;
    [SerializeField] GameObject walkSound;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        DownRaycastLength = 2.2f;
        UpRaycastLength = 2.5f;
        uGravity = Gravity;
        Physics.IgnoreLayerCollision(16, 16);
    }

    // Update is called once per frame
    void Update()
    {


        Debug.DrawRay(orientation.transform.position, Vector3.down * 2.8f);

        Physics.gravity = new Vector3(0, uGravity, 0);
        isGround = Physics.Raycast(orientation.transform.position, Vector3.down, DownRaycastLength, Ground);
        isSliding = Physics.Raycast(orientation.transform.position, Vector3.down, DownRaycastLength + 1.6f, Slide);
        SmthAbove = Physics.Raycast(orientation.transform.position, Vector3.up, UpRaycastLength * 3, BlockCrouch);
        WallLeft = Physics.Raycast(orientation.transform.position, -orientation.transform.right, 2f, Wall);
        WallRight = Physics.Raycast(orientation.transform.position, orientation.transform.right, 2f, Wall);

        Inputt();


       if(isWallrunning)
        {
            canMovedir = false;
        }else
        {
            if (!isWallrunning) canMovedir = true;
        }

        if(Input.GetKey(KeyCode.LeftControl) && Movement)
        {
            isCROUCHED = true;
        } else
        {
            if(!SmthAbove)
            {
                isCROUCHED = false;
                CanMove = true;
                Cntrl = false;
                DownForceAdded = false;
                transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
                DownRaycastLength = 2.2f;
                UpRaycastLength = 2.5f;
            }
        }

        if(isCROUCHED)
        {
            CanMove = false;
            Cntrl = true;
            MoveCrouch();
            transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
            if (isGround && !DownForceAdded) rb.AddForce(Vector3.down * 1.6f, ForceMode.Impulse);
            DownForceAdded = true;
            DownRaycastLength = 1.1f;
            UpRaycastLength = 1.4f;
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

        if((Input.GetKey(KeyCode.LeftControl) && CanMultiSlide && isSliding) || (isSliding && SmthAbove && CanMultiSlide))
        {
            CanMultiSlide = false;
            StartCoroutine(MultiSliding());
        }

        if(WallLeft && !isGround && Input.GetKey(KeyCode.A)) {

            if (!isWallrunning) StartWallrun(); 
            
               
        } else
        {
            if(WallRight && !isGround && Input.GetKey(KeyCode.D))
            {
                if (!isWallrunning) StartWallrun();
            } else
            {
                if (isWallrunning) StopWallrun();
            }

        }
      

        if (Input.GetKeyDown(KeyCode.LeftShift) && CanDash && Dashed < Dashes)
        {
            rb.AddForce(Camera.forward * DashSpeed, ForceMode.Impulse);
            SoundManager.SMInstance.PlaySound(dashSound);
            StartCoroutine(DashCooolDooown());
            Dashed++;
        }

        if (isGround)
        {
            Dashed = 0;
            Jumped = 0;
        } 

        if((WallLeft || WallRight) && !isGround)
        {
            CanDash = false;
            CanJump = false;
        } else
        {
            CanJump = true;
            if(!DashCour) CanDash = true;
        }

     if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && isGround)
        {
            walkSound.SetActive(true);
        } else
        {
            walkSound.SetActive(false);
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

        if (WallLeft && Input.GetKeyDown(KeyCode.Space) && !isGround)
        {
            OutOfWallLeft(true);
        }

        if (WallRight && Input.GetKeyDown(KeyCode.Space) && !isGround)
        {
            OutOfWallRight(true);
        }
    }

    void movePlayer()
    {
        MoveDir = orientation.forward * vertical + orientation.right * horizontal;
        if(isGround && canMovedir) rb.AddForce(MoveDir.normalized * Speed, ForceMode.Force);
        if (!isGround && canMovedir) rb.AddForce(MoveDir.normalized * AirSpeed, ForceMode.Force);
    }
    void MoveCrouch()
    {
        MoveDirCrouch = orientation.right * horizontal;
        rb.AddForce(MoveDirCrouch.normalized * CrouchWalkSpeed, ForceMode.Force);
    }

    void Drag()
    {
        if(!Movement)
        {
            rb.drag = 99999;
        } else
        {
            if (isGround && !isWallrunning)
            {
                rb.drag = GroundDrag;
            }
            else
            {
                if (!isGround && !isWallrunning)
                {
                    rb.drag = airDrag;
                }
            }

            if (!CanMove && !isWallrunning) rb.drag = CrouchDrag; else if (CanMove && isGround && !isWallrunning) rb.drag = GroundDrag;
            if (isWallrunning) rb.drag = WallRunDrag;
        }
    }



    void Jump()
    {
        //rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
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
        uGravity = Gravity;
    }

    void WallRunMovement()
    {

        uGravity = WallRunGravity;
            rb.AddForce(Camera.transform.forward * WallrunSpeed, ForceMode.Force);
            if (WallLeft) rb.AddForce(-orientation.transform.right * 20f, ForceMode.Force);
            if (WallRight) rb.AddForce(orientation.transform.right * 20f, ForceMode.Force);
        

    }

    void OutOfWallLeft(bool JumpL)
    {
        Jumped = 0;
        StopWallrun();
        if(JumpL)
        {
            rb.AddForce(orientation.right * OutWallSide, ForceMode.Impulse);
            rb.AddForce(orientation.up * OutWallUp, ForceMode.Impulse);
        }
    }

    void OutOfWallRight(bool JumpR)
    { 
        Jumped = 0;
        StopWallrun();
        if (JumpR)
        {
            rb.AddForce(-orientation.right * OutWallSide, ForceMode.Impulse);
            rb.AddForce(orientation.up * OutWallUp, ForceMode.Impulse);
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
