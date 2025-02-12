using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarRouteSettings;
using Redcode.Pools;

public class BulletScript : Base_Bullet, IPoolObject
{
    protected override void OnCollisionEnter2D(Collision2D Collision) //傳入碰撞對象 取名Collision
    {
        //color2 是否具有 IDamageable Interface  有則 使用IDamageable Interface Damage(weaponType) 造成傷害
        

    }
}
