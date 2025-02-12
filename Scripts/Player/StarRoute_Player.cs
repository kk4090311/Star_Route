using System.Collections;
using System.Collections.Generic;
using Shapes;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using StarRouteSettings;

using System.Linq;
using Unity.Burst.Intrinsics;
public class StarRoute_Player : MonoBehaviour, IMovement
{
    [Title("StarRoute玩家")]

    [LabelText("玩家初始化數據放置ScriptObjects")] public Player_ScriptObjects player_ScriptObjects;
    [LabelText("玩家船艦數據")] public List<GameObject> Player_Ship_List;
    [LabelText("玩家資源礦物在搜索範圍內")] public List<GameObject> Resource_InRange;


    [Title("StarRoute玩家內部參數")]


    [LabelText("玩家Rigidbody")] private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }




    // Update is called once per frame
    private void FixedUpdate()
    {

        Resource_Mining();
        StarRoute_Player_IMovement();
        StarRoute_Enemy_Detection();
    }

    private void StarRoute_Player_IMovement()
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

    private void Resource_Mining()
    {
        Resource_InRange = Ship_Utils.Find_Tag_GameObject_InRange(this.transform, player_ScriptObjects.Player_SearchRadius, SysUtils.Resource_Tag);
        if (Resource_InRange != null)
        {

            foreach (var resource in Resource_InRange)
            {
                if (!resource.GetComponent<StarRoute_Resource>().IsMined && player_ScriptObjects.Player_Crews > 0)
                {
                    resource.GetComponent<StarRoute_Resource>().IsMined = true;
                    GameObject crew = Instantiate(player_ScriptObjects.Player_CrewPrefab, this.transform.position, Quaternion.identity);
                    crew.GetComponent<StarRoute_Crew>().Crew_Mining_Coroutine(resource.transform);
                    player_ScriptObjects.Player_Crews--;
                }
            }
        }
        Debug.Log(" player_ScriptObjects.Player_Crews" + player_ScriptObjects.Player_Crews);
    }

    private void StarRoute_Enemy_Detection()
    {
        // Color2------------StarRouteGameManager.Instance.enemy_In_Maps----------------//
        StarRouteGameManager.Instance.enemy_In_Maps = SysUtils.FindGameObjects_WithTag(SysUtils.Enemy_Tag);
        List<GameObject> enemy_In_Maps = StarRouteGameManager.Instance.enemy_In_Maps;

        bool isEnemyNearby = false; // 標記是否附近有敵人

        foreach (var enemy in enemy_In_Maps)
        {
            if (Vector2.Distance(this.transform.position, enemy.transform.position) <= 10f)
            {
                StarRouteGameManager.Instance.enemy_ScriptableObjects = enemy.GetComponent<StarRoute_Enemy>().enemy_ScriptableObject;
                isEnemyNearby = true; // 附近有敵人
            }
        }

        // 根據附近是否有敵人來設置 UI_EnterBattle 的顯示狀態
        StarRouteGameManager.Instance.UI_EnterBattle.gameObject.SetActive(isEnemyNearby);


    }







    //color2------------IMovement接口實現----------------//
    public void Move(float horizontalInput, float verticalInput)
    {
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * (player_ScriptObjects.Player_MoveSpeed / 2);
        rb.AddRelativeForce(movement, ForceMode2D.Force);
    }
    public void Rotate(float horizontalInput)
    {
        rb.rotation -= horizontalInput * player_ScriptObjects.Player_RotationSpeed;
    }
    public void Thrust(float horizontalInput, float verticalInput)
    {
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * (player_ScriptObjects.Player_MoveSpeed / 2);
        rb.AddRelativeForce(movement * (player_ScriptObjects.Player_ThrustingSpeed - 1), ForceMode2D.Force);
    }


}
