using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dieontouch : MonoBehaviour
{
    [SerializeField] GameObject Spawn;

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
        if(collision.transform.tag == "Player")
        {
            collision.transform.position = Spawn.transform.position;
        }
    }
}
