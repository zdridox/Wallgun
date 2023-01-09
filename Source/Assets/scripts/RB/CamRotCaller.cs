using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotCaller : MonoBehaviour
{
    [SerializeField] float x, y, z, s;
    public mmCam mmCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Call()
    {
        mmCam.Rotatee(x, y, z, s);
    }
}
