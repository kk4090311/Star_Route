using System.Collections;
using System.Collections.Generic;
using Redcode.Pools;
using StarRouteSettings;
using UnityEngine;

public class Blaster_Bullet : Base_Bullet, IPoolObject
{
    private int Hit_Count = 0;
    protected void Update()
    {
        transform.position += transform.up * 10f * Time.deltaTime;
        Debug.Log(this.name + "Bullet_PoolName: " + Bullet_PoolName);
    }

    public override void Get_Damage(WeaponType type)
    {

        Hit_Count++;
        if (Hit_Count > 5)
        {
            Bullet_TakeToPool_ByName(Bullet_PoolName);
            GameObject effcet = Instantiate(Hiteffcet, transform.position, Quaternion.identity);
            Destroy(effcet, 0.5f);
            Hit_Count = 0;
        }
        return;
    }

    protected override void OnCollisionEnter2D(Collision2D Collision)
    {
        IDamageable iDamageable = Collision.gameObject.GetComponent<IDamageable>();
        if (iDamageable != null)
        {
            iDamageable.Get_Damage(weaponType);//觸發被命中之 iDamageable.Get_Damage 的功能
        }
    }
}
