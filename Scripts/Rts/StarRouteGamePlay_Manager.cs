using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class StarRouteGamePlay_Manager : MonoBehaviour
{
    [Title("StarRouteGamePlay 戰鬥畫面控制器")]

    [LabelText("StarRouteGameManager 單例船艦列表 (Prefab)")] public List<GameObject> Instance_Ship_List;
    [LabelText("StarRouteGamePlay-我方船艦")] public List<GameObject> Local_Ship_List;
    [LabelText("StarRouteGamePlay-我方船艦--實例化")] public List<GameObject> Local_Ship_Instance_List;
    [LabelText("StarRouteGamePlay-敵方方船艦")] public List<GameObject> Local_Enemy_Ship_List;

    [LabelText("StarRouteGamePlay-我方船艦--實例化")] public List<GameObject> Local_Enemy_Instance_Ship_List;

    [LabelText("StarRouteGamePlay-UI-RTS-ReturnPanel")] public GameObject UI_RTS_ReturnPanel;
    private void Awake()
    {
        GamePlayInitialize();

    }
    private void FixedUpdate()
    {
        RTS_GameCheck(); // 檢查遊戲是否結束

    }

    private void GamePlayInitialize()
    {
        init_Player_Ships();
        init_Enemy_Ships();
    }

    private void init_Player_Ships()
    {

        if (StarRouteGameManager.Instance.player_ScriptObjects.Player_Ship_List.Count > 0)
        {
            Local_Ship_List = StarRouteGameManager.Instance.player_ScriptObjects.Player_Ship_List;
            RTS_ShipArranger.ArrangeShips(Local_Ship_List);
        }
        else Debug.Log("沒有船艦列表"); return;
    }
    private void init_Enemy_Ships()
    {
        if (StarRouteGameManager.Instance.enemy_ScriptableObjects.Enemy_Ship_List.Count > 0)
        {
            Local_Enemy_Ship_List = StarRouteGameManager.Instance.enemy_ScriptableObjects.Enemy_Ship_List;
            RTS_ShipArranger.ArrangeShips(Local_Enemy_Ship_List, new Vector3(0, 60, 0));
        }
        else Debug.Log("沒有船艦列表"); return;
    }

    private void RTS_GameCheck()
    {
        foreach (var item in Local_Ship_Instance_List)
        {
            if (item == null)
            {
                Local_Ship_Instance_List.Remove(item);
            }
        }
        foreach (var item in Local_Enemy_Instance_Ship_List)
        {
            if (item == null)
            {
                Local_Enemy_Instance_Ship_List.Remove(item);
            }
        }
        if (Local_Ship_Instance_List.Count == 0)
        {
            Debug.Log("我方遊戲結束");
            UI_RTS_ReturnPanel.SetActive(true);

        }
        if (Local_Enemy_Instance_Ship_List.Count == 0)
        {
            Debug.Log("敵方遊戲結束");
            UI_RTS_ReturnPanel.SetActive(true);
        }
    }

}
