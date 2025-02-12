using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Setting_TabGroup : MonoBehaviour
{
    public List<UI_Setting_TabButton> TabButton_List;
    public List<GameObject> UI_Setting_Page;
    
    public void TabGroup_AddToList(UI_Setting_TabButton button)
    {
        if (TabButton_List == null)
        {
            TabButton_List = new List<UI_Setting_TabButton>();
        }
        TabButton_List.Add(button);
    }
    public void OnTabEnter(UI_Setting_TabButton button)
    {
        Debug.Log(button.name + "OnTabEnter");
    }
    public void OnTabExit(UI_Setting_TabButton button)
    {
        Debug.Log(button.name + "OnTabExit");
    }
    public void OnTabSelected(UI_Setting_TabButton button)
    {
        Debug.Log(button.name + "OnTabSelected");



        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < UI_Setting_Page.Count; i++)
        {
            if (i == index)
            {
                UI_Setting_Page[i].gameObject.SetActive(true);
            }
            else
            {
                UI_Setting_Page[i].gameObject.SetActive(false);
            }
        }
    }
}
