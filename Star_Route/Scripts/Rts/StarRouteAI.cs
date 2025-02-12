using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Sirenix.OdinInspector;
using UnityEngine;
public enum AIState
{
    Idle,
    Searching,
    Chasing,
    Attacking,
    Retreating,
    UnitControl,
}
public class StarRouteAI : MonoBehaviour
{


    [Title("StarRoute_AI")]
    [LabelText("StarRouteAI - 目前狀態")] public AIState currentState = AIState.Idle;
    [LabelText("自動獲取--StarRouteAI - 目前最接近的敵人")] public GameObject Nearest_Target;
    [Title("StarRoute_AI--移動目標點")] public Transform WayPoint;
    [LabelText("自動獲取--StarRouteAI - 目前搜索狀態距離")] public float searchRadius;
    [LabelText("自動獲取--StarRouteAI - 目前攻擊狀態距離")] public float attackRange;
    [LabelText("自動獲取--StarRouteAI - 目前移動速度")] public float movementSpeed;
    [LabelText("自動獲取--StarRouteAI - 目前旋轉速度")] public float rotationSpeed;
    [LabelText("自動獲取--AIDestinationSetter")] public StarRoute_BaseShip starRoute_BaseShip;
    [LabelText("自動獲取--AIDestinationSetter")] public AIPath aIPath;
    [LabelText("自動獲取--AIDestinationSetter")] public AIDestinationSetter aIDestinationSetter;
    [SerializeField] private float timer; // 初始化計時器

    private void Start()
    {
        starRoute_BaseShip = transform.GetComponent<StarRoute_BaseShip>();
        aIDestinationSetter = transform.GetComponent<AIDestinationSetter>();
        aIPath = transform.GetComponent<AIPath>();
        WayPoint = new GameObject { name = transform.name + " MoveTo" }.transform;
        WayPoint.position = transform.position;


        searchRadius = starRoute_BaseShip.ship_SearchRadius;
        attackRange = starRoute_BaseShip.ship_AttackRange;
        movementSpeed = starRoute_BaseShip.ship_MovementSpeed / 10;
        rotationSpeed = starRoute_BaseShip.ship_RotationSpeed;
        aIPath.maxSpeed = movementSpeed;
        aIPath.rotationSpeed = 360 * rotationSpeed;
        Debug.Log(starRoute_BaseShip.ship_MovementSpeed + "  " + movementSpeed + "  " + aIPath.maxSpeed);
    }


    private void Update()
    {
        //Debug.Log(transform.name + "   Target   ==" + starRoute_BaseShip.Target.name);
        //Debug.Log(transform.name + " SearchRadius =" + starRoute_BaseShip.ship_SearchRadius + " attackRange =" + starRoute_BaseShip.ship_AttackRange);


        if (starRoute_BaseShip.Target != null)
        {
            Nearest_Target = starRoute_BaseShip.Target;
            switch (currentState)
            {
                case AIState.Idle:
                    Idle_State();
                    break;
                case AIState.Searching:
                    Searching_State();
                    break;
                case AIState.Attacking:
                    Attacking_State();
                    break;
                case AIState.Chasing:
                    Chasing_State();
                    break;
                case AIState.Retreating:
                    Retreating_State();
                    break;
                case AIState.UnitControl:
                    UnitControl_State();
                    break;
                default:
                    break;
            }
        }
        if (Nearest_Target == null)
        {
            //Debug.Log("Nearest_Target==null" + this.transform.name);
            currentState = AIState.Idle;
            aIDestinationSetter.target = null; // 如果目標不存在，則將目標設置為空
        }
        if (Unit_Selections.instance.Unit_Selected_List.Contains(this.gameObject))
        {
            currentState = AIState.UnitControl;
        }
        Debug.DrawLine(transform.position, WayPoint.position, Color.white);

        timer += Time.deltaTime; // 每幀增加計時器
    }
    private void UnitControl_State()
    {
        if (!Unit_Selections.instance.Unit_Selected_List.Contains(this.gameObject))
        {
            currentState = AIState.Idle;
        }
    }
    private void Idle_State()
    {

        // 在此状态下，敌人等待或巡逻，如果玩家进入检测范围，则切换到搜索状态

        //color2  AIState.Idle ==> AIState.Searching
        if (Vector3.Distance(transform.position, Nearest_Target.transform.position) <= starRoute_BaseShip.ship_SearchRadius)
        {
            currentState = AIState.Searching;
        }
        //! Idle_State  巡逻系統邏輯 
        aIPath.maxSpeed = movementSpeed; //回歸原本速度
    }

