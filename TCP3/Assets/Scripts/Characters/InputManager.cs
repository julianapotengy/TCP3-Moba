using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // singleton
    public static InputManager IM;

    public KeyCode walk { get; set; }
    public KeyCode store { get; set; }

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
        store = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("storeKey", "P"));
    }
}
