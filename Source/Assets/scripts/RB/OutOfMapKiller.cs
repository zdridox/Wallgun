using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfMapKiller : MonoBehaviour
{
    public Transform spawn;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player") Player.transform.position = spawn.transform.position;  else
        {
            other.transform.position = spawn.transform.position;
        }
    }
}
