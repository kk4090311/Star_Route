using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;


public class UI_RTS_Select_Box : Ship_HealthBar, IPointerClickHandler
{
    //color1------------UI_RTS_Interface選擇使用---------   -------//
    [SerializeField] protected GameObject unitToAdd;

    protected Image Ship_Image;
    protected Image StateImage;
    public Sprite[] StateList;

    protected GameObject lastClickedUnit;

    protected float lastClickTime;

    public Slider Select_Box_Slider;


    //color1------------UI_RTS_Interface選擇使用----------------//

    //color1------------UI_RTS_格子圖片顏色----------------//
    public Color greenColor = Color.green; // 绿色
    public Color redColor = Color.red; // 红色



    public virtual void Init_RTS_SelectBox(GameObject unit)
    {
        unitToAdd = unit;

        Ship_slider = unitToAdd.GetComponent<StarRoute_Ship>().ship_HealthBar.Ship_slider;

        Select_Box_Slider = transform.Find("Ship_HealthBar_Canvas/Ship_HealthBar").GetComponent<Slider>();

        Ship_Image = transform.Find("Image").GetComponent<Image>();
        StateImage = transform.Find("StateImage").GetComponent<Image>();

    }


    protected override void LateUpdate()
    {

        if (unitToAdd != null)
        {
            Select_Box_Slider.maxValue = Ship_slider.maxValue;
            Select_Box_Slider.value = Ship_slider.value;
            UpdateHealthColor();
            UpdateState();
        }
    }
    protected virtual void UpdateState()
    {
        AIState aIState = unitToAdd.GetComponent<StarRouteAI>().currentState;
        switch (aIState)
        {
            case AIState.Idle:

                StateImage.sprite = StateList[0];
                break;
            case AIState.Searching:

                StateImage.sprite = StateList[1];
                break;
            case AIState.Attacking:

                StateImage.sprite = StateList[2];
                break;
            case AIState.Chasing:
                StateImage.sprite = StateList[3];
                break;
            case AIState.Retreating:
                StateImage.sprite = StateList[4];
                break;
            case AIState.UnitControl:
                StateImage.sprite = StateList[5];
                break;
        }

    }
    protected virtual void UpdateHealthColor()
    {
        // 计算当前血量比例
        float healthPercent = Ship_slider.value / Ship_slider.maxValue;

        // 根据血量比例设置颜色
        Color targetColor = Color.Lerp(redColor, greenColor, healthPercent);

        // 使用DOTween平滑过渡颜色
        Ship_Image.DOColor(targetColor, 0.2f);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {

        if (unitToAdd != null)
        {
            Unit_Selections.instance.Unit_ClickSelect(unitToAdd);

            if (unitToAdd == lastClickedUnit && Time.time - lastClickTime < 0.5f)
            {
                Camera mainCamera = Camera.main;
                mainCamera.transform.position = unitToAdd.transform.position;
                Debug.Log("Double click detected on unit: " + unitToAdd.name);
            }
        }

        // 更新上一次点击的单位和时间
        lastClickedUnit = unitToAdd;
        lastClickTime = Time.time;
    }

}
