using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUpScript : MonoBehaviour
{
    private TextMeshPro SetTextMeshPro;
    private float DamagePopUpOffset = 1.0f;
    private float DisappearTimer = 5.5f;
    private int SortingOrder = 0;
    private float SizeTimer;
    private Color textColor;
    public static DamagePopUpScript Create(Vector3 Position, int DamageAmount, bool IsCriticalHit)
    {
        Transform damagePopUp = Instantiate(All_Prefab_Table.i.DamagePopUp, Position, Camera.main.transform.rotation);
        DamagePopUpScript damagePopUpScript = damagePopUp.GetComponent<DamagePopUpScript>();
        damagePopUpScript.Setup(DamageAmount, IsCriticalHit);
        return damagePopUpScript;
    }
    private void Awake()
    {
        SetTextMeshPro = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int damageAmount, bool IsCriticalHit)
    {
        SetTextMeshPro.SetText(damageAmount.ToString());
        if (!IsCriticalHit)
        {
            SetTextMeshPro.fontSize = 2.0f;
        }
        else
        {
            SetTextMeshPro.color = Color.red;
            SetTextMeshPro.fontSize = 2.5f;

        }
        textColor = SetTextMeshPro.color;
        SizeTimer = DisappearTimer;
        SortingOrder++;
        SetTextMeshPro.sortingOrder = SortingOrder;
    }
    private void Update()
    {
        transform.position += new Vector3(2.0f * Time.deltaTime, DamagePopUpOffset * Time.deltaTime, 0f);
        if (DisappearTimer > SizeTimer * 0.5f)
        {
            float Addsize = 1.0f;
            transform.localScale += Vector3.one * Addsize * Time.deltaTime;
        }
        else
        {
            float Minsize = 1.0f;
            transform.localScale -= Vector3.one * Minsize * Time.deltaTime;
        }


        if (DisappearTimer > 0)
        {
            float DisappearSpeed = 2.0f;
            textColor.a -= DisappearSpeed * Time.deltaTime;
            SetTextMeshPro.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
        DisappearTimer -= Time.deltaTime;

    }

}
