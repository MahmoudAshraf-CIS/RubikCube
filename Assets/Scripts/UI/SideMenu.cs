using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SideMenu : MonoBehaviour
{
    [SerializeField]
    Toggle timerToggle;
    [SerializeField]
    Button restartBtn;
    [SerializeField]
    Button mainMenuBtn;
    [SerializeField]
    Button quitBtn;

    UnityAction onShowSideMenu, onHideSideMenu;

    public SideMenu OnTimerToggle(UnityAction<bool> OnToggle)
    {
        timerToggle.onValueChanged.RemoveAllListeners();
        timerToggle.onValueChanged.AddListener(OnToggle);
        return this;
    }

    public SideMenu OnRestartClick(UnityAction OnRestart)
    {
        restartBtn.onClick.RemoveAllListeners();
        restartBtn.onClick.AddListener(OnRestart);
        return this;
    }

    public SideMenu OnMainMenuClick(UnityAction OnMainMenu)
    {
        mainMenuBtn.onClick.RemoveAllListeners();
        mainMenuBtn.onClick.AddListener(OnMainMenu);
        return this;
    }
    public SideMenu OnQuitClick(UnityAction OnQuit)
    {
        quitBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.AddListener(OnQuit);
        return this;
    }

    public void Toggle()
    {
        if (transform.GetChild(0).transform.gameObject.activeInHierarchy)
            Hide();
        else
            Show();
    }
    public SideMenu Show()
    {
        transform.GetChild(0).transform.gameObject.SetActive(true);
        if (onShowSideMenu != null)
            onShowSideMenu.Invoke();

        return this;
    }

    
    public SideMenu OnShow(UnityAction OnShowMenu)
    {
        onShowSideMenu = OnShowMenu;
        return this;
    }

    public SideMenu Hide()
    {
        transform.GetChild(0).transform.gameObject.SetActive(false);
        if (onHideSideMenu != null)
            onHideSideMenu.Invoke();

        return this;
    }
    public SideMenu OnHide(UnityAction OnHideMenu)
    {
        onHideSideMenu = OnHideMenu;
        return this;
    }
    private static SideMenu instance;
    public static SideMenu Instance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(SideMenu)) as SideMenu;
            if (!instance)
                Debug.Log("There need to be at least one active DropMenuDialog on the scene");
        }

        return instance;
    }
}
