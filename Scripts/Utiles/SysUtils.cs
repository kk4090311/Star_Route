
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarRouteSettings;
using UnityEditor;
using System.IO;
using System.Linq;
using System;


//color1------------SysUtils 系統功能----------------//
/// <summary>
/// 工具類，包含一些用於處理系統級別功能的靜態方法。
/// </summary>
public class SysUtils
{
    //////////////////////////////
    public static ILogger Logger = new ConsoleLog();  //new FileLog("D:\\data.log"); //new ConsoleLog(); two mode
    public static string Player_Tag = "Player";
    public static string Player_Missile_Bullet_Tag = "Player_Missile_Bullet";
    public static string Enemy_Tag = "Enemy";
    public static string Enemy_Missile_Bullet_Tag = "Enemy_Missile_Bullet";
    public static string Resource_Tag = "Resource";
    //////////////////////////////


    //////////////////////////////
    //color2------------SysUtils 延遲秒數----------------//
    public static IEnumerator DelaySeconds(float time, System.Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
        Debug.Log($"[已經延遲秒數:{time}f]");
    }
    //////////////////////////////



    //Set_Bullet_CollideLayerByTag
    public static int Set_Bullet_CollideLayerByTag(string tag, WeaponType weaponType)
    {
        int layer = 0;
        if (weaponType == WeaponType.Bullet)
        {
            if (tag == "Player")
            {
                layer = LayerMask.NameToLayer("Player_BulletCollide_Layer");

            }
            else
            {
                layer = LayerMask.NameToLayer("Enemy_BulletCollide_Layer");
            }
        }
        if (weaponType == WeaponType.Laser)
        {
            if (tag == "Player")
            {
                layer = LayerMask.GetMask("Wall", "Enemy_BulletCollide_Layer", "Enemy_Layer");

            }
            else
            {
                layer = LayerMask.GetMask("Wall", "Player_BulletCollide_Layer", "Player_Layer");
            }
        }
        if (weaponType == WeaponType.Missile)
        {
            if (tag == "Player")
            {
                layer = LayerMask.NameToLayer("Player_BulletCollide_Layer");

            }
            else
            {
                layer = LayerMask.NameToLayer("Enemy_BulletCollide_Layer");
            }
        }
         if (weaponType == WeaponType.Blaster)
        {
            if (tag == "Player")
            {
                layer = LayerMask.NameToLayer("Player_BulletCollide_Layer");

            }
            else
            {
                layer = LayerMask.NameToLayer("Enemy_BulletCollide_Layer");
            }
        }

        return layer;

    }
    //color2------------SysUtils Get_Angle 計算角度----------------//
    /// <summary>
    /// 以給定的 dx 和 dy 計算角度，以 X 軸為基準，逆時針到 Y 軸 90 度，以紅色 X 軸為指向。
    /// </summary>
    /// <param name="dx">X 軸的差異</param>
    /// <param name="dy">Y 軸的差異</param>
    /// <returns>計算得到的角度</returns>
    public static float Get_Angle(float dx, float dy)//*以X軸起算逆時針到Y軸 90度   以紅色X軸為指向
    {
        float angle = Mathf.Acos(dx / Mathf.Sqrt(dx * dx + dy * dy)) * Mathf.Rad2Deg;
        if (angle >= 0)
        {
            if (dy < 0)
            {
                angle = 360 - angle;
            }
        }
        else
        {
            if (dy < 0)
            {
                angle = angle + 90;
            }

        }
        return angle;
    }
    /// <summary>
    /// 以給定的 from 和 to 向量計算角度，以 X 軸為基準，逆時針到 Y 軸 90 度，以紅色 X 軸為指向。
    /// </summary>
    /// <param name="from">起始向量</param>
    /// <param name="to">目標向量</param>
    /// <returns>計算得到的角度</returns>
    public static float Get_Angle(Vector3 from, Vector3 to)//*以X軸起算逆時針到Y軸 90度   以紅色X軸為指向
    {

        Vector3 dir = to - from;
        return Get_Angle(dir.x, dir.y);
    }
    //color2------------SysUtils Get_Position_OnCircleRadius 獲得在位置圓上的點----------------//


    /// <summary>
    /// 獲得在位置圓上的點
    /// </summary>
    /// <param name="from">起始點</param>
    /// <param name="to">結束點</param>
    /// <param name="radius">圓的半徑</param>
    /// <returns>在圓上的位置</returns>
    public static Vector2 Get_Position_OnCircleRadius(Vector3 from, Vector3 to, float radius)
    {
        // 計算角度
        float angle = SysUtils.Get_Angle(from, to);

        // 計算這個角度在圓半徑上的座標點 X Y
        float x = from.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = from.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);

        // 在圓上的位置
        Vector2 posOnCircleRadius = new Vector2(x, y);

