using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarRouteSettings;
using System.Text;
using Sirenix.OdinInspector;
public class UI_HUD_GamePlay : UI_HUD
{
    [Title("UI_HUD_GamePlay")]
    [LabelText("子物件-基本數船艦數據")] public Ship_ScriptObjects ship_ScriptObjects;


    protected override void Start()
    {

    }
    public void Set_RtsPlayer(GameObject player)
    {
        this.gameObject.SetActive(true);

        Player = player;
        if (Player != null)
        {
            ship_ScriptObjects = player.GetComponent<StarRoute_BaseShip>().ship_ScriptObjects;
        }


    }
    protected override void FixedUpdate()
    {
        if (Player == null)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            //!UI_HUD顯示
            UI_HUD_Speed_Display(); //*速度顯示
            UI_HUD_Angle_Display(); //*角度顯示
            UI_HUD_Distance_Display(All_Enemy_Array);//*任何GameObject 距離顯示
            UI_Enemy_Distance_Display(Enemy_Array_InRange); //*敵人距離顯示
            UI_Enemy_Indicator_Display(All_Enemy_Array);
            //!UI_HUD顯示
        }
    }


    protected override void UI_HUD_Speed_Display()
    {
        stringBuilder.Clear();
        int speed = (int)Ship_Utils.Get_Ship_Speed(Player.transform);
        stringBuilder.Append($"Speed: {speed} km/s");
        //*修改UI_TMP_Text
        UI_HUD_Speed_TMP.text = stringBuilder.ToString();
    }
    protected override void UI_HUD_Angle_Display()
    {
        stringBuilder.Clear();
        int angle = (int)Ship_Utils.Get_Ship_Angle(Player.transform);
        stringBuilder.Append($"Angle: {angle}°");
        //*修改UI_TMP_Text
        UI_HUD_Angle_TMP.text = stringBuilder.ToString();
    }

    protected override void UI_Enemy_Distance_Display(List<GameObject> enemy_array)
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


            }
            //*修改UI_Enemy_Distance_Display_TMP_Text
            UI_Enemy_Distance_InRange_TMP.text = stringBuilder.ToString();
        }
        else //*enemy_array 為空值時，將 UI_Enemy_Distance_TMP 清空
        {
            UI_Enemy_Distance_InRange_TMP.text = "No Enemy in Range";
        }
    }


}
