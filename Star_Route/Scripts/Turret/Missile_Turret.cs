using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;

public class Missile_Turret : Base_Turret
{
    protected override void Turret_Rotation()
    {
        if (TargetTransform != null)
        {
            Shoot();
        }
        return;
    }
    protected override void Shoot()
    {
        if (FireCountDown <= 0.0f)
        {
            FireCountDown = 1.0f / turret_ScriptObjects.FireRate;
            var clone = PoolManager.Instance.GetFromPool<Base_Bullet>(turret_ScriptObjects.Bullet_Prefabs.name);
            Debug.Log(turret_ScriptObjects.Bullet_Prefabs.name);
            if (clone != null)
            {
                // Instantiate(clone.gameObject);
                //color2 設定子彈物件池 、 子彈位置旋轉 、 Tag 、 Layer 、 BulletSpeed
                clone.Bullet_PoolName = turret_ScriptObjects.Bullet_Prefabs.name;
                clone.transform.position = transform.position;
                clone.transform.rotation = transform.rotation;


                // 4 .17 update
                clone.target = TargetTransform;

                //color2 設定TAG 用於判斷敵人的Turret name tag 尋找 、設定lay 用於避免碰撞
                clone.tag = transform.root.tag + "_Missile_Bullet"; // 設定為Player 或 Enemy  _Missile_Bullet Tag;
                clone.gameObject.layer = SysUtils.Set_Bullet_CollideLayerByTag(transform.root.tag, turret_ScriptObjects.weaponType);
                //clone.GetComponent<Rigidbody2D>().AddForce(this.transform.up * turret_ScriptObjects.BulletSpeed, ForceMode2D.Force);

            }
        }
    }



}
