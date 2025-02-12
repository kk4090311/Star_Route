
using System.Text;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using StarRouteSettings;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;


public class UI_HUD : MonoBehaviour
{



    //color2------------UI_HUD_Transform底下子物件中各TMP --------------//


    [Title("右側UI")]

    public TextMeshProUGUI UI_HUD_Speed_TMP;

    public TextMeshProUGUI UI_HUD_Angle_TMP;


    [Title("左側UI")]

    public TextMeshProUGUI UI_HUD_Distance_TMP;

    public TextMeshProUGUI UI_Enemy_Distance_InRange_TMP;

    [Title("左側UI")]

    public TextMeshProUGUI UI_HUD_Resource_TMP;

    public TextMeshProUGUI UI_HUD_Crew_TMP;



    //color1------------UI_HUD_Variable 變數----------------//

    [Title("UI_HUD_Variable 變數")]
    [LabelText("玩家物件")] public GameObject Player;
    [LabelText("玩家物件基本數據腳本")] public Player_ScriptObjects player_ScriptObjects;
    [LabelText("目標地點指示器")] public List<GameObject> _Destination;
    [LabelText("敵人標籤字串")] protected string enemy_tag = "Enemy";

    //TMP 用字串
    protected StringBuilder stringBuilder = new StringBuilder();



    //color2------------UI_HUD_Variable 敵人列表----------------//
    [LabelText("在探測範圍內的敵人陣列")] public List<GameObject> Enemy_Array_InRange;
    [LabelText("在探測範圍內的敵人陣列")] public List<GameObject> All_Enemy_Array;
    [LabelText("探測範圍指針顯示")] public List<Transform> UI_Enemy_Indicator;
    [LabelText("最近敵人")] public GameObject Nearest_Enemy;

    protected virtual void Start()
    {



        //StarRoute_Maps_Play 遊戲場景
        Player = SysUtils.Get_Player_GameObject();
        player_ScriptObjects = StarRouteGameManager.Instance.GetPlayer_ScriptObjects();

        _Destination = GameObject.FindGameObjectsWithTag("DestinationTag").ToList();


    }


    protected virtual void UI_Enemy_Indicator_Display(List<GameObject> enemy_array)
    {
        foreach (var enemy in enemy_array)
        {
            Transform indicator = enemy.transform.Find("UI_Enemy_Indicator");
            if (indicator != null && !UI_Enemy_Indicator.Contains(indicator))
            {
                UI_Enemy_Indicator.Add(indicator);
            }
            if (UI_Enemy_Indicator.Contains(indicator))
            {
                //* 玩家到敵人的向量
                Vector3 playerToEnemyVector = enemy.transform.position - Player.transform.position;
                playerToEnemyVector.z = 0f; // 將 z 軸設置為 0，因為我們只關心平面上的角度
                playerToEnemyVector.Normalize(); // 正規化向量

                //* 敵人在探測圓上的位置
                Vector3 enemyOnCirclePosition = Player.transform.position + playerToEnemyVector * player_ScriptObjects.Player_SearchRadius;

                Debug.DrawLine(Player.transform.position, enemyOnCirclePosition, Color.magenta);

                // 計算指示器需要旋轉的角度
                float angle = Mathf.Atan2(playerToEnemyVector.y, playerToEnemyVector.x) * Mathf.Rad2Deg;
                indicator.position = enemyOnCirclePosition;
                indicator.eulerAngles = new Vector3(0f, 0f, angle - 90f); // 將指示器旋轉到正確的角度
            }
        }
    }


    //*UI_HUD_Speed_TMP速度顯示
    protected virtual void UI_HUD_Speed_Display()
    {
        stringBuilder.Clear();
        int speed = (int)Ship_Utils.Get_Ship_Speed(Player.transform);
        stringBuilder.Append($"Speed: {speed} km/s");
        //*修改UI_TMP_Text
        UI_HUD_Speed_TMP.text = stringBuilder.ToString();

    }

