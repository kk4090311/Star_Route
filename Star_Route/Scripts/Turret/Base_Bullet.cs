using System.Collections;
using System.Collections.Generic;
using StarRouteSettings;
using UnityEngine;
using Redcode.Pools;
using Sirenix.OdinInspector;

public class Base_Bullet : MonoBehaviour, IDamageable, IPoolObject
{

    //color1------------Public----------------//
    [Title("Base_Bullet 參數")]
    //////////////////////////////
    //weaponType 與 Hiteffcet 來自TurretScriptobjcet的參數
    [LabelText("Base_Bullet 武器種類")] public WeaponType weaponType;// 武器種類
    [LabelText("Base_Bullet 命中特效")] public GameObject Hiteffcet;// 命中特效
    [LabelText("Base_Bullet 存滅時間")] public float Bullet_LifeTime = 3f; // 存滅時間

    [LabelText("Base_Bullet 鎖定目標")] public Transform target; // 鎖定目標
    [LabelText("Bullet_PoolName 子彈該去的物件池名稱")] public string Bullet_PoolName; //子彈該去的物件池名稱
    //////////////////////////////


    //color1------------Protected----------------//
    //////////////////////////////
    //子類繼承後設定Bullet_PoolName後返回指定的Pool
    protected string NameTag;
    protected TrailRenderer trailRenderer;
    //protected int hp = 5;
    //////////////////////////////


    //!  IPoolObject Interface
    void IPoolObject.OnCreatedInPool()
    {
        //Color2 初始化生成時
        Debug.Log("OnCreatedInPool");
    }
    void IPoolObject.OnGettingFromPool()
    {
        //Color2 從Pool中抓取時 ， 用於 Reset 設定
        StartCoroutine(ReturnToPoolAfterDelay(Bullet_LifeTime));
    }
    private IEnumerator ReturnToPoolAfterDelay(float delay)
    {
        //Color2    等待秒數後 放回指定物件池
        yield return new WaitForSeconds(delay);
        //Debug.Log(Bullet_PoolName);//log 子彈該去的物件池名稱
        Bullet_TakeToPool_ByName(Bullet_PoolName);
    }
    protected virtual void Bullet_TakeToPool_ByName(string name)
    {
        PoolManager.Instance.TakeToPool<Base_Bullet>(name, this);
        trailRenderer.Clear();
    }
    //!  IPoolObject Interface








    //color1----------------實作功能--------------------//
    //////////////////////////////
    protected virtual void Awake()
    {
        trailRenderer = this.gameObject.GetComponent<TrailRenderer>();
    }
    /// <summary>
    /// 可以設定被敵人打中時 e.g 血量，可被破壞
    /// </summary>
    public virtual void Get_Damage(WeaponType type)
    {

    }
    ///  <summary>
    ///  可以設定命中時 e.g 傷害方式等
    ///  </summary>
    protected virtual void OnCollisionEnter2D(Collision2D Collision)
    {
        IDamageable iDamageable = Collision.gameObject.GetComponent<IDamageable>();
        if (iDamageable != null)
        {
            Bullet_TakeToPool_ByName(Bullet_PoolName);
            GameObject effcet = Instantiate(Hiteffcet, transform.position, Quaternion.identity);
            //DamageOnHit = Random.Range(100, 300);

            iDamageable.Get_Damage(weaponType);//觸發被命中之 iDamageable.Get_Damage 的功能


            Destroy(effcet, 1.0f);
        }
    }
    //////////////////////////////
    //color1----------------實作功能--------------------//
}