using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GooseKill : MonoBehaviour
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GooseRestart();
        }
    }

    public void GooseRestart()
    {
        //UI­«¸m
        uIManager.openUI = false;
        uIManager.menuOpen = false;

        //loadscene
        gameManager.LoadPos = new Vector3(7.2f, -2.5f, 0f);
        gameManager.LoadFacing = new Vector2(0, -1);
        gameManager.restart = true;
        gameManager.sceneLoading = true;

        SceneManager.LoadScene("StaffCabin");

    }
}
