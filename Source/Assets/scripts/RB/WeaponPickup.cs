using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] GameObject Weapon, Prop;
    [SerializeField] Transform Camera;
    [SerializeField] LayerMask WeaponPick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.E) && Physics.Raycast(Camera.position, Camera.forward, 5f, WeaponPick))
        {
            Prop.SetActive(false);
            Weapon.SetActive(true);
            Wall.DestroyWalls = false;
        }
    }
}
