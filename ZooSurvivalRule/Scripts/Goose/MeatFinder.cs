using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatFinder : MonoBehaviour
{
    [SerializeField] private GameObject[] meat;
    private StoryManager storyManager;

    private void Awake()
    {
        storyManager = GameObject.Find("Manager").GetComponent<StoryManager>();
    }

    void Update()
    {        
        meat = GameObject.FindGameObjectsWithTag("Meat");

        if(meat.Length == 0)
        {
            storyManager.finishDay1 = true;
        }
    }
}
