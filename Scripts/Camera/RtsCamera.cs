using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RtsCamera : MonoBehaviour
{
    Vector3 PlayerPosition;
    public bool seton;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    public float ScrollSpeed = 5.0f;
    public float ScrollCameraByMouseOffset = 50.0f;
    public float ZoomSpeed = 5.0f;
    public float MinZoomSizeLimit = 1.0f;
    public float MaxZoomSizeLimit = 20.0f;
    public Vector2 MapScrollLimit;
    // Update is called once per frame
    private Vector3 Origin;
    private Vector3 Difference;
    private Vector3 ResetCamera;
    private bool drag = false;

    private void Awake()
    {
        ResetCamera = Camera.main.transform.position;
        Camera.main.gameObject.TryGetComponent<CinemachineBrain>(out var brain);
        if (brain == null)
        {
            brain = Camera.main.gameObject.AddComponent<CinemachineBrain>();
        }
        cinemachineVirtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();

    }

    private void LateUpdate()
    {
        {
            if (Input.GetMouseButton(2))
            {
                Difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
                if (drag == false)
                {
                    drag = true;
                    Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }

            }
            else
            {
                drag = false;
            }

            if (drag)
            {
                Camera.main.transform.position = Origin - Difference * 0.5f;
            }

            //Camera 跟隨模式

            // if (Input.GetKeyDown(KeyCode.O)||seton)
            // {
            //     seton=true;
            //     PlayerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            //     Camera.main.transform.position = PlayerPosition;
            // }

        }
        Vector3 CameraPos = Camera.main.transform.position;
        //CameraPos.Y 上下移動 //CameraPos.X 左右移動

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - ScrollCameraByMouseOffset)
        {

            CameraPos.y += ScrollSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= ScrollCameraByMouseOffset)
        {

            CameraPos.y -= ScrollSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - ScrollCameraByMouseOffset)

        {

            CameraPos.x += ScrollSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= ScrollCameraByMouseOffset)
        {

            CameraPos.x -= ScrollSpeed * Time.deltaTime;
        }

        // float scroll = Input.GetAxis("Mouse ScrollWheel");
        // float ZoomSize = scroll * ZoomSpeed;

        // //限制相機的Size在MinZoomSizeLimit和MaxZoomSizeLimit之間
        // cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(Camera.main.orthographicSize - ZoomSize, MinZoomSizeLimit, MaxZoomSizeLimit);
        //Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - ZoomSize, MinZoomSizeLimit, MaxZoomSizeLimit);

        
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            float ZoomSize = scroll * ZoomSpeed;

            // 計算系數
            float factor = Mathf.Clamp01(Mathf.Abs(scroll) / 0.1f);

            // 限制相機的Size在MinZoomSizeLimit和MaxZoomSizeLimit之間
            cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(cinemachineVirtualCamera.m_Lens.OrthographicSize - ZoomSize * factor, MinZoomSizeLimit, MaxZoomSizeLimit);
        
        //限制Camera 最大Y 為+-MapScrollLimit
        CameraPos.y = Mathf.Clamp(CameraPos.y, -MapScrollLimit.y, MapScrollLimit.y);
        //限制Camera 最大X 為+-MapScrollLimit
        CameraPos.x = Mathf.Clamp(CameraPos.x, -MapScrollLimit.x, MapScrollLimit.x);
        //設定最終Camera位置
        this.transform.position = CameraPos;
    }
}