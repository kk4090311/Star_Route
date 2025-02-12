using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class StarRouteGameManager : MonoBehaviour
{
    // Singleton 實例
    // 獲取 StarRouteManager 的實例
    public static StarRouteGameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {

            Destroy(this.gameObject);
            Initialize();
        }
        else
        {
            Instance = this;
            Initialize();
            DontDestroyOnLoad(this);
        }
    }

    private void Initialize()
    {
        // 添加其他初始化邏輯
        gameVersion = "1.0";
        GetPlayer_ScriptObjects();
        enemy_In_Maps = SysUtils.FindGameObjects_WithTag(SysUtils.Enemy_Tag);
        Debug.Log("StarRouteManager initialized. Version: " + gameVersion);
    }

    // 在這裡添加其他方法和屬性
    [Title("StarRouteGameManager 單一實例")]
    [LabelText("StarRoute//遊戲版本")] public string gameVersion;

    [Title("StarRoute//玩家數據")]
    [LabelText("StarRoute//玩家數據ScriptObjects")] public Player_ScriptObjects player_ScriptObjects;

    [Title("StarRoute//敵人數據")]
    [LabelText("StarRoute//Maps 中的所有敵人(Tag = Enemy)")] public List<GameObject>    enemy_In_Maps;
    [LabelText("StarRoute//目標敵人數據ScriptObjects")] public Enemy_ScriptableObject enemy_ScriptableObjects;
    [Title("UI-Ref//UI數據")]
    [LabelText("UI-Ref//UI-EnterBattle")] public Transform UI_EnterBattle;



    // 例如，你可能想要獲取遊戲版本
    public string GetGameVersion()
    {
        return gameVersion;
    }

    public Player_ScriptObjects GetPlayer_ScriptObjects()
    {
        player_ScriptObjects = SysUtils.Get_Player_ScriptObjects();
        return player_ScriptObjects;
    }

    public Enemy_ScriptableObject GetEnemy_ScriptObjects(GameObject enemy)
    {
        enemy_ScriptableObjects = SysUtils.Get_Enemy_ScriptObjects(enemy);
        return enemy_ScriptableObjects;
    }

}
