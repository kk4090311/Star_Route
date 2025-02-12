using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Node : MonoBehaviour
{
    public Color HoverColor;
    private Color NormalColor;
    private SpriteRenderer Node;
    private GameObject Turret;
    BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.instance;
        Node = GetComponent<SpriteRenderer>();
        NormalColor = Node.color;
        // Debug.Log(NormalColor);
    }
    private void OnMouseDown()
    {

        if (Turret != null)
        {
            buildManager.SetNode_NodeBuild(this);
            //Build_Turret()已經蓋過砲塔
            Debug.Log("Cant build here");
            return;
        }

        if (buildManager.GetTurretToBuild() == null)
        {
            //buildManager.GetTurretToBuild()沒有選砲塔
            return;
        }


        Build_Turret();
    }
    //!---------------------------------------------------------------//
    private void Build_Turret()
    {
        GameObject TurretToBuild = buildManager.GetTurretToBuild();
        Turret = (GameObject)Instantiate(TurretToBuild, transform.position, transform.rotation, transform);


        
        // Turret.transform.localScale = new Vector3(
        //     //砲台物件之Scale.X/Node.Scale.x 使大小等於Node框框
        //     TurretToBuild.transform.localScale.x / this.transform.localScale.x,
        //     //砲台物件之Scale.y/Node.Scale.y 使大小等於Node框框
        //     TurretToBuild.transform.localScale.y / this.transform.localScale.y,
        //      //砲台物件之.Scale.Z預設0
        //      0f);
        // // Turret.transform.localScale = transform.localScale;

        Debug.Log(TurretToBuild.transform.localScale.x + "Turret  x");
        Debug.Log(transform.localScale.x + "node  x");
    }
    public void Sell_Turret()
    {
        //出售特效
        Destroy(Turret);
    }

    //!---------------------------------------------------------------//
    private void OnMouseEnter()
    {
        Node.color = HoverColor;

    }
    private void OnMouseExit()
    {
        Node.color = NormalColor;

    }


}