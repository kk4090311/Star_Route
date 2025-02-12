using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseDead : MonoBehaviour
{
    [SerializeField] private GameObject goose;
    [SerializeField] private GameObject goosebody;
    [SerializeField] private StoryManager storyManager;

    private void Awake()
    {
        goosebody.SetActive(false);
    }

    private void Update()
    {
        if (storyManager == null)
        {
            storyManager = GameObject.Find("Manager").GetComponent<StoryManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "¶WÃZ")
        {
            Destroy(goose);
            goosebody.SetActive(true);
            storyManager.RemoveList("¥øÃZ¬O³¾");
            storyManager.AddList("¥øÃZ±þÃZ");
        }
    }
}
