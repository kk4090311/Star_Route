using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using StarRouteSettings;
using Shapes;
using DG.Tweening;
using Pathfinding;
using System.Linq;



public class StarRoute_Ship : StarRoute_BaseShip, IMovement
{
    //color1 StarRoute_BaseShip 父類
    // [Title("StarRoute_各自船艦")]
    // [LabelText("船艦數據放置ScriptObjects")] public Ship_ScriptObjects ship_ScriptObjects;
    // [LabelText("自動獲取--船艦數據-血條")] public Ship_HealthBar ship_HealthBar;
    // [Title("船艦數據")]
    // [LabelText("自動獲取--船艦名稱")] public string ship_Name;
    // [LabelText("自動獲取--船艦裝甲")] public float ship_Hull;
    // [LabelText("自動獲取--船艦護頓")] public float ship_Shield;
    // [LabelText("自動獲取--船艦護頓")] public float ship_SearchRadius;
    // [LabelText("自動獲取--船艦敵人")] public List<GameObject> ship_TargetList;

    //color1 StarRoute_Ship 子類
    [Title("StarRoute_Ship 子類")]
    [LabelText("自動獲取--玩家Rigidbody")] public Rigidbody2D rb;
    [LabelText("玩家手動操作中")] public bool PlayerControl;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (PlayerControl) StarRoute_Ship_IMovement();
        StarRoute_Ship_TargetInRange();
        
    }
    public void StarRoute_Ship_PlayerControl(bool isPlayerControl)
    {
        PlayerControl = isPlayerControl;
        if (isPlayerControl == true)
        {
            transform.GetComponent<AIPath>().enabled = false;
            transform.GetComponent<AIDestinationSetter>().enabled = false;

            Destroy(transform.GetComponent<Unit_Movement>()._TargetWayPoint);
            Camera.main.GetComponent<StarRouteCamera_Game>().target = this.transform;
        }
        else
        {
            transform.GetComponent<AIPath>().enabled = true;
            transform.GetComponent<AIDestinationSetter>().enabled = true;
            Transform pos = transform;
            transform.GetComponent<AIDestinationSetter>().target = pos;
            Camera.main.GetComponent<StarRouteCamera_Game>().target = null;
        }

    }


    private void StarRoute_Ship_TargetInRange()
    {
        List<GameObject> enemies = Ship_Utils.Find_Tag_GameObject_InRange(this.transform, ship_SearchRadius, SysUtils.Enemy_Tag);
        List<GameObject> enemies_missileBullets = Ship_Utils.Find_Tag_GameObject_InRange(this.transform, 15f, SysUtils.Enemy_Missile_Bullet_Tag);
        if (enemies != null)
        {
            ship_TargetList= enemies.ToList();
        }
         if (enemies_missileBullets != null)
        {
            foreach (var missile in enemies_missileBullets)
            {
                missile.GetComponent<Missile_Bullet>().Draw_Missile_Visual();
            }
        }

    }






    private void StarRoute_Ship_IMovement()
    {
        //color2------------IMovement接口實現----------------//
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Rotate(horizontal);
        Move(horizontal, vertical);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Thrust(horizontal, vertical);
        }
        //color2------------IMovement接口實現----------------//
    }


    //color2------------IMovement接口實現----------------//
    public void Move(float horizontalInput, float verticalInput)
    {
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * (ship_ScriptObjects.Ship_MoveSpeed / 2);
        rb.AddRelativeForce(movement, ForceMode2D.Force);
    }
    public void Rotate(float horizontalInput)
    {
        rb.rotation -= horizontalInput * ship_ScriptObjects.Ship_RotationSpeed;
    }
    public void Thrust(float horizontalInput, float verticalInput)
    {
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * (ship_ScriptObjects.Ship_MoveSpeed / 2);
        rb.AddRelativeForce(movement * (ship_ScriptObjects.Ship_ThrustingSpeed - 1), ForceMode2D.Force);
    }
}
