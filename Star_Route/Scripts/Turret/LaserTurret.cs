
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using StarRouteSettings;

using System.Linq;

public class LaserTurret : Base_Turret
{
    [SerializeField] private Vector2 Laser_boxSize;
    [SerializeField] LineRenderer lineRenderer;
    protected override void Start()
    {
        base.Start();
        //!laser use
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        Laser_boxSize = new Vector2(lineRenderer.endWidth, lineRenderer.endWidth);
    }

    protected override List<GameObject> GetEnemiesInRange()
    {
        List<GameObject> enemies = GameObject.FindGameObjectsWithTag(Name_Target).ToList();
        List<GameObject> enemies_missile_bullet = GameObject.FindGameObjectsWithTag(Name_Target + "_Missile_Bullet").ToList();
        List<GameObject> enemies_bullet = GameObject.FindGameObjectsWithTag(Name_Target + "_Bullet").ToList();
        enemies.AddRange(enemies_bullet);
        enemies.AddRange(enemies_missile_bullet);
        return enemies;
    }
    protected override void Turret_Rotation()
    {
        if (TargetTransform == null)
        {
            //?-----------模式一-----------Cannon_Angle使用瞬間旋轉----------------//
            //Cannon_Angle = Target_Angle;使用瞬間旋轉
            //?-----------模式二-----------Quaternion.Lerp旋轉--------------------//
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(transform.parent.eulerAngles), turret_ScriptObjects.Turn_Speed * Time.deltaTime);
            lineRenderer.enabled = false;
            return;
        }
        //?-----------模式一-----------Cannon_Angle使用瞬間旋轉----------------//
        Cannon_Angle = Target_Angle;//使用瞬間旋轉
        lineRenderer.enabled = true;
        Shoot();
    }

    protected override void Shoot()
    {
        lineRenderer.sortingLayerName = "Turret_Layer";

        int layerMask = SysUtils.Set_Bullet_CollideLayerByTag(transform.root.tag, turret_ScriptObjects.weaponType);
        Vector2 dir = Vector2.up;
        Vector3 origin = transform.position;

        RaycastHit2D raycastHit2D = Physics2D.BoxCast(origin, Laser_boxSize, transform.eulerAngles.z, transform.up, turret_ScriptObjects.FireRange, layerMask);

        Vector3 endPos;

        if (raycastHit2D.collider != null)
        {
            if (FireCountDown <= 0.0f)
            {
                FireCountDown = 1.0f / turret_ScriptObjects.FireRate;
                IDamageable iDamageable = raycastHit2D.collider.gameObject.GetComponent<IDamageable>();
                if (iDamageable != null)
                {
                    iDamageable.Get_Damage(turret_ScriptObjects.weaponType);
                }
            }
            endPos = raycastHit2D.point;
            Debug.Log($"{raycastHit2D.collider.name}");
            //-------------------------------------//

        }
        else
        {
            endPos = transform.position + (transform.up * turret_ScriptObjects.FireRange);
        }

        lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));
        lineRenderer.SetPosition(1, endPos);
        Debug.DrawRay(transform.position, transform.TransformDirection(dir * turret_ScriptObjects.FireRange), Color.red);
        Vector3 topLeft = origin + new Vector3(-Laser_boxSize.x / 2, Laser_boxSize.y / 2, 0);
        Vector3 topRight = origin + new Vector3(Laser_boxSize.x / 2, Laser_boxSize.y / 2, 0);
        Vector3 bottomLeft = origin + new Vector3(-Laser_boxSize.x / 2, -Laser_boxSize.y / 2, 0);
        Vector3 bottomRight = origin + new Vector3(Laser_boxSize.x / 2, -Laser_boxSize.y / 2, 0);
        Debug.DrawLine(topLeft, topRight, Color.green);
        Debug.DrawLine(topRight, bottomRight, Color.green);
        Debug.DrawLine(bottomRight, bottomLeft, Color.green);
        Debug.DrawLine(bottomLeft, topLeft, Color.green);


        Vector3 topLeft2 = origin + transform.up * turret_ScriptObjects.FireRange + new Vector3(-Laser_boxSize.x / 2, Laser_boxSize.y / 2, 0);
        Vector3 topRight2 = origin + transform.up * turret_ScriptObjects.FireRange + new Vector3(Laser_boxSize.x / 2, Laser_boxSize.y / 2, 0);
        Vector3 bottomLeft2 = origin + transform.up * turret_ScriptObjects.FireRange + new Vector3(-Laser_boxSize.x / 2, -Laser_boxSize.y / 2, 0);
        Vector3 bottomRight2 = origin + transform.up * turret_ScriptObjects.FireRange + new Vector3(Laser_boxSize.x / 2, -Laser_boxSize.y / 2, 0);
        Debug.DrawLine(topLeft2, topRight2, Color.green);
        Debug.DrawLine(topRight2, bottomRight2, Color.green);
        Debug.DrawLine(bottomRight2, bottomLeft2, Color.green);
        Debug.DrawLine(bottomLeft2, topLeft2, Color.green);


        Debug.DrawRay(origin, transform.up * turret_ScriptObjects.FireRange, Color.yellow);

        // Debug.DrawRay(origin + new Vector3(-Laser_boxSize.x / 2, 0, 0), transform.up * turret_ScriptObjects.FireRange, Color.yellow);
        //Debug.DrawRay(origin + new Vector3(Laser_boxSize.x / 2, 0, 0), transform.up * turret_ScriptObjects.FireRange, Color.yellow);

        //Debug.DrawLine(topLeft, topLeft2, Color.yellow);
        Debug.DrawLine(topRight, topRight2, Color.yellow);
        Debug.DrawLine(bottomLeft, bottomLeft2, Color.yellow);
        //Debug.DrawLine(bottomRight, bottomRight2, Color.yellow);

    }


}