    private void Searching_State()
    {
        // 在此状态下，移動到目標物，如果在攻击范围内，则切换到攻击状态，否则继续搜索

        //color2  AIState.Searching ==> AIState.Attacking
        if (Vector3.Distance(transform.position, Nearest_Target.transform.position) <= starRoute_BaseShip.ship_AttackRange)
        {

            currentState = AIState.Attacking;
        }
        //! Searching_State  搜索 系統邏輯 
        MoveTo(Nearest_Target.transform.position);

    }
    private void Attacking_State()
    {


        // 在此状态下，敌人攻击玩家，如果玩家脱离攻击范围，则切换回追逐状态
        // 如果玩家脱离攻击范围，将 currentState 设置为 EnemyState.Chasing
        //color2  AIState.attacking ==> AIState.chasing
        if (Vector3.Distance(transform.position, Nearest_Target.transform.position) > starRoute_BaseShip.ship_AttackRange)
        {
            currentState = AIState.Chasing;
        }

        // 在此状态下，敌人攻击玩家，如果玩家Hull低於25%數值有機率，则切换到撤退状态
        //color2  AIState.attacking ==> AIState.retreating
        if (starRoute_BaseShip.ship_Hull / starRoute_BaseShip.ship_ScriptObjects.Ship_Hull <= 0.25f)
        {
            currentState = AIState.Retreating;
        }

        //! Attacking_State 攻擊State 系統邏輯
        if (timer >= starRoute_BaseShip.ship_ScriptObjects.Ship_Attack_Timer) // 檢查計時器是否超過攻擊間隔
        {
            Vector3 randomPosition = GetRandomPosition(Nearest_Target.transform.position, starRoute_BaseShip.ship_AttackRange);
            MoveTo(randomPosition);
            timer = 0f; // 重置計時器
        }
    }
    private void Chasing_State()
    {

        // 在此状态下，敌人追擊玩家，如果玩家进入攻击范围，则切换到攻击状态
        if (Vector3.Distance(transform.position, Nearest_Target.transform.position) <= starRoute_BaseShip.ship_AttackRange)
        {
            currentState = AIState.Attacking;
        }
        // 在此状态下，敌人追擊玩家，如果玩家进入攻击范围，则切换到攻击状态
        if (Vector3.Distance(transform.position, Nearest_Target.transform.position) >= starRoute_BaseShip.ship_SearchRadius)
        {
            currentState = AIState.Idle;
        }

        //追擊系統 
        MoveTo(Nearest_Target.transform.position);
    }

    private void Retreating_State()
    {

        // 在此状态下，遠離攻击范围，则切换到 Idle 状态
        if (Vector3.Distance(transform.position, Nearest_Target.transform.position) >= starRoute_BaseShip.ship_SearchRadius)
        {
            currentState = AIState.Idle;
        }

        //color1 Retreating_State 撤退系統 State 系統邏輯
        if (timer >= 4f)
        {
            currentState = AIState.Idle;
            timer = 0f;
        }
        Vector3 direction = (Nearest_Target.transform.position - transform.position).normalized;
        Vector3 newPosition = transform.position - (direction * 2f);
        //aIPath.maxSpeed = movementSpeed * 1.25f;  //原本速度的 1.25倍
        WayPoint.position = newPosition;

    }

    private void MoveTo(Vector3 pos)
    {
        // 移動到目標物
        WayPoint.position = pos;
        aIDestinationSetter.target = WayPoint;
    }

    public Vector3 GetRandomPosition(Vector3 center, float radius)
    {



        // 计算X和Y的偏移量
        float offsetX = Random.Range(-radius, radius);
        float offsetY = Random.Range(-radius, radius);

        // 计算X和Y坐标
        float x = center.x + offsetX;
        float y = center.y + offsetY;

        // 返回位置
        return new Vector3(x, y, center.z);
    }




    private void OnDrawGizmos()
    {
        //画出射程范围 綠色
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, starRoute_BaseShip.ship_SearchRadius);

        Gizmos.color = Color.blue;
        //画出攻击范围 藍色
        Gizmos.DrawWireSphere(transform.position, starRoute_BaseShip.ship_AttackRange);

        // 画出AI_Target线段 黄色
        Gizmos.color = Color.yellow;
        //Gizmos.DrawLine(transform.position, AI_Target.position);

    }



}
