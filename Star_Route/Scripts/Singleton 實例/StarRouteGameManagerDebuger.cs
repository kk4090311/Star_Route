using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class StarRouteGameManagerDebuger : MonoBehaviour
{
    // Start is called before the first frame update
    [Title("Debuger__StarRouteGameManager")]
    [LabelText("Debug_StarRouteGameManager //單一實例")] public static StarRouteGameManagerDebuger Instance;
    [LabelText("Debug_StarRoute//遊戲版本")] public string Version;
    [LabelText("Debug_StarRoute//玩家ScriptObjects")] public Player_ScriptObjects player_ScriptObjects;
    void Start()
    {
        Version = StarRouteGameManager.Instance.gameVersion;
        player_ScriptObjects = StarRouteGameManager.Instance.GetPlayer_ScriptObjects();
    }

}
