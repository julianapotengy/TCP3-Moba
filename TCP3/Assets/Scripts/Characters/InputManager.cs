using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // singleton
    public static InputManager IM;

    public KeyCode walk { get; set; }
    public KeyCode autoAtk { get; set; }
    public KeyCode seeAtkRange { get; set; }
    public KeyCode store { get; set; }
    public KeyCode skill1 { get; set; }
    public KeyCode skill2 { get; set; }
    public KeyCode skill3 { get; set; }
    public KeyCode upSkill { get; set; }

    void Awake()
    {
        // singleton pattern
        if (IM == null)
        {
            DontDestroyOnLoad(gameObject);
            IM = this;
        }
        else if (IM != this)
        {
            Destroy(gameObject);
        }
        
        walk = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("walkKey", "Mouse1"));
        autoAtk = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("autoAtkKey", "Mouse1"));
        seeAtkRange = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("seeAtkRangeKey", "A"));
        store = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("storeKey", "P"));
        skill1 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("skill1Key", "Q"));
        skill2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("skill2Key", "W"));
        skill3 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("skill3Key", "E"));
        upSkill = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("upSkillKey", "LeftControl"));
    }
}
