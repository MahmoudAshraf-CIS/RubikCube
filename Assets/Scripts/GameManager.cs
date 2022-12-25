using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ContinueOrNewGame();
        SideMenu sidemenu = SideMenu.Instance();
            sidemenu.OnTimerToggle((val) =>
            {
                // set the timer active = val
            })
            .OnRestartClick(() =>
            {
                sidemenu.Hide();
                Restart();
            })
            .OnMainMenuClick(() =>
            {
                sidemenu.Hide();
                ContinueOrNewGame();
            })
            .OnQuitClick(() =>
            {
                Debug.Log("Quit app");
                sidemenu.Hide();
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
                
            }).OnShow(() =>
            {
                client.SetActive(false);
            }).OnHide(() =>
            {
                client.SetActive(true);
            });
    }

    public RubikCubeClient client;
    
    public void NewGame()
    {
        Debug.Log("New Game, should pick the size...");
        DropMenuDialog sizePicker = DropMenuDialog.Instance();
        sizePicker.Title("Cube Size").Items(new string[] { "2*2", "3*3", "4*4", "5*5", "6*6" })
            .OnOkay((index) =>
            {
                Debug.Log("Create a new game with " + (index + 2).ToString());
                client.Init(index + 2);
                client.SetActive(true);
                sizePicker.Hide();
            })
            .Show();
        client.SetActive(false);
    }


    public void Restart()
    {
        YesNoDialog newGameDialog = YesNoDialog.Instance();
        newGameDialog.Title("Restart?").Message("Rubik Cube Game")
            .OnAccept("Yes", true, () =>
            {
                // Here go to create  a new game with new cube
                newGameDialog.Hide();
                NewGame();
            })
            .OnDecline("No", true, () =>
            {
                newGameDialog.Hide();
                client.SetActive(true);
            })
            .Show();
        client.SetActive(false);
    }
    

    void OldGame()
    {
        Debug.Log("Old Game, should Load the game from player prefs...");
        client.Init();
        client.SetActive(true);
    }

    void ContinueOrNewGame()
    {
        int oldSize = PlayerPrefs.GetInt("RubikCube.size", 0);
        YesNoDialog newGameDialog = YesNoDialog.Instance();
        newGameDialog.Title("Continue proveous game?").Message("Rubik Cube Game")
            .OnAccept("New Game", true, () =>
            {
                // Here go to create  a new game with new cube
                newGameDialog.Hide();
                NewGame();
            })
            .OnDecline("Continue", oldSize > 0, () =>
            {
                newGameDialog.Hide();
                OldGame();
            })
            .Show();
        client.SetActive(false);
    }

     
}
