using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager _storyInstance;
    public StoryManager storyInstance;
    public List<string> stroyCanPlay = new List<string>();

    public bool finishDay1 = false;

    public static StoryManager GetInstance()
    {
        return _storyInstance;
    }

    private void Awake()
    {
        storyInstance = this;
    }

    private void Update()
    {
        _storyInstance = storyInstance;

        if (finishDay1 && FindList("完成工作") == false)
        {
            AddList("完成工作");
        }
        else
        {
            if (FindList("未完工作") == false)
            {
                AddList("未完工作");
            }
        }
    }

    public bool FindList(string name)
    {
        if (stroyCanPlay.Find(s => s == name) != null)
        { return true; }
        else
        { return false; }
    }
    public void AddList(string name)
    {
        stroyCanPlay.Add(name);
    }
    public void RemoveList(string name)
    {
        stroyCanPlay.Remove(name);
    }

    
}
