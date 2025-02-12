using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Sirenix.OdinInspector;

public class StarRouteCamera_Game : StarRouteCamera, IMovement
{


    [Title("StarRouteCamera_Game")]
    [LabelText("RTS-Info-Panel Camera 目標船艦")] public Transform target;
    [LabelText("horizontal")] public float horizontal;
    [LabelText("vertical")] public float vertical;


    protected override void LateUpdate()
    {


        if (target != null)
        {
            StarRouteCamera_Game_LookAt();
        }
        else
        {
            Move_Camera();
        }

        CameraZoom();
    }
    private void Move_Camera()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Move(horizontal, vertical);
    }

    public void StarRouteCamera_Game_LookAt()
    {
        Camera.main.transform.position = target.transform.position;
    }

    //color2------------IMovement接口實現----------------//
    public void Move(float horizontalInput, float verticalInput)
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(horizontalInput, verticalInput, 0), Time.deltaTime * 20);

    }

    public void Rotate(float horizontalInput)
    {
        throw new System.NotImplementedException();
    }

    public void Thrust(float horizontalInput, float verticalInput)
    {
        throw new System.NotImplementedException();
    }

}
