using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarRouteSettings;
using System;
using Unity.VisualScripting;

public class CheckEnemiesInCircle : MonoBehaviour
{
    public float gotoUIHUD;



    [SerializeField] private Transform[] tes1;
    public float Circleradius = 10f;

    Vector3 worldMousePos;
    private Vector3 pointOnCircle;

    // CheckEnemiesInCircle use Physics2D.CircleCast
    // to detect enemies in a circle around the player
    float Ship_Angle//以X軸起算Ship的Angle
    {
        get => (transform.root.eulerAngles.z + 90) % 360;
        set
        {
            value -= 90;
            while (value < 0) value += 360;//負角度設為0~360
            transform.root.eulerAngles = new Vector3(transform.root.eulerAngles.x, transform.root.eulerAngles.y, value);
        }
    }
    private void Update()
    {
        //DrawLinesToEnemiesInCircle1(transform.position, Circleradius);

        //ship_Utils.Find_Enemies_In_Range(transform,Circleradius);

        //tes1 = ship_Utils.Find_Enemies_In_Range(transform, Circleradius);




        //!畫切線圓
        worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = SysUtils.Get_Angle(transform.position, worldMousePos);
        //Debug.Log("船艦現在角度"+ship_Utils.Get_Ship_Angle(transform));
        //ship_Utils.Set_Ship_Angle(transform, angle);
        float x = transform.position.x + Circleradius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = transform.position.y + Circleradius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pointOnCircle = new Vector3(x, y, transform.position.z);
    }


    private void OnDrawGizmos()
    {
        //画出射程范围

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 10f);


        Vector3 boxCenter = transform.position + transform.up * Circleradius;
        Vector3 topLeft2 = transform.position + transform.up * Circleradius + new Vector3(-2, 2, 0);
        Vector3 topRight2 = transform.position + transform.up * Circleradius + new Vector3(2, 2, 0);
        Vector3 bottomLeft2 = transform.position + transform.up * Circleradius + new Vector3(-2, -2, 0);
        Vector3 bottomRight2 = transform.position + transform.up * Circleradius + new Vector3(2, -2, 0);
        //draw box
        Debug.DrawLine(transform.position, boxCenter, Color.red);
        Debug.DrawLine(topLeft2, topRight2, Color.red);
        Debug.DrawLine(topRight2, bottomRight2, Color.green);
        Debug.DrawLine(bottomRight2, bottomLeft2, Color.green);
        Debug.DrawLine(bottomLeft2, topLeft2, Color.green);
        // mouse pos

        Gizmos.color = Color.red;
        //float angle = Mathf.Atan2(worldMousePos.y - transform.position.y, worldMousePos.x - transform.position.x);

        //SysUtils.Set_Ship_Angle(transform, angle);
        // 计算鼠标位置在圆周上的坐标


        Gizmos.DrawWireSphere(pointOnCircle, 1f);
        // Gizmos.DrawWireSphere(pointOnCircle, 1f);


    }


}
