using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;



public class DrawRangeLine : ImmediateModeShapeDrawer
{
   
    
    
    
    private float radius_index;
    private Base_Turret turret;
    private void Awake()
    {


    }


    public override void DrawShapes(Camera cam)
    {

        using (Draw.Command(cam))
        {
            //! 需要重寫
            // set up static parameters. these are used for all following Draw.Line calls


            // set static parameter to draw in the local space of this object

            // Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, turret.MaxFireAngle) * transform.root.up * turret.FireRange, Color.green);
            // Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, -turret.MaxFireAngle) * transform.root.up * turret.FireRange, Color.green);
            turret = GetComponent<Base_Turret>();
            radius_index = turret.turret_ScriptObjects.FireRange;
            Draw.Radius = 5f;
            
            Draw.LineGeometry = LineGeometry.Volumetric3D;
            Draw.DiscGeometry = DiscGeometry.Flat2D;
            Draw.ThicknessSpace = ThicknessSpace.Pixels;
            Draw.Thickness = 1.5f; // 4px wide
            Draw.UseDashes = true;
            Draw.DashSpace = DashSpace.FixedCount;
            Draw.DashSnap = DashSnapping.EndToEnd;
            Draw.DashSize = 10f;
            Draw.DashSpacing = 0.5f;
            Draw.Color = turret.turret_ScriptObjects.Auxiliary_Line_Color;
            Draw.DashType = DashType.Angled;
            
            Draw.DashShapeModifier = -1.0f;


            Draw.Line(turret.transform.position, Quaternion.Euler(0, 0, turret.turret_ScriptObjects.MaxFireAngle) * transform.parent.up * turret.turret_ScriptObjects.FireRange + turret.transform.position);
            Draw.Line(turret.transform.position, Quaternion.Euler(0, 0, -turret.turret_ScriptObjects.MaxFireAngle) * transform.parent.up * turret.turret_ScriptObjects.FireRange + turret.transform.position);

            for (float i = 1 % turret.turret_ScriptObjects.FireRange; i <= turret.turret_ScriptObjects.FireRange; i++)
            {

                Draw.Radius = i;
                Draw.Arc(turret.transform.position, (90 - turret.turret_ScriptObjects.MaxFireAngle + transform.parent.eulerAngles.z) * Mathf.Deg2Rad, (turret.turret_ScriptObjects.MaxFireAngle + transform.parent.eulerAngles.z + 90) * Mathf.Deg2Rad);
            }


        }

    }

}