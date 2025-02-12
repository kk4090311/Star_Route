using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarRouteSettings;

public class Turret : Base_Turret
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void Shoot()
    {
        if (FireCountDown <= 0.0f)
        {
            FireCountDown = 1.0f / turret_ScriptObjects.FireRate;
            
            GameObject Bullet = Instantiate(turret_ScriptObjects.Bullet_Prefabs, this.transform.position, this.transform.rotation);

            BulletScript bulletScript = Bullet.GetComponent<BulletScript>();
          

          
            Bullet.tag = transform.root.tag;//設定TAG 用於Turret name tag 尋找 
            Bullet.layer = SysUtils.Set_Bullet_CollideLayerByTag(transform.root.tag, turret_ScriptObjects.weaponType);//設定lay 用於避免碰撞
            Bullet.GetComponent<Rigidbody2D>().AddForce(this.transform.up * turret_ScriptObjects.BulletSpeed);

        }
    }
}