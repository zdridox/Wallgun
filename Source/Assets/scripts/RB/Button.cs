using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Button : MonoBehaviour
{
    [SerializeField] GameObject levelButton;
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject Spawn;
    [SerializeField] GameObject Player;
    [SerializeField] MovementRB MVRB;
    [SerializeField] mmCam mmCam;
    [SerializeField] Ammo Ammo;
    [SerializeField] CamRB CRB;
    [SerializeField] Gun Gun;
    [SerializeField] End End;
    [SerializeField] GameObject levels;
    [SerializeField] GameObject Main;
    bool LevelOnOff;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowLevels()
    {
        switch (LevelOnOff)
        {
            case (false):
                levelButton.SetActive(true);
                LevelOnOff = true;
                break;
            case (true):
                levelButton.SetActive(false);
                LevelOnOff = false;
                break;
        }
    }

    public void Credits()
    {
        //mmCam.Rotatee(0, 95, 0, 15);
        mmCam.Rotatee(0, 150, 20, 15);
    }

    public void Settings()
    {
        mmCam.Rotatee(90, 0, 0, 15);
    }

    public void Back()
    {
        mmCam.Rotatee(0, 323, 0, 15);
    }

    public void BackLevel()
    {
        mmCam.Rotatee(0, 323, 0, 15);
        levels.SetActive(false);
        Main.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Levels()
    {
        levels.SetActive(true);
        Main.SetActive(false);
        mmCam.Rotatee(0, 300, 5, 15);
    }

    public void Fullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void loadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void loadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Restart()
    {
        Menu.SetActive(false);
        MVRB.Movement = true;
        CRB.Cursorr = false;
        CRB.CanLook = true;
        Gun.canShoot = true;
        Player.transform.position = Spawn.transform.position;
        Ammo.RealAmmo = Ammo.AmmoSetup;
        Ammo.CanDoAmmoStuff = true;
        End.MenuCanOnOff = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
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

