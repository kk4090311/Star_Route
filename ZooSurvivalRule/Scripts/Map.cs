using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    private void Start()
    {
        uiManager = GameObject.Find("Manager").GetComponent<UIManager>();
    }
     /*
    public void OpenMap()
    {
        uiManager.mapOpen = !uiManager.mapOpen;
    }
     */
}
