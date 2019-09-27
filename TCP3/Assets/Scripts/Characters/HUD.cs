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
            else if (inputPanel.GetChild(i).name == "AutoAtkButton")
            {
                inputPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.autoAtk.ToString();
            }
            else if (inputPanel.GetChild(i).name == "SeeAtkRangeButton")
            {
                inputPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.seeAtkRange.ToString();
            }
            else if (inputPanel.GetChild(i).name == "StoreButton")
            {
                inputPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.store.ToString();
            }
            else if (inputPanel.GetChild(i).name == "Skill1Button")
            {
                inputPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.skill1.ToString();
            }
            else if (inputPanel.GetChild(i).name == "Skill2Button")
            {
                inputPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.skill2.ToString();
            }
            else if (inputPanel.GetChild(i).name == "Skill3Button")
            {
                inputPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.skill3.ToString();
            }
            else if (inputPanel.GetChild(i).name == "UpSkillButton")
            {
                inputPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.upSkill.ToString();
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

            case "autoAtk":
                InputManager.IM.store = newKey;
                buttonText.text = InputManager.IM.store.ToString();
                PlayerPrefs.SetString("autoAtkKey", InputManager.IM.autoAtk.ToString());
                break;

            case "seeAtkRange":
                InputManager.IM.store = newKey;
                buttonText.text = InputManager.IM.store.ToString();
                PlayerPrefs.SetString("seeAtkRangeKey", InputManager.IM.seeAtkRange.ToString());
                break;
            
            case "store":
                InputManager.IM.store = newKey; 
                buttonText.text = InputManager.IM.store.ToString(); 
                PlayerPrefs.SetString("storeKey", InputManager.IM.store.ToString()); 
                break;

            case "skill1":
                InputManager.IM.store = newKey;
                buttonText.text = InputManager.IM.store.ToString();
                PlayerPrefs.SetString("skill1Key", InputManager.IM.skill1.ToString());
                break;

            case "skill2":
                InputManager.IM.store = newKey;
                buttonText.text = InputManager.IM.store.ToString();
                PlayerPrefs.SetString("skill2Key", InputManager.IM.skill2.ToString());
                break;

            case "skill3":
                InputManager.IM.store = newKey;
                buttonText.text = InputManager.IM.store.ToString();
                PlayerPrefs.SetString("skill3Key", InputManager.IM.skill3.ToString());
                break;

            case "upSkill":
                InputManager.IM.store = newKey;
                buttonText.text = InputManager.IM.store.ToString();
                PlayerPrefs.SetString("upSkillKey", InputManager.IM.upSkill.ToString());
                break;
        }
        yield return null;
    }
}