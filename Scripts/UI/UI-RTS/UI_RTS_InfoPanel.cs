
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using StarRouteSettings;
public class UI_RTS_InfoPanel : UI_RTS_Select_Box
{
    public Image InfoPanel_Image;
    public Image InfoPanel_StateImage;
    public Sprite Default_InfoPanel_Sprite;
    public RenderTexture renderTexture;
    public UI_HUD_GamePlay UI_HUD_GamePlay;
    private void Start()
    {
        InfoPanel_Image = GameObject.Find("UI-RTS-MainUI-Bar-InfoPanel-Icon-Image").GetComponent<Image>();
        Default_InfoPanel_Sprite = InfoPanel_Image.sprite;
        Select_Box_Slider = transform.Find("UI-RTS-MainUI-Bar-InfoPanel-Sub/Ship_HealthBar_Canvas/Ship_HealthBar").GetComponent<Slider>();
        StateImage = InfoPanel_StateImage;
    }




    protected override void LateUpdate()
    {
        if (Unit_Selections.instance.Unit_Selected_List.Count > 0)
        {
            unitToAdd = Unit_Selections.instance.Unit_Selected_List[0];
            Debug.Log("unitToAdd" + unitToAdd.name);
            Ship_slider = Unit_Selections.instance.Unit_Selected_List[0].GetComponent<StarRoute_Ship>().ship_HealthBar.Ship_slider;
            InfoPanel_Image.sprite = unitToAdd.GetComponent<StarRoute_Ship>().ship_ScriptObjects.Ship_BigIcon;
            InfoPanel_Image.transform.rotation = unitToAdd.transform.rotation;

            UpdateHealthColor();
            UpdateState();
        }
        else
        {
            unitToAdd = null;
            InfoPanel_Image.sprite = Default_InfoPanel_Sprite;
        }


    }



    protected override void UpdateHealthColor()
    {
        if (unitToAdd != null)
        {
            Select_Box_Slider.maxValue = Ship_slider.maxValue;
            Select_Box_Slider.value = Ship_slider.value;


            // 计算当前血量比例
            float healthPercent = Ship_slider.value / Ship_slider.maxValue;

            // 根据血量比例设置颜色
            Color targetColor = Color.Lerp(redColor, greenColor, healthPercent);

            // 使用DOTween平滑过渡颜色
            InfoPanel_Image.DOColor(targetColor, 0.2f);
        }
    }


    public override void OnPointerClick(PointerEventData eventData)
    {

        if (unitToAdd != null)
        {
            Unit_Selections.instance.Unit_ClickSelect(unitToAdd);
            unitToAdd.GetComponent<StarRoute_Ship>().StarRoute_Ship_PlayerControl(true);
            Debug.Log(unitToAdd.name + " isPlayerControl true");
            UI_HUD_GamePlay.Set_RtsPlayer(unitToAdd);

            if (unitToAdd == lastClickedUnit && Time.time - lastClickTime < 0.5f)
            {
                unitToAdd.GetComponent<StarRoute_Ship>().StarRoute_Ship_PlayerControl(false);
                UI_HUD_GamePlay.Set_RtsPlayer(null);
                Debug.Log("Double click detected on unit: " + unitToAdd.name);
            }
        }

        // 更新上一次点击的单位和时间
        lastClickedUnit = unitToAdd;
        lastClickTime = Time.time;
    }

}
