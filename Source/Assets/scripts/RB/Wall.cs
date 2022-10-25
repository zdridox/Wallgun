using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    Rigidbody rb;
    public Transform L;
    public Transform R;
    bool bL, bR;
    public LayerMask WallStick;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
    }

    private void Update()
    {
        bL = Physics.Raycast(L.transform.position, L.transform.forward, 5f, WallStick);
        bR = Physics.Raycast(R.transform.position, R.transform.forward, 5f, WallStick);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.transform.tag == "WallStick")
        {
            transform.rotation = collision.transform.rotation;
            rb.freezeRotation = true;
            if (bL) rb.AddForce(L.transform.forward * 500, ForceMode.Impulse);
            if (bR) rb.AddForce(R.transform.forward * 500, ForceMode.Impulse);
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

}
