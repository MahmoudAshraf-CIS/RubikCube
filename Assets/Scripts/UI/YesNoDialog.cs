using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Events;
using TMPro;
[RequireComponent(typeof(CanvasGroup))]
public class YesNoDialog : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI message;
    [SerializeField]
    private TextMeshProUGUI accept, decline;
    [SerializeField]
    private Button acceptButton, declineButton;

     

    void Awake()
    {
        Hide();       
    }

    public YesNoDialog OnAccept(string text,bool interactible, UnityAction action)
    {
        accept.text = text;
        acceptButton.onClick.RemoveAllListeners();
        acceptButton.onClick.AddListener(action);
        acceptButton.interactable = interactible;
        return this;
    }

    public YesNoDialog OnDecline(string text, bool interactible, UnityAction action)
    {
        decline.text = text;
        declineButton.onClick.RemoveAllListeners();
        declineButton.onClick.AddListener(action);
        declineButton.interactable = interactible;
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
    public YesNoDialog Show()
    {
        transform.GetChild(0).transform.gameObject.SetActive(true);
        return this;
    }
    public YesNoDialog Show(UnityAction OnShow)
    {
        transform.GetChild(0).transform.gameObject.SetActive(true);
        OnShow.Invoke();
        return this;
    }
    public YesNoDialog Hide()
    {
        transform.GetChild(0).transform.gameObject.SetActive(false);
        return this;
    }
    public YesNoDialog Hide(UnityAction OnHide)
    {
        transform.GetChild(0).transform.gameObject.SetActive(false);
        OnHide.Invoke();
        return this;
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