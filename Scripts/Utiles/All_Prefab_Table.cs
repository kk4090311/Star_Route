using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class All_Prefab_Table : MonoBehaviour
{
    private static All_Prefab_Table _i;
    public static All_Prefab_Table i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<All_Prefab_Table>("Prefabs/MISC/All_Prefab_Table"));
            return _i;
        }
    }
    [Title("攻擊數字顯示")]
    //------------------攻擊數字顯示--------------------//
    public Transform DamagePopUp;
    //-----------------------------------------------//
    [Title("砲台Prefabs")]
    public GameObject[] Turret;

    //------------------Turret Prefabs--------------------//

    public Turret_ScriptObjects[] Turret_Prefabs;

    //-----------------------------------------------//


    [BoxGroup("UI")]
    [Title("UI-Enemy-Detected-Circle")]
    public GameObject UI_Enemy_Detected_Circle;

    

}
