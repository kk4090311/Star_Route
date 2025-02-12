using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {  return _instance; }
    }
    [SerializeField] private GameObject player;
    public bool moveLock = false;

    [SerializeField] private StoryManager storyManager;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private ItemsManager itemsManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] public int hp = 100;
    [SerializeField] private int trust = 100;
    [SerializeField] private float electricity = 100f;

    //轉場
    public bool restart = false;
    public bool sceneLoading = false;
    public Vector2 LoadFacing = new Vector2(0, 0);
    public Vector3 LoadPos = new Vector3(0, 0, 0);

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        player = GameObject.Find("Player");
        itemsManager = GetComponent<ItemsManager>();
        uiManager = GetComponent<UIManager>();
        sceneLoading = false;
        moveLock = false;
    }

    private void Update()
    {
        _instance = instance;

        player = GameObject.Find("Player");

        MoveToLoadPos();

        if (DialogueManager.GetInstance().dialogueIsPlaying || uiManager.menuOpen || sceneLoading)
        {
            moveLock = true;
        }
        else
        {
            moveLock = false;
        }
    }

    public static GameManager GetInstance()
    {
        return _instance;
    }

    public void MoveToLoadPos()
    {
        if (sceneLoading)
        {
            player = GameObject.Find("Player");
            player.transform.position = new Vector3(LoadPos.x, LoadPos.y, LoadPos.z);
            player.GetComponent<PlayerControl2DRPG>().ani.SetFloat("hSpeed", LoadFacing.x);
            player.GetComponent<PlayerControl2DRPG>().ani.SetFloat("vSpeed", LoadFacing.y);
            dialogueManager.ExitDialogueMode();
            itemsManager.flashLightMode = 0;
            itemsManager.flashLightOn = false;

            if(restart)
            {
                StartCoroutine(RestartStory());
                restart = false;
            }

            sceneLoading = false;
        }
    }

    private void LoadDialogueManager()
    {
        player = GameObject.Find("Player");
        dialogueManager.dialoguePanel = GameObject.Find("DialoguePanel");
        dialogueManager.dialogueText = GameObject.Find("DialogueText").GetComponent<Text>();
        dialogueManager.dialogueName = GameObject.Find("DialogueName").GetComponent<Text>();
    }

    private IEnumerator RestartStory()
    {
        yield return new WaitForSeconds(0.3f);
        //故事重置
        storyManager.stroyCanPlay.Clear();
        storyManager.AddList("鴨子前輩");
        storyManager.AddList("遇見紅鴨");
        storyManager.AddList("遇上企鵝");
        storyManager.AddList("企鵝是鳥");
    }

}
