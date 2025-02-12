using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Sirenix.OdinInspector;

public class StarRouteCamera_Map : StarRouteCamera
{
    protected override void LateUpdate()
    {
        Camera.main.transform.position = SysUtils.Get_Player_GameObject().transform.position;
        CameraZoom();
    }
}
