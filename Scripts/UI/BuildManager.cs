using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{


    private GameObject TurretToBuild;

    private Turret_Node turret_Node;



    public UI_NodeSelect ui_NodeSelect;
    public static BuildManager instance;//static BuildManager  instance  方法
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than oneBuildingTurret BuildManager in Scene");
            return;
            
        }
        instance = this;
    }


    public GameObject GetTurretToBuild()
    {
        return TurretToBuild;

    }
    public void SetNode_NodeBuild(Turret_Node node)
    {
        if (turret_Node == node)
        {
       
            return;
        }
        turret_Node = node;
        TurretToBuild = null;
        ui_NodeSelect.UI_position(node);

    }

    public void SetTurret_TurretToBuild(GameObject turret)
    {
        TurretToBuild = turret;
        
    }
}
