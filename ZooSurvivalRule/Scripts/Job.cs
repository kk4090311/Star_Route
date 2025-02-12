using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    private void Start()
    {
        uiManager = GameObject.Find("Manager").GetComponent<UIManager>();
    }

    public void OpenJob()
    {
        if(!uiManager.menuOpen)
        {
            uiManager.jobOpen = !uiManager.jobOpen;
        }
    }
}
