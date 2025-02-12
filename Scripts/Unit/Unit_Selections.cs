using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using Unity.Mathematics;
using DG.Tweening;

public class Unit_Selections : MonoBehaviour
{
    [Title("Unit_Selections 控制腳本")]
    [LabelText("Unit_List 所有Unit_List")] public List<GameObject> Unit_List = new List<GameObject>();
    [LabelText("Unit_Selected_List 已經被選擇的Unit")] public List<GameObject> Unit_Selected_List = new List<GameObject>();


    public static Unit_Selections instance
    {
        get { return _instance; }
        set { }
    }
    private static Unit_Selections _instance;

    private void Awake()
    {
        if (_instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && Unit_Selected_List.Count >= 1)
        {
            // 右键单击时调用 PatrolAroundMousePosition 方法
            PatrolAroundMousePosition(3f, Unit_Selected_List.Count); // 传递巡逻半径和单位数量

        }
        Debug.Log("Unit_Selected_List.Count = " + Unit_Selected_List.Count);
    }
    //Color2  -------取得移動位置陣列-------//
    private List<Vector3> GetPositionListAround(Vector3 start_Position, float distance, int pos_Count)
    {
        List<Vector3> positionList = new List<Vector3>();

        for (int i = 0; i < pos_Count; i++)
        {
            float angle = i * (360f / pos_Count);
            Vector3 dir = ApplyRotation(new Vector3(1, 0), angle);
            Vector3 pos = start_Position + dir * distance;
            positionList.Add(pos);
        }
        return positionList;
    }
    private Vector3 ApplyRotation(Vector3 vector, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vector;
    }
    public void PatrolAroundMousePosition(float distance, int unitCount)
    {
        // 获取鼠标指定点的世界坐标
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // 保证 z 坐标为 0


        // 如果单位数量等於 1，则 使用  鼠标指定点的世界坐标 作為目標點
        if (unitCount == 1)
        {

            Unit_Selected_List[0].GetComponent<Unit_Movement>().SetDestinationTarget(mousePosition);
            Unit_Visual(Unit_Selected_List[0], true);
        }
        // 如果单位数量大于 1，则 使用  圓心座標系 作為目標點
        else
        {
            // 获取指定点周围的位置列表
            List<Vector3> positions = GetPositionListAround(mousePosition, distance, unitCount);

            // 计算每个单位在圆形排列中的目标位置，并设置目标位置
            for (int i = 0; i < unitCount; i++)
            {
                if (i < positions.Count)
                {
                    if (Unit_Selected_List[i] != null)
                    {
                        Unit_Selected_List[i].GetComponent<Unit_Movement>().SetDestinationTarget(positions[i]);
                        Unit_Visual(Unit_Selected_List[i], true);
                    }

                }
            }
        }

    }



    //Color2  -------取得移動位置陣列------//



    public void Unit_ClickSelect(GameObject unitToAdd)
    {
        //*Unit 單點選取
        Unit_DeSelect_All();
        Unit_Selected_List.Add(unitToAdd);
        //*Unit視覺顯示
        Unit_Visual(unitToAdd, true);

    }
    public void Unit_Shift_ClickSelect(GameObject unitToAdd)
    {
        //*Unit 單點+Shift 增加選取
        if (!Unit_Selected_List.Contains(unitToAdd))
        {
            //*Unit 單點+Shift 不在在Unit_Select_List則選取
            Unit_Selected_List.Add(unitToAdd);

            //*Unit視覺顯示
            Unit_Visual(unitToAdd, true);


        }
        else
        {
            //*Unit 單點+Shift 已經在Unit_Select_List則取消選取

            //*Unit視覺顯示
            Unit_Visual(unitToAdd, false);

            //?移除Unit
            Unit_Selected_List.Remove(unitToAdd);
        }
    }
    public void Unit_Drag_Select(GameObject unitToAdd)
    {
        //*Unit 框選選取
        if (!Unit_Selected_List.Contains(unitToAdd))
        {
            Unit_Selected_List.Add(unitToAdd);
            //*Unit視覺顯示
            Unit_Visual(unitToAdd, true);
        }
    }

    public void Unit_DeSelect_All()
    {
        //*Unit 全部取消選取
        foreach (var unit in Unit_Selected_List)
        {
            //*Unit視覺顯示
            if (unit != null) Unit_Visual(unit, false);
            else Debug.LogWarning("Unit in Unit_Selected_List is null.");
        }
        Unit_Selected_List.Clear();
    }
    public void Unit_DeSelect(GameObject unitToAdd)
    {
        //*Unit 單個取消選取
        Unit_Selected_List.Remove(unitToAdd);
        //*Unit視覺顯示
        Unit_Visual(unitToAdd, false);

    }

    private void Unit_Visual(GameObject unit, bool flag)
    {
        if (unit.GetComponent<StarRoute_Ship>().PlayerControl == false)
        {
            //---------------啟用或關閉選取底圖---------------------//
            Transform selectionIndicator = unit.transform.Find("船艦-選取顯示");
            if (selectionIndicator != null)
            {
                // 如果 flag 為 true，則顯示路徑點，否則隱藏
                if (flag)
                {
                    selectionIndicator.gameObject.SetActive(true);

                }
                else
                {
                    selectionIndicator.gameObject.SetActive(false);
                }
            }



            //--------------啟用或關閉路徑點顯示---------------------//
            Unit_Movement unitMovement = unit.GetComponent<Unit_Movement>();
            if (unitMovement != null && unitMovement._TargetWayPoint != null)
            {
                // 如果 flag 為 true，則顯示路徑點，否則隱藏
                if (flag)
                {
                    // 顯示路徑點
                    unitMovement._TargetWayPoint.SetActive(true);

                    // 路徑點放大效果
                    unitMovement._TargetWayPoint.transform.localScale = Vector3.zero;
                    unitMovement._TargetWayPoint.transform.DOScale(new Vector3(0.8f, 0.8f, 0), 0.3f);
                }
                else
                {
                    // 隱藏路徑點
                    unitMovement._TargetWayPoint.SetActive(false);
                }
            }
        }

    }
}
