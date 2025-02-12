using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarRouteSettings;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "StarRoute", menuName = "StarRoute/Player_ScriptObjects")]
public class Player_ScriptObjects : ScriptableObject
{

    [Title("玩家數據")]
    [LabelText("玩家名稱")] public string Player_Name;
    [LabelText("玩家血量")] public int Player_health;


    [LabelText("玩家最大速度")] public float Player_MoveSpeed;
    [LabelText("玩家曲速引擎推力")] public float Player_ThrustingSpeed;
    [LabelText("玩家旋轉速度")] public float Player_RotationSpeed;
    [LabelText("玩家搜尋範圍//在MAPS 遊戲畫面中")] public float Player_SearchRadius;

    [LabelText("玩家各類數據Place")] public float Player_Money;

    [LabelText("玩家挖礦小隊Prefabs")] public GameObject Player_CrewPrefab;
    [LabelText("玩家挖礦小隊數量")] public int Player_Crews;

    [LabelText("玩家資源礦物")] public int Player_Resource;

    [Title("玩家船艦數據")] public List<GameObject> Player_Ship_List;

}
