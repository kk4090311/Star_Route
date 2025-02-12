using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarRouteSettings;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "StarRoute", menuName = "StarRoute/Turret_ScriptObjects")]
public class Turret_ScriptObjects : ScriptableObject
{

    public WeaponType weaponType;

    public string Turret_Name;
    public bool AutoAttack = false;

    public GameObject Bullet_Prefabs;

    //--------------"砲台射擊"------------------//


    [BoxGroup("砲台射擊")]
    public float Turn_Speed = 1.0f;
    [BoxGroup("砲台射擊")]
    public float FireRange = 2.0f;
    [BoxGroup("砲台射擊")]
    public float FireRate = 1.0f;
    [BoxGroup("砲台射擊")]
    public float BulletSpeed = 3.0f;

    [BoxGroup("砲台射擊")]
    public float MaxFireAngle = 45.0f;
    [BoxGroup("砲台射擊")]
    [ColorPalette("射擊輔助線")]
    public Color Auxiliary_Line_Color;


    //--------------"砲台射擊"------------------//

}
