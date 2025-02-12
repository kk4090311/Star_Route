using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class UI_StarRoute_BackGround : MonoBehaviour
{
    [Title("UI背景移動,背景圖片需要設置成重複模式")]
    //color2--------------------背景圖-------------------------------//
    [Title("主UI背景-背景圖")] public GameObject MainUI_BackGround;
    [LabelText("背景移動速度")] public float MainUI_BackGround_offset;

    //color2--------------------星圖1-------------------------------//
    [Title("主UI背景-星圖1")] public GameObject MainUI_BackGround_Star1;
    [LabelText("星圖1移動速度")] public float MainUI_BackGround_Star1_offset;

    //color2--------------------星圖2"-------------------------------//
    [Title("主UI背景-星圖2")] public GameObject MainUI_BackGround_Star2;
    [LabelText("星圖2移動速度")] public float MainUI_BackGround_Star2_offset;


    private GameObject MainCamera;

    private Material background_mat, star1_mat, star2_mat;

    private Vector2 background_offset, star1_offset, star2_offset;
    private Vector2 Pos;



    private void Start()
    {
        MainCamera = Camera.main.gameObject;
        background_mat = MainUI_BackGround.GetComponent<Renderer>().material;
        star1_mat = MainUI_BackGround_Star1.GetComponent<Renderer>().material;
        star2_mat = MainUI_BackGround_Star2.GetComponent<Renderer>().material;


    }



    void  LateUpdate()
    {

        Pos = MainCamera.transform.position;
        transform.position = Pos;

        MainUI_BackGround_Star1.transform.position = Pos;
        MainUI_BackGround_Star2.transform.position = Pos;
        //color2------------------------"-------------------------------//




        background_offset = new Vector2(MainCamera.transform.position.x / MainUI_BackGround_offset, MainCamera.transform.position.y / MainUI_BackGround_offset);
        star1_offset = new Vector2(MainCamera.transform.position.x / MainUI_BackGround_Star1_offset, MainCamera.transform.position.y / MainUI_BackGround_Star1_offset);
        star2_offset = new Vector2(MainCamera.transform.position.x / MainUI_BackGround_Star2_offset, MainCamera.transform.position.y / MainUI_BackGround_Star2_offset);

        star1_mat.mainTextureOffset = star1_offset;
        star2_mat.mainTextureOffset = star2_offset;
        background_mat.mainTextureOffset = background_offset;
    }
}


