using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;
using StarRouteSettings;
using DG.Tweening;

public class Missile_Bullet : Base_Bullet, IPoolObject
{
    private int Hit_Count = 0;
    private bool isVisual = false;
    private Transform missile_Visual;
    protected override void Awake()
    {
        missile_Visual = transform.Find("missile_Visual");
        trailRenderer = GetComponent<TrailRenderer>();
    }

    protected override void Bullet_TakeToPool_ByName(string name)
    {
        PoolManager.Instance.TakeToPool<Base_Bullet>(name, this);
        trailRenderer.Clear();
        isVisual = false;
        Hit_Count = 0;
        missile_Visual.localScale = new Vector3(2f, 2f, 0f);
    }
    protected void Update()
    {
        if (target != null)
        {
            transform.up = Vector3.Slerp(transform.up, target.position - transform.position, 0.5f / Vector2.Distance(transform.position, target.position));
            transform.position += transform.up * 10f * Time.deltaTime;
        }
        else
        {
            Bullet_TakeToPool_ByName(Bullet_PoolName);
        }
    }
    public void Draw_Missile_Visual()
    {
        // if (isVisual == false)
        {
            isVisual = true;
            missile_Visual.gameObject.SetActive(true);
            missile_Visual.DOScale(new Vector3(0.5f, 0.5f, 0f), 3f);
        }
    }
    public override void Get_Damage(WeaponType type)
    {
        Hit_Count++;
        if (Hit_Count > 5)
        {
            Bullet_TakeToPool_ByName(Bullet_PoolName);
            missile_Visual.gameObject.SetActive(false);
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
            Bullet_TakeToPool_ByName(Bullet_PoolName);
            missile_Visual.gameObject.SetActive(false);
            GameObject effcet = Instantiate(Hiteffcet, transform.position, Quaternion.identity);
            Destroy(effcet, 0.5f);
            iDamageable.Get_Damage(weaponType);//觸發被命中之 iDamageable.Get_Damage 的功能

        }
    }
}
