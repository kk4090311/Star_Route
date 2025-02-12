using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using StarRouteSettings;
using UnityEngine;

public class StarRoute_Enemy : MonoBehaviour, IDamageable
{
    [LabelText("敵人數據放置ScriptObjects")] public Enemy_ScriptableObject enemy_ScriptableObject;


    // Start is called before the first frame update
    void Start()
    {
        if (enemy_ScriptableObject != null)
        {
            Enemy_Init();  //初始化設定
        }
        else
        {
            Debug.LogWarning("Enemy_ScriptableObject not assigned!" + transform.name);
        }
    }

    void Enemy_Init()
    {
        // 生成 UI_Enemy_Detected_Circle  子物件
        //UI_Enemy_Detected_Circle_Instantiate();

        // 生成 UI_Enemy_Indicator  子物件
        // UI_Enemy_Indicator_Instantiate();
    }

    // private void UI_Enemy_Detected_Circle_Instantiate()
    // {
    //     Transform UI_Enemy_Detected_Circle = transform.Find("UI_Enemy_Detected_Circle");
    //     if (UI_Enemy_Detected_Circle == null)
    //     {
    //         // 如果未找到，生成一个新的对象并设置为当前对象的子对象
    //         GameObject gameObject = Instantiate(enemy_ScriptableObject.UI_Enemy_Detected_Circle, transform.position, Quaternion.identity);
    //         gameObject.transform.SetParent(transform); // 设置为当前对象的子对象
    //         gameObject.name = "UI_Enemy_Detected_Circle"; // 设置生成对象的名称
    //     }
    //     else
    //     {
    //         return;
    //     }
    // }
    // private void UI_Enemy_Indicator_Instantiate()
    // {
    //     Transform UI_Enemy_Indicator = transform.Find("UI_Enemy_Indicator");
    //     if (UI_Enemy_Indicator == null)
    //     {
    //         // 如果未找到，生成一个新的对象并设置为当前对象的子对象
    //         GameObject gameObject = Instantiate(enemy_ScriptableObject.UI_Enemy_Indicator, transform.position, Quaternion.identity);
    //         gameObject.transform.SetParent(transform); // 设置为当前对象的子对象
    //         gameObject.name = "UI_Enemy_Indicator"; // 设置生成对象的名称
    //     }
    //     else
    //     {
    //         return;
    //     }
    // }

    public void Get_Damage(WeaponType weaponType)
    {

    }
}
