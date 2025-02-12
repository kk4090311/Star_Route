using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager _instance;
    public DialogueManager instance;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameObject player;

    public GameObject dialoguePanel;
    public Text dialogueText;
    public Text dialogueName;
    public bool diaSceneLoading;
    [SerializeField] private List<string> tags;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            print("Found more than one Dialogue Manager in this scene");
        }
        instance = this;

        gameManager = GetComponent<GameManager>();
        uiManager = GetComponent<UIManager>();
    }

    public static DialogueManager GetInstance()
    {
        return _instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
    }

    private void Update()
    {
        _instance = instance;

        player = GameObject.Find("Player");

        if (!dialogueIsPlaying)
        {
            dialoguePanel.SetActive(false);
        }

        if (dialogueIsPlaying && Input.GetKeyDown(KeyCode.F))
        {
            ContinueStory();
        }

        //更新tag
        if (dialogueIsPlaying)
        {
            tags = currentStory.currentTags;
        }

        if (pressFSpeedLock)
        {
            StartCoroutine(FSpeedTime());
        }
    }

    private void LateUpdate()
    {
        InkyControl();
    }

    public void EnterDiologueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }
    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            //顯示下一句故事
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            ExitDialogueMode();
        }
    }
    IEnumerator dialogueIsPlayingFalse()
    {
        yield return new WaitForSeconds(0.5f);
        dialogueIsPlaying = false;
    }
    public void ExitDialogueMode()
    {
        StartCoroutine(dialogueIsPlayingFalse());
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        dialogueName.text = "";
    }

    private void DialogueName()
    {
        if (tags.Count == 0)
        {
            dialogueName.text = "";
        }

        if (tags.Count == 1)
        {
            dialogueName.text = tags[0];
        }

    }

    private void InkyControl()
    {
        DialogueName();
    }

    private bool pressFSpeedLock = false;
    private IEnumerator FSpeedTime()
    {
        yield return new WaitForSeconds(0.1f);
        pressFSpeedLock = false;
    }

}

