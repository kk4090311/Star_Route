using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Ship_HealthBar : MonoBehaviour
{
    public Slider Ship_slider;



    public void Init_Ship_HealthBar()
    {
        Ship_slider = gameObject.GetComponent<Slider>();
        if (Ship_slider == null)
        {
            Debug.LogError("Ship_slider not found !");
            return;
        }
    }

    protected virtual void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
        transform.position = transform.root.position + new Vector3(0, 1.5f, 0);

    }

    public virtual void SetMaxHealth(float health)
    {

        Ship_slider.maxValue = health;
        Ship_slider.value = health;
    }

    public virtual void SetHealth(float health)
    {
        Ship_slider.value = health;

    }
}