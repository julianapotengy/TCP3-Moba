using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    Transform inputPanel;
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;
    bool waitingForKey, pressedKey;

    void Start()
    {
        inputPanel = GameObject.Find("InputPanel").GetComponent<Transform>();
        inputPanel.gameObject.SetActive(false);
        waitingForKey = false;
        pressedKey = false;

        for (int i = 0; i < inputPanel.childCount; i++)
        {
            if (inputPanel.GetChild(i).name == "WalkButton")
            {
                inputPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.walk.ToString();
            }
            else if (inputPanel.GetChild(i).name == "StoreButton")
            {
                inputPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.store.ToString();
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !inputPanel.gameObject.activeSelf)
        {
            inputPanel.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && inputPanel.gameObject.activeSelf)
        {
            inputPanel.gameObject.SetActive(false);
        }
    }

    void OnGUI()
    {
        keyEvent = Event.current;
        
        if (waitingForKey)
        {
            if (keyEvent.isKey || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    newKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Mouse0");
                }
                else if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    newKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Mouse1");
                }
                else newKey = keyEvent.keyCode;

                pressedKey = false;
                waitingForKey = false;
            }
        }
    }
    
    public void StartAssignment(string keyName)
    {
        if (!waitingForKey)
        {
            StartCoroutine(AssignKey(keyName));
        }
    }
    
    public void SendText(Text text)
    {
        buttonText = text;
    }
    
    IEnumerator WaitForKey()
    {
        while (pressedKey)
            yield return null;
    }
    
    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;
        pressedKey = true;
        yield return WaitForKey();

        switch (keyName)
        {
            case "walk":
                InputManager.IM.walk = newKey; 
                buttonText.text = InputManager.IM.walk.ToString();
                PlayerPrefs.SetString("walkKey", InputManager.IM.walk.ToString());
                break;

            case "store":
                InputManager.IM.store = newKey; 
                buttonText.text = InputManager.IM.store.ToString(); 
                PlayerPrefs.SetString("storeKey", InputManager.IM.store.ToString()); 
                break;
        }
        yield return null;
    }
}