using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using StarRouteSettings;

public class UI_Ship_Interface : MonoBehaviour
{
    [Title("UI-裝備介面物件參考需指定")]
    [LabelText("UI-裝備介面-左側介面Content")] public Transform content;
    [LabelText("UI-裝備介面-左側介面-ItemBox")] public GameObject ItemBox;
    [LabelText("UI-裝備介面-中間介面-顯示器-顯示內容")] public Transform Monitor;
    [LabelText("UI-裝備介面-中間介面-數值表-文字表")] public Transform TextTable;


    [Title("UI-裝備介面內部參數")]

    [LabelText("StarRouteGameManager 單例船艦列表 (Prefab)")] public List<GameObject> Instance_Ship_List;
    [LabelText("UI-裝備介面-本地船艦列表")] public List<GameObject> Local_Ship_List;



    [LabelText("UI-裝備介面-Curret_Ship_Weapon")] public List<GameObject> WeaponSlot_List;


    [LabelText("UI-裝備介面-下側介面-裝備格")] public List<GameObject> UI_Weapon_List;
    [LabelText("ItemBox_目前被選則的BOX")] public int ItemBox_Index;
    [LabelText("ItemBox_總數量")] public int shipCounter;



    private void OnEnable()
    {




        InitializeShipList();
        List_Display();

    }
    private void OnDisable()
    {
        foreach (GameObject ship in Local_Ship_List)
        {
            ship.SetActive(false);
        }
    }


    private void Start()
    {
        //Instance_Ship_List 指向 Player_Ship_List
        Instance_Ship_List = StarRouteGameManager.Instance.GetPlayer_ScriptObjects().Player_Ship_List;
        shipCounter = Instance_Ship_List.Count;
        List_Display();
        InitializeShipList();
    }
    private void Update()
    {

        ShipCountChecker();
    }


    public void Add(GameObject ship_Object)
    {
        Instance_Ship_List.Add(ship_Object);
    }
    public void Remove(GameObject ship_Object)
    {
        Instance_Ship_List.Remove(ship_Object);
    }


    public void SaveTo_InstanceList()
    {
        GameObject ship = SysUtils.ConvertToPrefab(Local_Ship_List.ElementAt(ItemBox_Index));
        StarRouteGameManager.Instance.player_ScriptObjects.Player_Ship_List.RemoveAt(ItemBox_Index);
        StarRouteGameManager.Instance.player_ScriptObjects.Player_Ship_List.Insert(ItemBox_Index, ship);
    }

    public void InitializeShipList()
    {

        if (Local_Ship_List != null && Local_Ship_List.Count > 0)
        {
            foreach (GameObject ship in Local_Ship_List)
            {
                Destroy(ship);
            }

        }

        Local_Ship_List.Clear();
        Instance_Ship_List = StarRouteGameManager.Instance.GetPlayer_ScriptObjects().Player_Ship_List;
        foreach (GameObject ship in Instance_Ship_List)
        {
            GameObject gameObject = Instantiate(ship);
            gameObject.SetActive(false);
            Local_Ship_List.Add(gameObject);
        }

    }

    public virtual void ItemBox_Click(int index)
    {
        ItemBox_Index = index;
        Debug.Log("ItemBox_Index = " + ItemBox_Index);

        InitializeShipList();
        Monitor_Display(index);
        TextTable_Display(index);
        Weapon_Display(index);

    }


    public void ShipCountChecker()
    {
        if (shipCounter != Instance_Ship_List.Count)
        {
            shipCounter = Instance_Ship_List.Count;
            Debug.Log($"ship_List.Count has changed: {shipCounter} to {Instance_Ship_List.Count}");
            Debug.Log($"Local_Ship_List: {Local_Ship_List.Count} to {Instance_Ship_List.Count}");
            Local_Ship_List.Clear();
            Local_Ship_List.AddRange(Instance_Ship_List);
            List_Display();
        }
    }
    public void List_Display()
    {

        if (content.childCount != 0)
        {
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
                Debug.Log("List_Display Reset");
            }
        }

