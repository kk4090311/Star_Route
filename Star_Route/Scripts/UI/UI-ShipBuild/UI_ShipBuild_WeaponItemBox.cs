using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ShipBuild_WeaponItemBox : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    public UI_ShipBuild_Interface UI_ShipBuild_Interface;
    private void Start()
    {
        UI_ShipBuild_Interface = GameObject.Find("UI-裝備介面-下側介面").GetComponent<UI_ShipBuild_Interface>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        UI_ShipBuild_Interface.WeaponItemBoxOnClick(this.transform.GetSiblingIndex());
    }
}
