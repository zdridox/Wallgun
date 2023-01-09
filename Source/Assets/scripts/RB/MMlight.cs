using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMlight : MonoBehaviour
{
    public Light light1;
    public Light light2;
    public Light light3;
    public Light light4;
    public float rng;

    // Start is called before the first frame update
    void Start()
    {
        light1.intensity = 60;
        light2.intensity = 0.3f;
        light3.intensity = 0.3f;
        light4.intensity = 0.8f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        rng = Random.Range(0, 1000);

        if(rng < 800 && rng > 770)
        {
            Flicker();
        }
    }

    void Flicker()
    {
        light1.intensity = 0;
        light2.intensity = 0;
        light3.intensity = 0;
        light4.intensity = 0;
        StartCoroutine(zerto80());
            
        
    }

    IEnumerator zerto80()
    {
        yield return new WaitForSeconds(0.2f);
        light1.intensity = 60;
        light2.intensity = 0.3f;
        light3.intensity = 0.3f;
        light4.intensity = 0.8f;
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
