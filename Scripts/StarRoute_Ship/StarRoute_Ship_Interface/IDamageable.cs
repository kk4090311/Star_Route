using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using StarRouteSettings;
public interface IDamageable
{
    [Title("IDamageable接口_實現可傷害")]
    void Get_Damage(WeaponType weaponType);

}
