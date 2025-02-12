using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Ship_Interface_ItemBox : MonoBehaviour, IPointerClickHandler
{

    public UI_Ship_Interface UI_Ship_Interface;
    private void Start()
    {
        UI_Ship_Interface = GameObject.Find("UI-裝備介面").GetComponent<UI_Ship_Interface>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        UI_Ship_Interface.ItemBox_Click(this.transform.GetSiblingIndex());
    }
}
