using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Events;
using TMPro;
[RequireComponent(typeof(CanvasGroup))]
public class YesNoDialog : MonoBehaviour
{

    public TextMeshProUGUI title;
    public TextMeshProUGUI message;
    public TextMeshProUGUI accept, decline;
    public Button acceptButton, declineButton;

     

    void Awake()
    {
        Hide();       
    }

    public YesNoDialog OnAccept(string text, UnityAction action)
    {
        accept.text = text;
        acceptButton.onClick.RemoveAllListeners();
        acceptButton.onClick.AddListener(action);
        return this;
    }



    public YesNoDialog OnDecline(string text, UnityAction action)
    {
        decline.text = text;
        declineButton.onClick.RemoveAllListeners();
        declineButton.onClick.AddListener(action);
        return this;
    }

    public YesNoDialog Title(string title)
    {
        this.title.text = title;
        return this;
    }

    public YesNoDialog Message(string message)
    {
        this.message.text = message;
        return this;
    }

    // show the dialog, set it's canvasGroup.alpha to 1f or tween like here
    public void Show()
    {
        transform.GetChild(0).transform.gameObject.SetActive(true);
    }

    public void Hide()
    {
        transform.GetChild(0).transform.gameObject.SetActive(false);
    }

    private static YesNoDialog instance;
    public static YesNoDialog Instance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(YesNoDialog)) as YesNoDialog;
            if (!instance)
                Debug.Log("There need to be at least one active GenericDialog on the scene");
        }

        return instance;
    }

}