    //*UI_HUD_Angle_TMP角度顯示
    protected virtual void UI_HUD_Angle_Display()
    {
        stringBuilder.Clear();
        int angle = (int)Ship_Utils.Get_Ship_Angle(Player.transform);
        stringBuilder.Append($"Angle: {angle}°");
        //*修改UI_TMP_Text
        UI_HUD_Angle_TMP.text = stringBuilder.ToString();
    }

    //*UI_HUD_Distance_TMP距離顯示
    protected virtual void UI_HUD_Distance_Display(List<GameObject> gameObjects)
    {

        stringBuilder.Clear();
        if (gameObjects != null)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                //color2-----------UI_HUD_Distance_Display敵人陣列顯示顯示代碼----------------//
                float distance = Vector2.Distance(Player.transform.position, gameObject.transform.position);
                //Debug.Log($"Enemy Name: {_destination.name}, Distance: {distance:F2}\n");
                stringBuilder.AppendLine($"Enemy Name: {gameObject.name}, Distance: {distance:F2}");
                //color2-----------UI_HUD_Distance_DisplayP敵人陣列顯示顯示代碼----------------//
            }
        }
        //*修改UI_TMP_Text
        UI_HUD_Distance_TMP.text = stringBuilder.ToString();
    }

    protected virtual void UI_HUD_Resource_Display()
    {
        int resource_count = (int)player_ScriptObjects.Player_Resource;
        int crew_count = (int)player_ScriptObjects.Player_Crews;
        //*修改UI_TMP_Text
        UI_HUD_Resource_TMP.text = $"礦物數量: {resource_count}";
        UI_HUD_Crew_TMP.text = $"採礦人員: {crew_count}";

    }


    //*UI_HUD_Enemy_Distance_TMP敵人陣列顯示
    protected virtual void UI_Enemy_Distance_Display(List<GameObject> enemy_array)
    {
        stringBuilder.Clear();
        if (enemy_array != null)
        {
            foreach (GameObject enemy in enemy_array)
            {
                //color2-----------UI_HUD_Enemy_Distance_TMP敵人陣列顯示顯示代碼----------------//
                float distance = Vector2.Distance(Player.transform.position, enemy.transform.position);
                //Debug.Log($"Enemy Name: {enemy.name}, Distance: {distance:F2}\n");
                stringBuilder.AppendLine($"Enemy Name: {enemy.name}, Distance: {distance:F2}");
                //color2------------UI_HUD_Enemy_Distance_TMP敵人陣列顯示顯示代碼----------------//

                //color1------------調用UI_HUD_Enemy_Detected_Circle_Display 功能----------------//

                if (distance < player_ScriptObjects.Player_SearchRadius)
                {
                    //UI_HUD_Enemy_Detected_Circle_Display(enemy);
                }
            }
            //*修改UI_Enemy_Distance_Display_TMP_Text
            UI_Enemy_Distance_InRange_TMP.text = stringBuilder.ToString();
        }
        else //*enemy_array 為空值時，將 UI_Enemy_Distance_TMP 清空
        {
            UI_Enemy_Distance_InRange_TMP.text = "No Enemy in Range";
        }
    }
    private void Update()
    {

        UI_Enemy_Indicator_Display(All_Enemy_Array); //*敵人指示器顯示
    }

    protected virtual void FixedUpdate()
    {
        //*在 Player_SearchRadius 內找到的敵人陣列  //可以切換為範圍或全部
        Enemy_Array_InRange = Ship_Utils.Find_Tag_GameObject_InRange(Player.transform, player_ScriptObjects.Player_SearchRadius, enemy_tag);
        //*所有敵人陣列
        All_Enemy_Array = Ship_Utils.Find_All_Enemies(enemy_tag);
        //!UI_HUD顯示
        UI_HUD_Speed_Display(); //*速度顯示
        UI_HUD_Angle_Display(); //*角度顯示
        UI_HUD_Resource_Display(); //*資源顯示  
                                   //!UI_HUD顯示
        UI_Enemy_Distance_Display(Enemy_Array_InRange); //*敵人距離顯示


        UI_HUD_Distance_Display(_Destination);//*任何GameObject 距離顯示
    }

}
