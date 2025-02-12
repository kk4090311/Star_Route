using System.Collections;
using System.Collections.Generic;
using StarRouteSettings;
using Sirenix.OdinInspector;
using UnityEngine;


/// <summary>
/// 定義可保存和可讀取的物件的介面。
/// </summary>
public interface ISaveLoadable
{
    /// <summary>
    /// 執行保存操作。
    /// </summary>
    [Title("ISaveLoadable Interface 實作 - 可保存，可讀取")]
    void Save();

    /// <summary>
    /// 執行讀取操作。
    /// </summary>
    void Load();
}