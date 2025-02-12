using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_TurretBuild : MonoBehaviour
{
    private List<GameObject> Shiplist;
    public bool AutoAttack = false;
    BuildManager buildManager;
    private void Start()
    {

        buildManager = BuildManager.instance;
    }
    public void Build_Turret_Prefabs_index_0()//TurretPrefab1
    {

        buildManager.SetTurret_TurretToBuild(All_Prefab_Table.i.Turret[0]);
        Debug.Log(All_Prefab_Table.i.Turret[0].name);

    }
    public void Build_Turret_Prefabs_index_1()//TurretPrefab1
    {

        buildManager.SetTurret_TurretToBuild(All_Prefab_Table.i.Turret[1]);
        Debug.Log(All_Prefab_Table.i.Turret[1].name);

    }
    public void Build_Turret_Prefabs_index_2()//TurretPrefab1
    {

        buildManager.SetTurret_TurretToBuild(All_Prefab_Table.i.Turret[2]);
        Debug.Log(All_Prefab_Table.i.Turret[2].name);

    }
    public void Build_Turret_Prefabs_index_3()//TurretPrefab1
    {

        buildManager.SetTurret_TurretToBuild(All_Prefab_Table.i.Turret[3]);
        Debug.Log(All_Prefab_Table.i.Turret[3].name);

    }

    public void OnClick()
    {
        if (AutoAttack)
        {
            AutoAttack = false;
            print("AutoAttack=true");
        }
        else
        {
            AutoAttack = true;
            print("AutoAttack=false");
        }

    }

    public void SavePrefabInstances()
    {
        List<GameObject> Player_List = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        Debug.Log("Player_List_Saved");
        // Save our prefabInstances list to fi  le using "prefabInstances" as the unique key to idetify the data.
        //ES3.Save("Player_List", Player_List);
        ES3.Save("Player_List_KEY", Player_List, "Player_List.es3");
    }
    public void LoadPrefabInstances()
    {
        Debug.Log("Player_List_Loaded");
        Shiplist = ES3.Load("Player_List", new List<GameObject>());
        foreach (GameObject ship in Shiplist)
        {
            Debug.Log(ship.name);
        }

    }




    public void LoadSceneBack()
    {
        SceneManager.LoadSceneAsync("StarRoute_Maps_Play");
    }
}