using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarRouteSettings;
using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "StarRoute", menuName = "StarRoute/Enemy_ScriptableObject")]
public class Enemy_ScriptableObject : ScriptableObject
{ 

    [Title("敵人數據")]
    [LabelText("敵人名稱")] public string Enemy_Name;
    [LabelText("敵人血量")] public int Enemy_health;
    [LabelText("敵人難度")] public int Enemy_Danger;
    [LabelText("敵人最大速度")] public float Enemy_MoveSpeed;
    [LabelText("敵人搜尋範圍//在MAPS 遊戲畫面中")] public float Enemy_SearchRadius;
    [Title("敵人船艦數據")] public List<GameObject> Enemy_Ship_List;

}
