using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float elapsedTime = 0;
    [SerializeField]
    private TextMeshProUGUI time;
    [SerializeField]
    private bool active = false;
    public string GetElapsedTime()
    {
        float minutes = Mathf.Floor(elapsedTime / 60);
        float seconds = Mathf.RoundToInt(elapsedTime % 60);
        return minutes+":"+seconds;
    }

    public void Reset()
    {
        elapsedTime = 0;
        Update();
    }
    public void Activate()
    {
        active = true;
    }
    public void DeActivate()
    {
        active = false;
    }
    void Start()
    {
        elapsedTime = PlayerPrefs.GetFloat("elapsedtime", 0);
        time.text = GetElapsedTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
            elapsedTime += Time.deltaTime;

        time.text = GetElapsedTime();
    }

    private static Timer instance;
    public static Timer Instance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(Timer)) as Timer;
            if (!instance)
                Debug.Log("There need to be at least one active OkDialog on the scene");
        }

        return instance;
    }

    private void OnDestroy()
    {
        //Debug.Log(model.Size);
        PlayerPrefs.SetFloat("elapsedtime", elapsedTime);
    }

    public Timer Show()
    {
        time.gameObject.SetActive(true);
        return this;
    }
    public Timer Hide()
    {
        time.gameObject.SetActive(false);
        return this;
    }

    public void Togle()
    {
        time.gameObject.SetActive(!time.gameObject.activeInHierarchy);
    }
}
