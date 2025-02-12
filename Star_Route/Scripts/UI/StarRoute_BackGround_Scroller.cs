using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class StarRoute_BackGround_Scroller : MonoBehaviour
{

    [Title("背景圖自動選轉")]
    [LabelText("背景移動速度")] public float Speed;
    [LabelText("背景Material")] private Material background_mat;

    private Vector2 vector2;

    // Start is called before the first frame update
    void Start()
    {
        background_mat = this.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        vector2 = new Vector2(vector2.x + Speed * Time.deltaTime, vector2.y + Speed * Time.deltaTime);
        background_mat.mainTextureOffset = vector2;
    }
}
