using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Facing
{
    Front,
    Back,
    Left,
    Right
}

public class SceneGate : MonoBehaviour
{
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private GameObject player;
    [SerializeField] private string loadSceneName;
    [SerializeField] private Vector3 playerLoadPos;
    private bool canLoad = false;

    public Facing facing = Facing.Front;
    
    private void Update()
    {
        player = GameObject.Find("Player");
        gameManager = FindObjectOfType<GameManager>();

        if (canLoad)
        {
            gameManager.LoadPos = new Vector3(playerLoadPos.x, playerLoadPos.y, playerLoadPos.z);

            if (facing == Facing.Front)
            { gameManager.LoadFacing = new Vector2(0, -1); }
            if (facing == Facing.Left)
            { gameManager.LoadFacing = new Vector2(-1, 0); }
            if (facing == Facing.Right)
            { gameManager.LoadFacing = new Vector2(1, 0); }
            if (facing == Facing.Back)
            { gameManager.LoadFacing = new Vector2(0, 1); }

            StartCoroutine(Loading());
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canLoad = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canLoad = false;
    }

    private IEnumerator Loading()
    {
        yield return new WaitForSeconds(0.3f);
        gameManager.sceneLoading = true;
        SceneManager.LoadScene(loadSceneName);
    }

}
