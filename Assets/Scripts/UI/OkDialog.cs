using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Events;
using TMPro;
[RequireComponent(typeof(CanvasGroup))]
public class OkDialog : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI message;
    [SerializeField]
    private TextMeshProUGUI btn1Txt, btn2Txt, btn3Txt;
    [SerializeField]
    private Button btn1,btn2,btn3;

     

    void Awake()
    {
        Hide();       
    }

    public OkDialog On1(string text, UnityAction action)
    {
        btn1Txt.text = text;
        btn1.onClick.RemoveAllListeners();
        btn1.onClick.AddListener(action);
        return this;
    }
    public OkDialog On2(string text, UnityAction action)
    {
        btn2Txt.text = text;
        btn2.onClick.RemoveAllListeners();
        btn2.onClick.AddListener(action);
        return this;
    }
    public OkDialog On3(string text, UnityAction action)
    {
        btn3Txt.text = text;
        btn3.onClick.RemoveAllListeners();
        btn3.onClick.AddListener(action);
        return this;
    }

    public OkDialog Title(string title)
    {
        this.title.text = title;
        return this;
    }

    public OkDialog Message(string message)
    {
        this.message.text = message;
        return this;
    }

    // show the dialog, set it's canvasGroup.alpha to 1f or tween like here
    public OkDialog Show()
    {
        transform.GetChild(0).transform.gameObject.SetActive(true);
        return this;
    }
 
    public OkDialog Hide()
    {
        transform.GetChild(0).transform.gameObject.SetActive(false);
        return this;
    }
  

    private static OkDialog instance;
    public static OkDialog Instance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(OkDialog)) as OkDialog;
            if (!instance)
                Debug.Log("There need to be at least one active OkDialog on the scene");
        }

        return instance;
    }

}