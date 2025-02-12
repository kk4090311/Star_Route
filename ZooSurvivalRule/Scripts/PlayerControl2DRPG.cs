using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl2DRPG : MonoBehaviour
{

    private SpriteRenderer spr = null;
    public Animator ani = null;
    private Transform tra = null;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ItemsManager itemsManager;
    [SerializeField] private Transform cameraTargetTra;
    [SerializeField] private Transform MouseTra;
    [SerializeField] private GameObject flashlightOn;
    [SerializeField] private GameObject tools;
    [SerializeField] private GameObject light0;
    [SerializeField] private GameObject light1;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float runSpeed = 7f;
    private float _speed = 5f;

    private void Start()
    {
        tra = GetComponent<Transform>();
        spr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
        itemsManager = GameObject.Find("Manager").GetComponent<ItemsManager>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        { _speed = runSpeed; }
        else
        { _speed = speed; }

        ToolsControl();

    }

    private void FixedUpdate()
    {
        CameraMove();

        if(GameManager.GetInstance().moveLock)
        {
            ani.SetBool("Walk", false);
        }
        else { Move(); }
    }

    private Vector2 facing = new Vector2(0, 0);

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0)
        { ani.SetBool("Walk", true); }
        else
        { ani.SetBool("Walk", false); }

        if (h != 0 && v != 0)
        { transform.position += new Vector3((h / 1.414f) * _speed * Time.deltaTime, (v / 1.414f) * _speed * Time.deltaTime, 0); }
        else
        { transform.position += new Vector3(h * _speed * Time.deltaTime, v * _speed * Time.deltaTime, 0); }

        if (Mathf.Abs(h) + Mathf.Abs(v) != 0)
        { facing = new Vector2(h, v); }
        
        if(Mathf.Abs(h) + Mathf.Abs(v) == 0)
        { return;}

        ani.SetFloat("hSpeed", h);
        ani.SetFloat("vSpeed", v);
    }

    private void CameraMove()
    {
        cameraTargetTra.localPosition = new Vector3(facing.x, facing.y, 0f);
    }

    private void ToolsControl()
    {
        //開關燈與切換模式
        if(itemsManager.flashLightOn == true)
        {
            flashlightOn.SetActive(true);
            if(itemsManager.flashLightMode == 0) 
            { light0.SetActive(true); }
            else { light0.SetActive(false); }

            if (itemsManager.flashLightMode == 1)
            { light1.SetActive(true); }
            else { light1.SetActive(false); }
        }
        else
        {
            flashlightOn.SetActive(false);
            light0.SetActive(false);
            light1.SetActive(false);
        }

        //指向
        Vector3 direction = MouseTra.position - tra.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        tools.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
