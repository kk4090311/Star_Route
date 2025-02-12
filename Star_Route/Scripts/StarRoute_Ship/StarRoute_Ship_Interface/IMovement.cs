using System.Collections;
using System.Collections.Generic;
using StarRouteSettings;
using Sirenix.OdinInspector;
using UnityEngine;


/// <summary>
/// 定義可移動物件的介面。
/// </summary>
public interface IMovement
{
    /// <summary>
    /// 根據水平和垂直輸入移動物件。
    /// </summary>
    /// <param name="horizontalInput">水平輸入值。</param>
    /// <param name="verticalInput">垂直輸入值。</param>
    [Title("IMovement 介面實作 - 可移動")]
    void Move(float horizontalInput, float verticalInput);

    /// <summary>
    /// 根據水平輸入旋轉物件。
    /// </summary>
    /// <param name="horizontalInput">水平輸入值。</param>
    void Rotate(float horizontalInput);

    /// <summary>
    /// 施加推力到物件。
    /// </summary>
    /// <param name="thrustInput">推力輸入值。</param>
    void Thrust(float horizontalInput, float verticalInput);
}