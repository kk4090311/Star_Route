using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class UI_ShipBuild_Interface : MonoBehaviour
{
    // Start is called before the first frame update
    [Title("UI-裝備介面-下側介面-裝備選單")]
    [LabelText("UI-裝備介面-參考")] public UI_Ship_Interface UI_Ship_Interface;

    [LabelText("UI-裝備介面-下側介面-裝備選單-BuildItemBox")] public int BuildItemBox;

    [LabelText("UI-裝備介面-下側介面-裝備選單-WeaponItemBox")] public int WeaponItemBox;


    [LabelText("UI-裝備介面-下側介面-裝備選單-選中裝備 Weapon")] public GameObject Weapon;
    [LabelText("UI-裝備介面-下側介面-裝備選單-選中裝備格 WeaponSlot")] public GameObject WeaponSlot;



    [LabelText("UI-裝備介面-下側介面-所有裝備Prefab WeaponSlot")] public List<GameObject> WeaponPrefabs;

    private void Start()
    {
        UI_Ship_Interface = GameObject.Find("UI-裝備介面").GetComponent<UI_Ship_Interface>();



    }
    public void WeaponItemBoxOnClick(int index)
    {
        if (index == 4)
        {
            WeaponSlot_Delect();
            return;
        }

        WeaponItemBox = index;
        Weapon = WeaponPrefabs[index];


        if (WeaponSlot != null)
        {
            GameObject WeaponToBuild = Instantiate(Weapon, WeaponSlot.transform);
        }


    }


    public void WeaponSlot_Delect()
    {
        if (WeaponSlot != null)
        {
            Destroy(WeaponSlot.transform.GetChild(1).gameObject);
        }
    }
    public void BuildItemBoxOnClick(int index)
    {
        BuildItemBox = index;
        WeaponSlot = UI_Ship_Interface.WeaponSlot_List[index];
    }


    private void Update()
    {
        Debug.Log("BuildItemBox = " + BuildItemBox);
        Debug.Log("WeaponItemBox = " + WeaponItemBox);
    }
}
