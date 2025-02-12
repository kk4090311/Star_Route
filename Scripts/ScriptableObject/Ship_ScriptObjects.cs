using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarRouteSettings;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "StarRoute", menuName = "StarRoute/SWeaponSlot")]
public class WeaponSlot : ScriptableObject
{


}
[CreateAssetMenu(fileName = "StarRoute", menuName = "StarRoute/Ship_ScriptObjects")]
public class Ship_ScriptObjects : ScriptableObject
{



    [Title("船艦數據 TYPE")]
    [LabelText("船艦種類")] public ShipType Ship_Type;

    //! 尚未實裝測試 船艦 護頓

    //! 尚未實裝測
    //[Title("船艦上武器的數據 TYPE")]
    //[LabelText("船艦上武器的數據")]public WeaponData Ship_weaponData;  
    //public ShieldType shieldType;

    //[LabelText("武器種類")] AIType Ship_AIType

    //public WeaponData  WeaponData;

    // public EngineType engineType;
    //! 尚未實裝測

    //--------------船艦數據------------------//



    [Title("船艦數據")]
    [LabelText("船艦名稱")] public string Ship_Name;
    [LabelText("船艦最大裝甲")] public float Ship_Hull;
    [LabelText("船艦最大護頓")] public float Ship_Shield;


    [LabelText("船艦最大能量值")] public float Ship_MaxEnergy;
    //! TMP 沒實裝
    [LabelText("船艦能量值")] public float Ship_Energy;

    [LabelText("船艦能量消散速度")] public float Ship_EnergyConsumptionRate;



    [LabelText("船艦最大速度")] public float Ship_MoveSpeed;
    [LabelText("船艦旋轉速度")] public float Ship_RotationSpeed;
    [LabelText("船艦曲速引擎推力")] public float Ship_ThrustingSpeed;
    [LabelText("船艦--搜索狀態距離")] public float Ship_SearchRadius;
    [LabelText("船艦--攻擊狀態距離")] public float Ship_AttackRange;
    [LabelText("船艦--攻擊移動時間--越小越快重新取得新的戰鬥位置")] public float Ship_Attack_Timer;
    [LabelText("船艦武器限制點數")] public float Ship_WeaponLimitPoint;

    [LabelText("船艦顏色")] public Color Ship_Color;
    [LabelText("船艦Big-Icon")] public Sprite Ship_BigIcon;
    [LabelText("船艦Small-Icon")] public Sprite Ship_SmallIcon;


}
