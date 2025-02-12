using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine.EventSystems;
public class Unit_Click : MonoBehaviour
{
    [Title("Unit 這是一個 Unit 點選腳本")]
    [LabelText("Player_Layer 可被點選")] public LayerMask Player_Layer;
    [LabelText("Ground_Layer 地板圖層")] public LayerMask Ground_Layer;




    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            // 检查射线是否碰撞到 UI 元素
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            bool hitUI = false;
            foreach (RaycastResult result in raycastResults)
            {
                if (result.gameObject.layer == LayerMask.NameToLayer("UI"))
                {
                    hitUI = true;
                    break;
                }
            }

            // 如果没有碰撞到 UI 元素，执行射线检测
            if (!hitUI)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D raycastHit2D = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, Player_Layer);

                if (raycastHit2D.collider)
                {
                    Debug.Log("Target Position: " + raycastHit2D.collider.gameObject.name);
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        Unit_Selections.instance.Unit_Shift_ClickSelect(raycastHit2D.collider.gameObject);
                        Debug.Log("Unit_Shift_ClickSelect" + gameObject.name);
                    }
                    else
                    {
                        Unit_Selections.instance.Unit_ClickSelect(raycastHit2D.collider.gameObject);
                        Debug.Log("Unit_ClickSelect" + raycastHit2D.collider.gameObject.name);
                    }
                }
                else
                {
                    if (!Input.GetKey(KeyCode.LeftShift))
                    {
                        Unit_Selections.instance.Unit_DeSelect_All();
                        Debug.Log("Unit_DeSelect_All");
                    }
                }
            }
        }
    }
}
