using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfMapKiller : MonoBehaviour
{
    public Transform spawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.position = spawn.transform.position;
    }
}
