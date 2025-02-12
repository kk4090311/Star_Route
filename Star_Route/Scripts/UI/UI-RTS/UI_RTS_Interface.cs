using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class UI_RTS_Interface : MonoBehaviour
{
    [Title("UI_RTS_Interface介面物件 選擇格子")]
    //color1------------UI_RTS_Interface選擇格子----------------//
    [LabelText("UI_RTS選擇Panel ")] public RectTransform RtsItemBox_Panel;
    [LabelText("UI_RTS選擇格子Prefab")] public GameObject RtsItemBox;
    [LabelText("UI_RTS選擇格子的容器 ")] public Transform RtsItemBox_Container;
    //color1------------UI_RTS_Interface選擇格子----------------//

    [Title("UI_RTS_Interface介面物件 InfoPanel")]
    //color2------------UI_RTS_Interfacen-InfoPanel----------------//
    [LabelText("UI_RTSInfoPanel ")] public Transform RtsInfoPanel;
    private float RtsPanelArrow_Index = 0;


    private void Start()
    {

        RtsItemBox = Resources.Load<GameObject>("Prefabs/UI/UI-RTS-MainUI-Bar-Panel-Box");
        RtsItemBox_Panel = GameObject.Find("UI-RTS-MainUI-Bar-panel").transform.GetComponent<RectTransform>();
        RtsItemBox_Container = GameObject.Find("UI-RTS-MainUI-Bar-panel").transform;

        Initialize_RTS_Interface();
    }

    public void Initialize_RTS_Interface()
    {
        RtsItemBox_Visual();
    }

    //color1------------UI_RTS_Interface選擇格子----------------//
    private void RtsItemBox_Visual()
    {
        Clear_RtsItemBox_Container();
        foreach (GameObject ship in Unit_Selections.instance.Unit_List)
        {
            GameObject itemBox = Instantiate(RtsItemBox, RtsItemBox_Container);
            itemBox.GetComponent<UI_RTS_Select_Box>().Init_RTS_SelectBox(ship);
            itemBox.name = itemBox.transform.GetSiblingIndex().ToString();
            Image image = itemBox.transform.Find("Image").GetComponent<Image>();
            image.sprite = ship.GetComponent<StarRoute_Ship>().ship_ScriptObjects.Ship_SmallIcon;
            Debug.Log("image.sprite" + image.sprite.name);
            image.SetNativeSize();
        }
        for (int i = 0; i < 20 - Unit_Selections.instance.Unit_List.Count; i++)
        {
            GameObject itemBox = Instantiate(RtsItemBox, RtsItemBox_Container);
            itemBox.name = (itemBox.transform.GetSiblingIndex() + 1).ToString();
        }
    }

    private void Clear_RtsItemBox_Container()
    {
        foreach (Transform child in RtsItemBox_Container)
        {
            Destroy(child.gameObject);
        }
    }
    //color1------------UI_RTS_Interface選擇格子----------------//

    //color2------------UI_RTS_Interfacen-InfoPanel----------------//
    private void InfoPanel_Visual()
    {
        // Unit_Selections.instance.Unit_Selected_List[0].
    }

    //color3------------UI_RTS_Interface 按鈕----------------//
    public void RTS_Interface_Arrow_R()
    {
        RtsItemBox_Panel.DOMove(RtsItemBox_Panel.position - new Vector3(100f, 0f, 0f), 0.3f).SetEase(Ease.OutQuad);
        RtsPanelArrow_Index += 1;
    }
    public void RTS_Interface_Arrow_L()
    {

        if (RtsPanelArrow_Index > 1)
        {
            RtsItemBox_Panel.DOMove(RtsItemBox_Panel.position + new Vector3(100f, 0f, 0f), 0.3f).SetEase(Ease.OutQuad);
            RtsPanelArrow_Index -= 1;

        }
        else
        {
            // 重製回到畫面
            RtsItemBox_Panel.position = new Vector3(965.5f, 106f, 0f);
            RtsPanelArrow_Index = 0;
        }
    }






}
