using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class StarRoute_Resource : MonoBehaviour
{
    [Title("StarRoute_Resource 礦物")]

    [LabelText("礦物正在被挖")] public bool IsMined = false;

    public bool ResourceBeingMined()
    {
        // 假設礦物上有一個名為 "IsMined" 的布爾值表示是否已經被開採
        IsMined = true;
        return IsMined; // 返回礦物是否被開採的結果
    }
}
