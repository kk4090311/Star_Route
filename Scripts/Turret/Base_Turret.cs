using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarRouteSettings;
using System.Linq;
using Sirenix.OdinInspector;

public class Base_Turret : MonoBehaviour
{

    //////////////////////////////////////////
    public Turret_ScriptObjects turret_ScriptObjects;
    //////////////////////////////////////////

    [SerializeField][LabelText("搜尋目標對象的TAG")] protected string Name_Target;
    [SerializeField][LabelText("目標對象Transform")] protected Transform TargetTransform;
    protected float Target_Distance = Mathf.Infinity;
    protected float Target_Angle = 0;
    protected float FireCountDown = 0.0f;

    //////////////////////////////////////////
    DebugLog debugLog = new DebugLog(true);
    //////////////////////////////////////////
    protected float Cannon_Angle//以X軸起算Cannon的Angle
    {
        get => (transform.eulerAngles.z + 90) % 360;
        set
        {
            value -= 90;
            while (value < 0) value += 360;//負角度設為0~360
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, value);
        }
    }

    protected float CannonBase_Angle//以X軸起算CannonBase_Angle的Angle
    {
        get => (transform.parent.eulerAngles.z + 90) % 360;
        set
        {
            value -= 90;
            while (value < 0) value += 360;//負角度設為0~360
            transform.parent.eulerAngles = new Vector3(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, value);

        }
    }
 
    protected virtual void Start()
    {
        Name_Target = SysUtils.targetSearchTag(transform);
        debugLog.IsDebug = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        FindAndSet_CannonAngle_ToNearestInRange();
        Turret_Rotation();
        debugLog.ShowMesg();
        debugLog.Clear();
        FireCountDown -= Time.deltaTime;
      
    }


    //color1----------------實作功能--------------------//

    protected bool IsInFireRange(Vector3 TargetPos, out float distance, out float angle)
    {
        angle = SysUtils.Get_Angle(transform.position, TargetPos);
        distance = Vector3.Distance(this.transform.position, TargetPos);

        if (distance > turret_ScriptObjects.FireRange)
        {
            return false;
        }
        debugLog.Add("IsInRange", $"[A={angle},D={distance}]");

        float angle_max = (CannonBase_Angle + turret_ScriptObjects.MaxFireAngle) % 360;
        float angle_min = (CannonBase_Angle - turret_ScriptObjects.MaxFireAngle + 360) % 360;
        debugLog.Add("anglebybase", $"[{angle},{angle_min},{angle_max}]");
        bool ok = false;
        if (angle_min > angle_max)//min~max由大到小，min>max 交換
        {

            float i = angle_max;
            angle_max = angle_min;
            angle_min = i;
            //判斷!=0~270之間
            ok = !(angle > angle_min && angle < angle_max);
        }
        else
        {
            //min~max由小到大
            ok = (angle >= angle_min && angle <= angle_max);

        }
        return ok;
    }
    //color3----------------自動攻擊專用--------------------//
    protected virtual List<GameObject> GetEnemiesInRange()
    {
        List<GameObject> enemies = GameObject.FindGameObjectsWithTag(Name_Target).ToList();
        return enemies;
    }

    protected virtual void FindAndSet_CannonAngle_ToNearestInRange()
    {

        List<GameObject> enemies = GetEnemiesInRange();

        float angle, distance;
        Target_Distance = Mathf.Infinity;
        Target_Angle = 0;
        TargetTransform = null;
        foreach (GameObject enemy in enemies)
        {
            // Debug.Log(enemy.name);
            if (IsInFireRange(enemy.transform.position, out distance, out angle) != true)
            {
                continue;
            }
            //Target Found
            if (distance < Target_Distance)
            {
                TargetTransform = enemy.transform;
                Target_Distance = distance;
                Target_Angle = angle;
            }

        }
        // if (TargetTransform != null)
        // {
        //     Vector3 targetTop = new Vector3(TargetTransform.position.x, TargetTransform.position.y + 0.5f, TargetTransform.position.z);
        //     Vector3 targetDown = new Vector3(TargetTransform.position.x, TargetTransform.position.y - 0.5f, TargetTransform.position.z);
        //     Vector3 targetTop2 = new Vector3(TargetTransform.position.x + 0.5f, TargetTransform.position.y, TargetTransform.position.z);
        //     Vector3 targetDown2 = new Vector3(TargetTransform.position.x - 0.5f, TargetTransform.position.y, TargetTransform.position.z);
        //     Debug.DrawLine(targetTop, targetTop2, Color.yellow);
        //     Debug.DrawLine(targetTop2, targetDown, Color.yellow);
        //     Debug.DrawLine(targetDown2, targetTop, Color.yellow);
        //     Debug.DrawLine(targetDown, targetDown2, Color.yellow);
        //     debugLog.Add("target found", $"[{TargetTransform.name},{Target_Distance.ToString("F1")},{Target_Angle.ToString("F1")}]");
        // }
    }
    //color3----------------自動攻擊專用--------------------//



    //color2-------------------砲台旋轉--------------------//
    protected virtual void Turret_Rotation()
    {
        if (TargetTransform == null)
        {
            //-----------模式一-----------Cannon_Angle使用瞬間旋轉----------------//
            //Cannon_Angle = Target_Angle;使用瞬間旋轉
            //-----------模式二-----------Quaternion.Lerp旋轉--------------------//
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(transform.parent.eulerAngles), turret_ScriptObjects.Turn_Speed * Time.deltaTime);
            return;
        }
        //----------一般砲台----------------//
        //-----------模式一-----------Cannon_Angle使用瞬間旋轉----------------//
        //Cannon_Angle = Target_Angle;//使用瞬間旋轉
        //-----------模式二-----------Quaternion.Lerp旋轉--------------------//
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, Target_Angle - 90)), turret_ScriptObjects.Turn_Speed * Time.deltaTime);
        Shoot();
    }
    //color2-------------------砲台旋轉--------------------//


    //color1-------------------砲台射擊--------------------//
    protected virtual void Shoot()
    {

        Debug.Log("i am shoot from Base_Turret");

    }
    //color1-------------------砲台射擊--------------------//

    //color1-----------------砲台射擊輔助線-----------------//
    private void OnDrawGizmos()
    {
        //画出射程范围
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, turret_ScriptObjects.FireRange);

        //画出Target 位置


    }
}
