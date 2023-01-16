using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockGunTimeout : MonoBehaviour
{
    public  bool CanKnockTO = true;
    [SerializeField] float TimeOutS = 4;


    public void CallKGTimeout()
    {
        StartCoroutine(TimeOut());
    }

    IEnumerator TimeOut()
    {
        CanKnockTO = false;
        yield return new WaitForSeconds(TimeOutS);
        CanKnockTO = true;
    }
}
