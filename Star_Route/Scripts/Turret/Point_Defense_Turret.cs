using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarRouteSettings;
using Redcode.Pools;
using Sirenix.Utilities;
using System.Linq;

public class Point_Defense_Turret : Base_Turret
{

    //Color1 砲塔上所有的的發射點
    public List<Transform> FirePoint;
    //////////////////////////////////////////////////////////
    protected override List<GameObject> GetEnemiesInRange()
    {
        List<GameObject> enemies = GameObject.FindGameObjectsWithTag(Name_Target).ToList();
        List<GameObject> enemies_missile_bullet = GameObject.FindGameObjectsWithTag(Name_Target + "_Missile_Bullet").ToList();
        enemies.AddRange(enemies_missile_bullet);
        return enemies;
    }

    protected override void Shoot()
    {
        if (FireCountDown <= 0.0f)
        {
            FireCountDown = 1.0f / turret_ScriptObjects.FireRate;
            foreach (var firepont in FirePoint)
            {
                var clone = PoolManager.Instance.GetFromPool<Base_Bullet>(turret_ScriptObjects.Bullet_Prefabs.name);

                if (clone != null)
                {
                    // Instantiate(clone.gameObject);
                    //color2 設定子彈物件池 、 子彈位置旋轉 、 Tag 、 Layer 、 BulletSpeed
                    clone.Bullet_PoolName = turret_ScriptObjects.Bullet_Prefabs.name;
                    clone.transform.position = firepont.position;
                    clone.transform.rotation = firepont.rotation;


                    // 4 .17 update
                    clone.target = TargetTransform;

                    //color2 設定TAG 用於判斷敵人的Turret name tag 尋找 、設定lay 用於避免碰撞
                    clone.tag = transform.root.tag + "_Bullet";
                    clone.gameObject.layer = SysUtils.Set_Bullet_CollideLayerByTag(transform.root.tag, turret_ScriptObjects.weaponType);
                    //clone.GetComponent<Rigidbody2D>().AddForce(this.transform.up * turret_ScriptObjects.BulletSpeed, ForceMode2D.Force);

                }
                //GameObject Bullet = Instantiate(turret_ScriptObjects.Bullet_Prefabs, firepont.position, this.transform.rotation);
                // BulletScript bulletScript = Bullet.GetComponent<BulletScript>();

                // bulletScript.weaponType = turret_ScriptObjects.weaponType;
                // bulletScript.Hiteffcet = turret_ScriptObjects.Hiteffcet;
                // Bullet.tag = transform.root.tag;//設定TAG 用於Turret name tag 尋找 
                // Bullet.layer = SysUtils.Set_Bullet_CollideLayerByTag(transform.root.tag, turret_ScriptObjects.weaponType);//設定lay 用於避免碰撞
                // Bullet.GetComponent<Rigidbody2D>().AddForce(this.transform.up * turret_ScriptObjects.BulletSpeed);



            }

        }
    }
}
