using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mmCam : MonoBehaviour
{
     float WrotX, WrotY, WrotZ;
     float rotSpeed;
     bool rot;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
        if(rot)
        {
            Vector3 to = new Vector3(WrotX, WrotY, WrotZ);
            if(Vector3.Distance(transform.eulerAngles, to) > 0.01f)
            {
                transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime * rotSpeed);
            } else
            {
                transform.eulerAngles = to;
                rot = false;
            }
        }

    }

    public void Rotatee(float wrx, float wry, float wrz, float rots)
    {
        WrotX = wrx;
        WrotY = wry;
        WrotZ = wrz;
        rotSpeed = rots;
        rot = true;
    }
}