        return posOnCircleRadius;
    }

    public static List<GameObject> FindGameObjects_WithTag(string tag)
    {
        //* 尋照所有特定tag的GameObject : e.g Player,Enemy Tag
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag(tag);
        if (allEnemies.Length > 0)
        {

            return allEnemies.ToList();
        }
        else
        {
            Debug.Log($"No GameObject found in Tag={tag}");
            return null;
        }
    }

    //color2------------SysUtils StarRoute Get_Player獲得玩家資訊----------------//
    
    /// <summary>
    /// 根据指定的 Transform 标签决定搜索目标的标签。
    /// 如果根标签为玩家，则搜索敌人的标签，否则搜索玩家的标签。
    /// </summary>
    /// <param name="transform">要检查其标签的 Transform</param>
    /// <returns>搜索目标的标签</returns>
    public static string targetSearchTag(Transform transform)
    {
        return transform.root.CompareTag(SysUtils.Player_Tag) ? SysUtils.Enemy_Tag : SysUtils.Player_Tag;
    }


    /// <summary>
    /// 取得玩家 GameObject。
    /// </summary>
    /// <returns>玩家 GameObject，若找不到則返回 null。</returns>
    public static GameObject Get_Player_GameObject()
    {
        if (GameObject.Find("Player") != null)
        {
            return GameObject.Find("Player");
        }
        Debug.LogWarning("找不到 Player Gameobject ");
        return null;
    }

    /// <summary>
    /// 取得玩家 StarRoute_Player 組件
    /// </summary>
    /// <returns>玩家 StarRoute_Player，若找不到則返回 null。</returns>
    public static Player_ScriptObjects Get_Player_ScriptObjects()
    {
        Player_ScriptObjects player_ScriptObjects;
        if (Get_Player_GameObject().TryGetComponent<StarRoute_Player>(out StarRoute_Player starRoute_Player))
        {
            player_ScriptObjects = starRoute_Player.player_ScriptObjects;
            return player_ScriptObjects;
        }
        Debug.LogWarning("找不到 StarRoute_Player 組件 ");
        return null;
    }

    /// <summary>
    /// 取得玩家 StarRoute_Player 組件
    /// </summary>
    /// <returns>玩家 StarRoute_Player，若找不到則返回 null。</returns>
    public static Enemy_ScriptableObject Get_Enemy_ScriptObjects(GameObject enemy)
    {

        if (enemy.TryGetComponent<StarRoute_Enemy>(out StarRoute_Enemy starRoute_Enemy))
        {
            Enemy_ScriptableObject enemy_ScriptableObject;
            enemy_ScriptableObject = starRoute_Enemy.enemy_ScriptableObject;
            return enemy_ScriptableObject;
        }
        Debug.LogWarning(enemy.name + "找不到 Enemy_ScriptableObject 組件 ");
        return null;
    }
    /// <summary>
    /// 取得船艦 ship_ScriptObjects 組件
    /// </summary>
    /// <returns>船艦 ship_ScriptObjects，若找不到則返回 null。</returns>
    public static Ship_ScriptObjects Get_Ship_ScriptObjects()
    {
        Ship_ScriptObjects ship_ScriptObjects;
        if (Get_Player_GameObject().TryGetComponent<StarRoute_Ship>(out StarRoute_Ship starRoute_Ship))
        {
            ship_ScriptObjects = starRoute_Ship.ship_ScriptObjects;
            return ship_ScriptObjects;
        }
        Debug.LogWarning("找不到 ship_ScriptObjects 組件 ");
        return null;
    }
    public static Ship_ScriptObjects Get_Ship_ScriptObjects(GameObject gameObject)
    {
        Ship_ScriptObjects ship_ScriptObjects;
        if (gameObject.TryGetComponent<StarRoute_Ship>(out StarRoute_Ship starRoute_Ship))
        {
            ship_ScriptObjects = starRoute_Ship.ship_ScriptObjects;
            return ship_ScriptObjects;
        }
        Debug.LogWarning("找不到 ship_ScriptObjects 組件 ");
        return null;
    }

    public static List<GameObject> ConvertToPrefabs(List<GameObject> gameObjects)
    {
       


        List<GameObject> prefabList = new List<GameObject>();

        foreach (var item in gameObjects)
        {
            string prefabPath = "Assets/Resources/Prefabs/Ship/Temp/" + item.name + ".prefab";
            // 在每次迴圈中創建一個獨立的Prefab
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(item, prefabPath);
            Debug.Log("GameObject saved as prefab: " + item.name);

            // 將Prefab添加到List中
            prefabList.Add(prefab);
        }

        // 返回保存的Prefab數組
        return prefabList;
    }

    public static GameObject ConvertToPrefab(GameObject gameObjects)
    {
        


        string prefabPath = "Assets/Resources/Prefabs/Ship/Temp/" + gameObjects.name + ".prefab";
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(gameObjects, prefabPath);
        Debug.Log("GameObject saved as prefab: " + gameObjects.name);

        // 返回保存的Prefab數組
        return prefab;
    }

    internal static void DelaySeconds(float v1, object v2)
    {
        throw new NotImplementedException();
    }
}
