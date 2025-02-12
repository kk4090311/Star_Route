using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarRouteSettings;
using Redcode.Pools;

public class TestBullet : Base_Bullet, IPoolObject
{
    public override void Get_Damage(WeaponType weaponType)
    {
        // bool IsCriticalHit = Random.Range(0, 100) < 30;
        // DamagePopUpScript.Create(transform.position, (int)weaponType, IsCriticalHit);
        // hp -= (int)weaponType;//武器類型代表傷害值
        // Debug.Log(hp);
        // if (hp <= 0)
        // {
        //     GameObject effcet = Instantiate(Hiteffcet, transform.position, Quaternion.identity);
        //     Destroy(effcet, 1.0f);
        //     Destroy(this.gameObject, 0.0f);
        // }

    }


    protected override void OnCollisionEnter2D(Collision2D Collision) //傳入碰撞對象 取名Collision
    {
        //color2 是否具有 IDamageable Interface  有則 使用IDamageable Interface Damage(weaponType) 造成傷害
        IDamageable iDamageable = Collision.gameObject.GetComponent<IDamageable>();
        if (iDamageable != null)
        {
            Bullet_TakeToPool_ByName(Bullet_PoolName);
           
            GameObject effcet = Instantiate(Hiteffcet, transform.position, Quaternion.identity);
            //DamageOnHit = Random.Range(100, 300);
            iDamageable.Get_Damage(weaponType);


            Destroy(effcet, 1.0f);

            //Destroy(Collision.gameObject);//破壞擊中的物件
        }

    }
}
