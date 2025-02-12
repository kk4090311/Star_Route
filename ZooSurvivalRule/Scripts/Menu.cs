using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private StoryManager storyManager;
    private UIManager uIManager;
    private GameManager gameManager;

    private void Awake()
    {
        storyManager = GameObject.Find("Manager").GetComponent<StoryManager>();
        uIManager = GameObject.Find("Manager").GetComponent<UIManager>();
        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
    }

    public void Restart()
    {
        //UI­«¸m
        uIManager.openUI = false;
        uIManager.menuOpen = false;

        //loadscene
        gameManager.LoadPos = new Vector3(1.5f, -2.5f, 0f);
        gameManager.LoadFacing = new Vector2(0, -1);
        gameManager.restart = true;
        gameManager.sceneLoading = true;

        SceneManager.LoadScene("StaffCabin");
        
    }


}
