using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    private static ItemsManager _instance;
    public static ItemsManager instance
    {
        get
        { return _instance; }
    }

    public bool flashLightOn = false;
    public int flashLightMode = 0;
    public bool gun = false;
    public bool dartGun = false;

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        SwitchLightMode();
    }

    private void SwitchLightMode()
    {
        if (Input.GetKeyDown(KeyCode.X))
        { flashLightOn = !flashLightOn; }

        if (flashLightOn)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            { flashLightMode = 0; }
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            { flashLightMode = 1; }
        }
        
    }
}
