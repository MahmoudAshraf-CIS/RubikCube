using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DropMenuDialog : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TMP_Dropdown ddm;
    public Button ok;

    void Awake()
    {
        Hide();
    }

    public DropMenuDialog OnOkay(UnityAction<int> action)
    {
        ok.onClick.RemoveAllListeners();
        

        ok.onClick.AddListener(() => action.Invoke(ddm.value));
        return this;
    }

    public DropMenuDialog Items(string[] items)
    {

        ddm.options.Clear();
        foreach (string t in items)
        {
            ddm.options.Add(new TMP_Dropdown.OptionData() { text = t });
        }
        return this;
    }
    public DropMenuDialog Title(string title)
    {
        this.title.text = title;
        return this;
    }
    public DropMenuDialog Show()
    {
        transform.GetChild(0).transform.gameObject.SetActive(true);
        return this;
    }
    public DropMenuDialog Hide()
    {
        transform.GetChild(0).transform.gameObject.SetActive(false);
        return this;
    }
    private static DropMenuDialog instance;
    public static DropMenuDialog Instance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(DropMenuDialog)) as DropMenuDialog;
            if (!instance)
                Debug.Log("There need to be at least one active DropMenuDialog on the scene");
        }

        return instance;
    }
}
