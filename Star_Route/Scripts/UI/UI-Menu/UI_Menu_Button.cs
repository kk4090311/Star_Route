using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Menu_Button : MonoBehaviour
{
    public void UI_StarRoute_StartGame()
    {
        SceneManager.LoadScene("StarRoute_Maps_Play");
    }

}
