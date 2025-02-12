
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public static class Ship_Utils
{

    static Transform Ship_transform;
    static Vector2 oldPosition;

    // protected GameObject[] enemyTransforms;

    //Color3  -------船艦功能-------//

    //Color1 搜索所有敵人陣列

    public static List<GameObject> Find_All_Enemies(string tag)
    {
        //* 尋照所有特定tag的GameObject : e.g Player,Enemy Tag
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag(tag);
        if (allEnemies.Length > 0)
        {

            return allEnemies.ToList();
        }
        else
        {
            Debug.Log($"No enemies found in Tag={tag}");
            return null;
        }
    }
    //Color1 搜索範圍內敵人陣列
    public static List<GameObject> Find_Tag_GameObject_InRange(Transform transform, float searchRadius, string tag)
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag(tag);
        List<GameObject> enemiesInRange = new List<GameObject>();

        foreach (GameObject enemy in allEnemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance <= searchRadius)
            {
                Debug.DrawLine(transform.position, enemy.transform.position, Color.red);
                enemiesInRange.Add(enemy); // 添加到範圍內的敵人列表
            }
        }
        if (enemiesInRange.Count > 0)
        {
            // 將 List<GameObject> 轉換為 GameObject[]
            return enemiesInRange;
        }
        else
        {
            //Debug.Log($"No enemies found in range.Tag={tag}");
            return null;
        }
    }

    //Color1 搜索範圍最近敵人單一 
    public static GameObject Find_NearestEnemy(Transform transform, float searchRadius, string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        if (gameObjects != null)
        {
            GameObject nearestEnemy = null;
            float nearestDistance = Mathf.Infinity;
            foreach (var item in gameObjects)
            {
                float distance = Vector2.Distance(transform.position, item.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = item;
                }
            }
            return nearestEnemy;
        }
        Debug.LogWarning("NearestEnemyIn_Range == null");
        return null;




        // GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        // GameObject nearestEnemy = null;
        // float nearestDistance = Mathf.Infinity;
        // if (enemies != null)
        // {
        //     foreach (GameObject enemy in enemies)
        //     {
        //         float distance = Vector2.Distance(transform.position, enemy.transform.position);

        //         if (distance <= searchRadius && distance < nearestDistance)
        //         {
        //             nearestDistance = distance;
        //             nearestEnemy = enemy;

        //         }
        //     }
        //     //Debug.DrawLine(transform.position, nearestEnemy.transform.position, Color.red);
        //     Debug.Log("Found Nearest Enemy: " + nearestEnemy.gameObject.name);
        //     
        // }
        // else
        // {
        //     Debug.Log($"No enemies found in range.Tag={tag}");
        //     return null;
        // }
    }
    //!! 需要修改
    //Color3  -------船艦功能-------//



    //Color2  -------船艦基本數值-------GET、Set角度 SPEED、ANGLE//
    public static float Get_Ship_Speed(Transform transform)//以X軸起算Ship的Angle
    {
        float Speed = Vector2.Distance(oldPosition, transform.position) * 100;
        oldPosition = transform.position;
        //計算transform.position 與 oldPosition Vector2 之間的距離
        //輸出  Debug.Log("Speed: " + speed.ToString("F2"));
        return Speed;
    }


    public static float Get_Ship_Angle(Transform transform)//以X軸起算Ship的Angle
    {
        float angle = transform.eulerAngles.z % 360;
        return angle;
    }

    public static void Set_Ship_Angle(Transform transform, float value)
    {
        value -= 90;
        while (value < 0) value += 360;//負角度設為0~360
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, value);
        transform.rotation.Normalize();
    }


    public static List<GameObject> Get_Ship_Turret(Transform transform)
    {
        string tag = "Turret";
        if (transform.childCount > 0)
        {
            List<GameObject> TurretSlots = new List<GameObject>();
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).CompareTag(tag))
                {
                    TurretSlots.Add(transform.GetChild(i).gameObject);
                }
            }
            return TurretSlots;
        }
        else return null;

    }
}