using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] GameObject ammo0, ammo1, ammo2, ammo3, ammo4;
    public int AmmoSetup = 0;
    public static int RealAmmo;
    public static bool AmmoZero;
    public bool CanDoAmmoStuff = true;
    // Start is called before the first frame update
    void Start()
    {
        RealAmmo = AmmoSetup;
    }

    // Update is called once per frame
    void Update()
    {
        if(CanDoAmmoStuff)
        {
            switch (RealAmmo)
            {
                case (0):
                    changeNumber(0);
                    break;
                case (1):
                    changeNumber(1);
                    break;
                case (2):
                    changeNumber(2);
                    break;
                case (3):
                    changeNumber(3);
                    break;
                case (4):
                    changeNumber(4);
                    break;
            }

            if (RealAmmo == 0)
            {
                AmmoZero = true;
            }
            else AmmoZero = false;
        }
    }

    void changeNumber(int Num)
    {
        ammo0.SetActive(false);
        ammo1.SetActive(false);
        ammo2.SetActive(false);
        ammo3.SetActive(false);
        ammo4.SetActive(false);

        switch (Num)
        {
            case (0):
                ammo0.SetActive(true);
                break;
            case (1):
                ammo1.SetActive(true);
                break;
            case (2):
                ammo2.SetActive(true);
                break;
            case (3):
                ammo3.SetActive(true);
                break;
            case (4):
                ammo4.SetActive(true);
                break;
        }
    }
}
