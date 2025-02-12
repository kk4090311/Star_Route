using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarRouteSettings
{
    public enum DamageValue { Easy = 1, Medium = 3, Hard = 5, Tough = 10 };
    public enum WeaponType { Laser = 1, Bullet = 2, Missile = 5, Blaster = 10 };
    public enum ShipType { 護衛艦, 驅逐艦, 巡洋艦, 主力艦, 貨船 };

    //public enum SlotType { 護衛艦, 驅逐艦, 巡洋艦, 主力艦, 貨船 };

    public enum CollideLayer_Name { Player_Layer = 1, Enemy_Layer = 2, Player_BulletCollide_Layer = 3, Enemy_BulletCollide_Layer = 4 };
    public static class StarRouteGameSettings
    {
        public static string StarRoute_Maps_Play_ScenceName => "StarRoute_Maps_Play";
        public static string StarRoute_Game_Play_ScenceName => "StarRoute_Game_Play";
        // 遊戲參數
        // ES3 參數
        public static string Player_Ship_List_Key => "Player_Ship_List_Key"; // 代表玩家船艦列表的 ES3 Key 
        public static string Player_Ship_List_KeyFiles => "Player_Ship_List_Key.es3"; // 代表玩家分數的 ES3 Key

    }
}