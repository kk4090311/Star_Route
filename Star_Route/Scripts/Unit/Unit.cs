using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class Unit : MonoBehaviour
{
    [Title("Unit 這是一個 Unit 單位")]
    // Start is called before the first frame update

    private void Start()
    {
        Unit_Selections.instance.Unit_List.Add(this.gameObject);
    }
    private void OnDestroy()
    {





        Unit_Selections.instance.Unit_List.Remove(this.gameObject);


        if (Unit_Selections.instance.Unit_Selected_List.Contains(this.gameObject))
        {
            Unit_Selections.instance.Unit_Selected_List.Remove(this.gameObject);
        }

        Destroy(transform.GetComponent<Unit_Movement>()._TargetWayPoint);
    
        if (GameObject.Find("UI-RTS-MainUI"))
        {
            GameObject.Find("UI-RTS-MainUI").TryGetComponent<UI_RTS_Interface>(out UI_RTS_Interface rts_interface);
            rts_interface.Initialize_RTS_Interface();
        }

    }


}
