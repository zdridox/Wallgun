using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Button : MonoBehaviour
{
    [SerializeField] GameObject levelButton;
    [SerializeField] GameObject Menu;
    [SerializeField] MovementRB MVRB;
    [SerializeField] CamRB CRB;
    [SerializeField] Gun Gun;
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

    }

    public void Settings()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void loadLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Restart()
    {
        Menu.SetActive(false);
        MVRB.Movement = true;
        CRB.Cursorr = false;
        CRB.CanLook = true;
        Gun.canShoot = true;
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
