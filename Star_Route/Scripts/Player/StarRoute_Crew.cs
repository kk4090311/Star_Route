using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

public class StarRoute_Crew : MonoBehaviour
{
    [Title("StarRoute_Crew 挖礦人員設定")]
    [LabelText("挖礦人員設定-移動速度")] public float MoveSpeed;
    [LabelText("挖礦人員設定-旋轉速度")] public float RotateSpeed;

    [LabelText("挖礦人員設定-挖礦速度")] public float MiningTime;
    [LabelText("挖礦人員設定-挖礦數量")] public float MiningAmount;
    [LabelText("挖礦人員設定-目標礦物")] public GameObject TargetResource;
    [LabelText("挖礦人員設定-指示線")] public LineRenderer LaserBeam;



    private Transform player;

    private void Start()
    {
        // 查找Player的Transform
        player = SysUtils.Get_Player_GameObject().transform;
        LaserBeam = transform.Find("挖礦光束").GetComponent<LineRenderer>();
    }


    // 1.外部呼叫挖礦協程
    public void Crew_Mining_Coroutine(Transform resource)
    {
        TargetResource = resource.gameObject;
        // 開始移動到目標礦物位置
        StartCoroutine(MoveTo(resource, OnCrew_MoveToResource));
        StartCoroutine(RotateTo(resource));
    }

    // 2.挖礦協程移動完成 啟動移動協程到玩家
    private void OnCrew_MoveToResource()
    {
        Debug.Log("Mining Complete");

        StartCoroutine(Mining_Process());

        // StartCoroutine(SysUtils.DelaySeconds(2f, () =>
        // {
        //     Debug.Log("delay 2f");
        //     TargetResource.transform.SetParent(this.gameObject.transform);
        //     // 開始移動到Player位置
        //     StartCoroutine(MoveTo(player, OnCrew_MoveToReturn));
        //     StartCoroutine(RotateTo(player));
        // }));
    }
    private IEnumerator Mining_Process()
    {
        float elapsedTime = 0f;
        float duration = MiningTime; // 总共持续时间


        while (elapsedTime < duration)
        {

            // 获取目标位置
            Vector3 targetPosition = TargetResource.transform.position;

            // 在 x 和 y 轴上分别偏移一个随机值
            targetPosition.x += UnityEngine.Random.Range(-0.5f, +0.5f);
            targetPosition.y += UnityEngine.Random.Range(-0.5f, +0.5f);

            // 设置 LaserBeam 的第二个点的位置
            LaserBeam.enabled = true;
            LaserBeam.SetPosition(0, transform.position);
            LaserBeam.SetPosition(1, targetPosition);

            // 每帧增加已经过去的时间
            elapsedTime += Time.deltaTime;

            // 等待一帧
            yield return null;
        }
        LaserBeam.enabled = false;
        TargetResource.transform.SetParent(this.gameObject.transform);
        StartCoroutine(MoveTo(player, OnCrew_MoveToReturn));
        StartCoroutine(RotateTo(player));
    }

    // 3.移動協程到玩家後 回調
    private void OnCrew_MoveToReturn()
    {
        Debug.Log("Crew Move Complete");

        // 摧毀目標礦物
        Destroy(TargetResource);

        // 摧毀挖礦人員
        Destroy(gameObject);

        // 增加Player的Crews和Resource計數
        StarRouteGameManager.Instance.player_ScriptObjects.Player_Crews++;
        StarRouteGameManager.Instance.player_ScriptObjects.Player_Resource++;
    }


    // 移動協程
    private IEnumerator MoveTo(Transform destination, System.Action onComplete)
    {
        while (Vector3.Distance(transform.position, destination.position) > 0.1f)
        {
            // 將位置逐漸移向目的地
            transform.position = Vector3.MoveTowards(transform.position, destination.position, MoveSpeed * Time.deltaTime);


            yield return null;
        }

        // 移動完成後執行回調
        onComplete?.Invoke();
    }

    private IEnumerator RotateTo(Transform destination)
    {
        // 在一定时间内平滑旋转到目标角度
        float elapsedTime = 0f;
        float rotationTime = RotateSpeed; // 旋转所需时间，可以根据需要调整
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < rotationTime)
        {
            // 获取当前位置到目标位置的角度
            float angle = SysUtils.Get_Angle(transform.position, destination.position);

            // 计算旋转的目标方向
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle + 90);

            // 在一定时间内平滑旋转到目标角度
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }

}