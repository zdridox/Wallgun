using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kicker : MonoBehaviour
{
    [SerializeField] Rigidbody PlayerRB;
    RaycastHit hit;
    bool canKick = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.up, out hit, 5) && hit.transform.tag == "Player")
        {
            if (canKick) StartCoroutine(Kickerr());
        }
    }

    IEnumerator Kickerr()
    {
        canKick = false;
        PlayerRB.velocity = PlayerRB.velocity * 5;
        yield return new WaitForSeconds(3);
        canKick = true;
    }
}
