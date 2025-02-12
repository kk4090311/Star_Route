using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using StarRouteSettings;
using UnityEngine;

public class StarRoute_EnemyShip : StarRoute_BaseShip
{
    //color1 StarRoute_BaseShip 父類
    // [Title("StarRoute_各自船艦")]
    // [LabelText("船艦數據放置ScriptObjects")] public Ship_ScriptObjects ship_ScriptObjects;
    // [LabelText("自動獲取--船艦數據-血條")] public Ship_HealthBar ship_HealthBar;
    // [Title("船艦數據")]
    // [LabelText("自動獲取--船艦名稱")] public string ship_Name;
    // [LabelText("自動獲取--船艦裝甲")] public float ship_Hull;
    // [LabelText("自動獲取--船艦護頓")] public float ship_Shield;
    // [LabelText("自動獲取--船艦護頓")] public float ship_SearchRadius;
    // [LabelText("自動獲取--船艦敵人")] public List<GameObject> ship_TargetList;

    //color1 StarRoute_EnemyShip 子類

    [Title("StarRoute_EnemyShip 子類")]
    protected override void Start()
    {
        base.Start();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        StarRoute_Ship_TargetInRange();
    }
    private void StarRoute_Ship_TargetInRange()
    {
        List<GameObject> enemies = Ship_Utils.Find_Tag_GameObject_InRange(this.transform, ship_SearchRadius, SysUtils.Player_Tag);
        List<GameObject> enemies_missileBullets = Ship_Utils.Find_Tag_GameObject_InRange(this.transform, 15f, SysUtils.Player_Missile_Bullet_Tag);
        if (enemies != null)
        {
            ship_TargetList = enemies.ToList();
        }
        if (enemies_missileBullets != null)
        {
            foreach (var missile in enemies_missileBullets)
            {
                missile.GetComponent<Missile_Bullet>().Draw_Missile_Visual();
            }
        }

    }
}