        foreach (var ship in Local_Ship_List)
        {
            GameObject itemBox = Instantiate(ItemBox, content);
            Image image = itemBox.transform.Find("Ship-Image").GetComponent<Image>();
            itemBox.name = (itemBox.transform.GetSiblingIndex() + 1).ToString();
            image.sprite = ship.GetComponent<SpriteRenderer>().sprite;
            image.preserveAspect = true;
            itemBox.transform.Find("Ship-Name").GetComponent<TextMeshProUGUI>().text = ship.GetComponent<StarRoute_Ship>().ship_ScriptObjects.Ship_Name;
        }

    }
    public void Monitor_Display(int index)
    {
        if (Local_Ship_List != null)
        {

            RawImage rawImage = Monitor.GetComponent<RawImage>();
            RenderTexture renderTexture = new RenderTexture(512, 512, 24);

            GameObject gameObject = Local_Ship_List.ElementAt(index);
            gameObject.transform.position = new Vector3(1000, 1000, 1000);
            gameObject.SetActive(true);

            gameObject.transform.Find("船艦-攝像機").GetComponent<Camera>().targetTexture = renderTexture;
            rawImage.texture = renderTexture;
        }
    }
    public void Weapon_Display(int index)
    {
        if (Local_Ship_List != null)
        {
            GameObject ship = Local_Ship_List.ElementAt(index);
            WeaponSlot_List = Ship_Utils.Get_Ship_Turret(ship.transform);

            for (int i = 0; i < WeaponSlot_List.Count; i++)
            {
                GameObject TurretSlot = WeaponSlot_List.ElementAt(i);
                UI_Weapon_List.ElementAt(i).transform.Find("Raw-Image").gameObject.SetActive(true);
                RawImage rawImage = UI_Weapon_List.ElementAt(i).transform.Find("Raw-Image").GetComponent<RawImage>();
                RenderTexture renderTexture = new RenderTexture(512, 512, 24);
                Transform turretCamera = TurretSlot.transform.Find("砲塔-攝像機");
                turretCamera.gameObject.SetActive(true);
                turretCamera.GetComponent<Camera>().targetTexture = renderTexture;
                rawImage.texture = renderTexture;
            }

        }

    }

    public void TextTable_Display(int index)
    {
        GameObject gameObject = Local_Ship_List.ElementAt(index);
        StarRoute_Ship starRouteShip = gameObject.GetComponent<StarRoute_Ship>();

        TextTable.Find("船艦種類-TMP").GetComponent<TextMeshProUGUI>().text = $"船艦種類 :{starRouteShip.ship_ScriptObjects.Ship_Type}";
        TextTable.Find("船艦裝甲-TMP").GetComponent<TextMeshProUGUI>().text = $"船艦裝甲 :{starRouteShip.ship_ScriptObjects.Ship_Hull}";
        TextTable.Find("船艦護頓-TMP").GetComponent<TextMeshProUGUI>().text = $"船艦護頓 :{starRouteShip.ship_ScriptObjects.Ship_Shield}";
        TextTable.Find("船艦最大能量值-TMP").GetComponent<TextMeshProUGUI>().text = $"船艦最大能量值 :{starRouteShip.ship_ScriptObjects.Ship_MaxEnergy}";
        TextTable.Find("船艦能量消散速度-TMP").GetComponent<TextMeshProUGUI>().text = $"船艦能量消散速度 :{starRouteShip.ship_ScriptObjects.Ship_EnergyConsumptionRate}";
        TextTable.Find("船艦最大速度-TMP").GetComponent<TextMeshProUGUI>().text = $"船艦最大速度 :{starRouteShip.ship_ScriptObjects.Ship_MoveSpeed}";
        TextTable.Find("船艦旋轉速度-TMP").GetComponent<TextMeshProUGUI>().text = $"船艦旋轉速度 :{starRouteShip.ship_ScriptObjects.Ship_RotationSpeed}";
        TextTable.Find("船艦曲速引擎推力-TMP").GetComponent<TextMeshProUGUI>().text = $"船艦曲速引擎推力 :{starRouteShip.ship_ScriptObjects.Ship_ThrustingSpeed}";
        TextTable.Find("船艦搜尋範圍-TMP").GetComponent<TextMeshProUGUI>().text = $"船艦搜尋範圍 :{starRouteShip.ship_ScriptObjects.Ship_SearchRadius}";

    }


}
