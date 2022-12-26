using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField]
    private Button menu,undo;

    public GamePlayUI Activate()
    {
        menu.interactable = true;
        undo.interactable = true;
        return this;         
    }
    public GamePlayUI DeActivate()
    {
        menu.interactable = false;
        undo.interactable = false;
        return this;
    }

    private static GamePlayUI instance;
    public static GamePlayUI Instance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(GamePlayUI)) as GamePlayUI;
            if (!instance)
                Debug.Log("There need to be at least one active DropMenuDialog on the scene");
        }

        return instance;
    }
}
