using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using StarRouteSettings;
using UnityEngine;

public class StarRoute_BaseShip : MonoBehaviour, IDamageable
{
    [Title("StarRoute_各自船艦")]
    [LabelText("船艦數據放置ScriptObjects")] public Ship_ScriptObjects ship_ScriptObjects;

    [LabelText("自動獲取--船艦數據-血條 ")] public Ship_HealthBar ship_HealthBar;

    [Title("BaseShip--船艦數據")]
    [LabelText("自動獲取--船艦名稱")] public string ship_Name;
    [LabelText("自動獲取--船艦裝甲")] public float ship_Hull;

    [LabelText("自動獲取--船艦護頓")] public float ship_Shield;
    [LabelText("自動獲取--搜索狀態距離")] public float ship_SearchRadius;
    [LabelText("自動獲取--攻擊狀態距離")] public float ship_AttackRange;
    [LabelText("自動獲取--移動速度")] public float ship_MovementSpeed;
    [LabelText("自動獲取--旋轉速度")] public float ship_RotationSpeed;
    [LabelText("自動獲取--船艦敵人")] public List<GameObject> ship_TargetList;
    [LabelText("自動獲取--目前最接近的敵人")] public GameObject Target;

    protected virtual void Start()
    {
        Init_ShipData();
    }
    protected virtual void FixedUpdate()
    {
        // 每隔 10 秒执行一次 AI_FindTarget 重新獲得最近目標
        InvokeRepeating(nameof(FindNearest_Target), 0f, 10f);
    }
    protected virtual void Init_ShipData()
    {
        //color2------------船艦數據----------------//
        ship_Hull = ship_ScriptObjects.Ship_Hull;
        ship_Shield = ship_ScriptObjects.Ship_Shield;
        ship_Name = ship_ScriptObjects.Ship_Name;
        ship_MovementSpeed = ship_ScriptObjects.Ship_MoveSpeed;
        ship_RotationSpeed = ship_ScriptObjects.Ship_RotationSpeed;
        ship_SearchRadius = ship_ScriptObjects.Ship_SearchRadius;
        ship_AttackRange = ship_ScriptObjects.Ship_AttackRange;

        //color2------------UI-船艦數據-血條----------------//
        ship_HealthBar = gameObject.GetComponentInChildren<Ship_HealthBar>();
        ship_HealthBar.Init_Ship_HealthBar();
        ship_HealthBar.SetMaxHealth(ship_Hull);
    }
    protected virtual void FindNearest_Target()
    {
        // Debug.Log(transform.name + "    " + ship_SearchRadius + "    " + SysUtils.targetSearchTag(transform));

        Target = Ship_Utils.Find_NearestEnemy(transform, ship_SearchRadius, SysUtils.targetSearchTag(transform));
        if (Target == null)
        {
            Debug.Log("Target==null" + this.transform.name);
        }
    }


    //color2------------IDamageable接口實現----------------//
    public virtual void Get_Damage(WeaponType weaponType)
    {
        bool IsCriticalHit = Random.Range(0, 100) < 30;
        DamagePopUpScript.Create(transform.position, (int)weaponType, IsCriticalHit);


        ship_Hull -= (int)weaponType;//武器類型代表傷害值
        ship_HealthBar.SetHealth(ship_Hull);


        //Debug.Log(gameObject.name  +  ship_Hull);


        if (ship_Hull <= 0)
        {
            Destroy(this.gameObject, 0.0f);
        }
    }


}
