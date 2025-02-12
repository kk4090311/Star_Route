using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manual : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    private void Start()
    {
        uiManager = GameObject.Find("Manager").GetComponent<UIManager>();
    }

    public void NextPage()
    {
        if (!uiManager.menuOpen)
        {
            if (uiManager.manualSheet < uiManager.sheets.Count - 1)
            {
                uiManager.manualSheet += 1;
            }
        }
    }
    public void PreviousPage()
    {
        if (!uiManager.menuOpen)
        {
            if (uiManager.manualSheet > 0)
            {
                uiManager.manualSheet -= 1;
            }
        }
    }

    public void OpenManual()
    {
        if(!uiManager.menuOpen)
        {
            uiManager.manualOpen = !uiManager.manualOpen;
        }
    }

}
