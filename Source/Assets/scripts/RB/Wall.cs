using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    Rigidbody rb;
    public Transform L, Lb, R, Rb, MUL, MULb, MUR, MURb;
    bool bL, bR, bLb, bRb, Mul, Mur, Mulb, Murb;
    bool DontKill;
    float DontKillTime = 10;
    [SerializeField] LayerMask WallStick;
    [SerializeField] int rayLen = 30;
    public bool showRaycast;
    bool firstHit;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        StartCoroutine(DontKillDoTime());
    }

    private void Update()
    {
        if(showRaycast)
        {
            Debug.DrawRay(L.transform.position, L.transform.forward * rayLen);
            Debug.DrawRay(R.transform.position, R.transform.forward * rayLen);
            Debug.DrawRay(Lb.transform.position, Lb.transform.forward * rayLen);
            Debug.DrawRay(Rb.transform.position, Rb.transform.forward * rayLen);
            Debug.DrawRay(MUL.transform.position, MUL.transform.forward * rayLen);
            Debug.DrawRay(MULb.transform.position, MULb.transform.forward * rayLen);
            Debug.DrawRay(MUR.transform.position, MUR.transform.forward * rayLen);
            Debug.DrawRay(MURb.transform.position, MURb.transform.forward * rayLen);
        }
    }

    IEnumerator DontKillDoTime()
    {
        yield return new WaitForSeconds(DontKillTime);
        if (!DontKill) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.transform.tag == "WallStick")
        {
            if(!firstHit)
            {
                firstHit = true;
                DontKill = true;
                transform.rotation = collision.transform.rotation;
                rb.freezeRotation = true;
                rb.velocity = new Vector3(0, 0, 0);
                bL = Physics.Raycast(L.transform.position, L.transform.forward, rayLen, WallStick);
                bR = Physics.Raycast(R.transform.position, R.transform.forward, rayLen, WallStick);
                bLb = Physics.Raycast(Lb.transform.position, Lb.transform.forward, rayLen, WallStick);
                bRb = Physics.Raycast(Rb.transform.position, Rb.transform.forward, rayLen, WallStick);
                Mul = Physics.Raycast(MUL.transform.position, MUL.transform.forward, rayLen, WallStick);
                Mulb = Physics.Raycast(MULb.transform.position, MULb.transform.forward, rayLen, WallStick);
                Mur = Physics.Raycast(MUR.transform.position, MUR.transform.forward, rayLen, WallStick);
                Murb = Physics.Raycast(MURb.transform.position, MURb.transform.forward, rayLen, WallStick);

                if ((bL || bLb || Mur || Murb)) { rb.AddForce(-transform.right * 30, ForceMode.Impulse); Debug.Log(""); };
                if ((bR || bRb || Mul || Mulb)) { rb.AddForce(transform.right * 30, ForceMode.Impulse); Debug.Log(""); };
            } else
            {
                rb.mass = 9999;
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