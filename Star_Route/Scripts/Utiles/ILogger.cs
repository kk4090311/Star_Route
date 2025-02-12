using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILogger
{
    void LogMsg(string msg);
}

public class ConsoleLog : ILogger// Switch ConsoleLog
{
    public void LogMsg(string msg)
    {
        Debug.Log(msg);
    }
    


}
public class FileLog : ILogger// Switch Filelog
{
    System.IO.StreamWriter stream;
    public FileLog(string fileName="d://unity.log")
    {
        
        stream = new System.IO.StreamWriter(System.IO.File.Open(fileName,System.IO.FileMode.Append));
         
    }

    ~FileLog()
    {
        if (stream!=null)
            stream.Close();
        stream=null;
    }


    public void LogMsg(string msg)
    {
        if (stream!=null)
        {
           stream.WriteLine($"{System.DateTime.Now.ToString("yy-dd-MM HH:mm:ss ")}{msg}");
        }
    }
}