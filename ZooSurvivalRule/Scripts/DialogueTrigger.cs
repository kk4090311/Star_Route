using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private GameObject visualCue;
    [SerializeField] private bool playWhenToutch = false;
    [SerializeField] private bool playOneTime = false;
    [SerializeField] private StoryManager storyManager;

    private bool playerInRange = false;
    private bool dontPlay = false;
    private string storyName = "";

    private void Awake()
    {
        playerInRange = false;
        storyName = this.gameObject.name;
        
        if(inkJSON == null) 
        { Destroy(this.gameObject); }
        if(visualCue == null)
        { visualCue = this.gameObject; }
    }

    private void Update()
    {
        storyManager = GameObject.Find("Manager").GetComponent<StoryManager>();

        if (playOneTime)
        {
            if (storyManager.FindList(storyName))
            { dontPlay = false; }
            else
            { dontPlay = true; }
        }

        if (dontPlay)
        { return; }

        if ( playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            if (playWhenToutch)
            {
                DialogueManager.GetInstance().EnterDiologueMode(inkJSON);
                if (playOneTime)
                { StoryManager.GetInstance().RemoveList(storyName); }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.F) && !DialogueManager.GetInstance().dialogueIsPlaying)
                {
                    DialogueManager.GetInstance().EnterDiologueMode(inkJSON);
                    if (playOneTime)
                    { StoryManager.GetInstance().RemoveList(storyName); }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { playerInRange = true; }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { playerInRange = false; }
    }
}
