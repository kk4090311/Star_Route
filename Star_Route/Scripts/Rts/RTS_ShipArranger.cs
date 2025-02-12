using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTS_ShipArranger : MonoBehaviour
{
    // 布阵时的起始位置

    // 两艘船之间的间隔
    public static float shipSpacing = 2f;

    public static void ArrangeShips(List<GameObject> ships, Vector3 SetPosition = default)
    {
        foreach (var shipPrefab in ships)
        {
            GameObject newShip = Instantiate(shipPrefab);
            newShip.SetActive(true);
            SetActiveInChildren(newShip.transform, "船艦-攝像機", false);
            SetActiveInChildren(newShip.transform, "砲塔-攝像機", false);
            ArrangeShip(newShip.transform, SetPosition);
            if (newShip.CompareTag(SysUtils.Player_Tag))
            {
                GameObject.Find("StarRouteGamePlay-Manager").GetComponent<StarRouteGamePlay_Manager>().Local_Ship_Instance_List.Add(newShip);

            }
            if (newShip.CompareTag(SysUtils.Enemy_Tag))
            {
                GameObject.Find("StarRouteGamePlay-Manager").GetComponent<StarRouteGamePlay_Manager>().Local_Enemy_Instance_Ship_List.Add(newShip);

            }
            SetPosition += Vector3.right * shipSpacing; // 设置下一个船的起始位置
        }
    }


    // 设置船只的位置
    private static void ArrangeShip(Transform ship, Vector3 position)
    {
        ship.position = position;
    }
    private static void SetActiveInChildren(Transform parent, string childName, bool active)
    {
        Transform child = parent.Find(childName);
        if (child != null)
        {
            child.gameObject.SetActive(active);
        }
    }
}
