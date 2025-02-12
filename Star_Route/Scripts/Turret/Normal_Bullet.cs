using System.Collections;
using System.Collections.Generic;
using Redcode.Pools;
using UnityEngine;
using StarRouteSettings;



public class Normal_Bullet : Base_Bullet, IPoolObject
{
    private int Hit_Count = 0;
    protected void Update()
    {
        transform.position += transform.up * 10f * Time.deltaTime;
        Debug.Log(this.name + "Bullet_PoolName: " + Bullet_PoolName);
    }

    public override void Get_Damage(WeaponType type)
    {
        if (type == WeaponType.Laser)
        {
            Hit_Count++;
            if (Hit_Count >= 2)
            {
                Bullet_TakeToPool_ByName(Bullet_PoolName);
                GameObject effcet = Instantiate(Hiteffcet, transform.position, Quaternion.identity);
                Destroy(effcet, 0.5f);
                Hit_Count = 0;
            }
        }
        return;
    }
    
    protected override void OnCollisionEnter2D(Collision2D Collision)
    {
        IDamageable iDamageable = Collision.gameObject.GetComponent<IDamageable>();
        if (iDamageable != null)
        {
            Bullet_TakeToPool_ByName(Bullet_PoolName);
            GameObject effcet = Instantiate(Hiteffcet, transform.position, Quaternion.identity);
            Destroy(effcet, 0.5f);
            //DamageOnHit = Random.Range(100, 300);

            iDamageable.Get_Damage(weaponType);//觸發被命中之 iDamageable.Get_Damage 的功能



        }
    }
}
