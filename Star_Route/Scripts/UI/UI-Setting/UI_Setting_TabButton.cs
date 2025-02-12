using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Setting_TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private UI_Setting_TabGroup UI_Setting_TabGroup;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        UI_Setting_TabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UI_Setting_TabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UI_Setting_TabGroup.OnTabExit(this);
    }


    void Start()
    {
        UI_Setting_TabGroup = GetComponentInParent<UI_Setting_TabGroup>();
        UI_Setting_TabGroup.TabGroup_AddToList(this);
    }
}
