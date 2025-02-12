using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLog
{

    public void Clear() => mesg_list.Clear();
    public void Add(string name, string value)
    {
        if (IsDebug)
        {
            mesg_list.Add($"{mesg_list.Count}.{name}", value);
        }
    }
    public bool IsDebug = false;
    //-------------------------------------------------//
    Dictionary<string, string> mesg_list = new Dictionary<string, string>();
    public DebugLog(bool isdebug)
    {
        IsDebug = isdebug;
    }
    public void ShowMesg()
    {
        if (IsDebug)
        {
            string txt = "";
            foreach (var key in mesg_list.Keys)
            {
                if (txt != "")
                {
                    txt += ",";
                }
                txt += $"{key}={mesg_list[key]}";
            }
            Debug.Log(txt);
        }

    }

}
