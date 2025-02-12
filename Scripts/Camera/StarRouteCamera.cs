using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Sirenix.OdinInspector;

public class StarRouteCamera : MonoBehaviour
{

    [Title("StarRouteCamera")]
    
    [LabelText("StarRouteCamera 最小 縮放範圍")] public float MinZoomSizeLimit;
    [LabelText("StarRouteCamera 最大 縮放範圍")] public float MaxZoomSizeLimit;
    [LabelText("StarRouteCamera 縮放速度")] public float ZoomSpeed;
   
    protected virtual void LateUpdate()
    {
       
    }

    protected virtual void CameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float ZoomSize = scroll * ZoomSpeed;

        // 計算系數
        float factor = Mathf.Clamp01(Mathf.Abs(scroll) / 0.1f);

        // 限制相機的Size在MinZoomSizeLimit和MaxZoomSizeLimit之間
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - ZoomSize * factor, MinZoomSizeLimit, MaxZoomSizeLimit);
    }
}
