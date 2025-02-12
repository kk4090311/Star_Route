using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

[RequireComponent(typeof(Seeker), typeof(Unit))]
public class Unit_Movement : MonoBehaviour
{
    [Title("Unit_Movement 移動腳本")]
    [LabelText("Unit WayPoint目標點物件")] public GameObject _TargetWayPoint;

    [LabelText("A* 目標物件 - lineRenderer")] public LineRenderer lineRenderer;

    [LabelText("A* 目標物件 - Seeker")] public Seeker seeker;
    // ////////////////////////////////////////////////


    [Title("A* 組件")]
    [LabelText("AIDestinationSetter")] public AIDestinationSetter _destinationSetter;
    [LabelText("AIPath")] public AIPath _aiPath;

    private void Start()
    {
        _destinationSetter = gameObject.GetComponent<AIDestinationSetter>();
        _aiPath = gameObject.GetComponent<AIPath>();
        seeker = gameObject.GetComponent<Seeker>();
        if (_TargetWayPoint == null)
        {
            _TargetWayPoint = Instantiate(Resources.Load<GameObject>("Prefabs/MISC/_TargetWayPoint-Prefab"), transform.position, Quaternion.identity, GameObject.Find("TargetWayPoint_List").transform);
            _TargetWayPoint.name = transform.name + " _TargetWayPoint";
            lineRenderer = _TargetWayPoint.GetComponent<LineRenderer>();
            lineRenderer.startWidth = 0.030f;
            lineRenderer.endWidth = 0.030f;
            _TargetWayPoint.SetActive(false);
        }

    }
    private void Update()
    {

        if (lineRenderer)
        {
            if (seeker.lastCompletedVectorPath != null)
            {
                lineRenderer.positionCount = seeker.lastCompletedVectorPath.Count;

                for (int i = 0; i < seeker.lastCompletedVectorPath.Count; i++)
                {
                    lineRenderer.SetPosition(i, seeker.lastCompletedVectorPath[i]);
                }
            }


            // for (int i = 0; i < seeker.lastCompletedVectorPath.Count; i++)
            // {
            //     lineRenderer.SetPosition(i, seeker.lastCompletedVectorPath[i]);
            // }
        }
        // if (lastCompletedVectorPath != null)
        // {
        //     for (int i = 0; i < lastCompletedVectorPath.Count - 1; i++)
        //     {
        //         Gizmos.DrawLine(lastCompletedVectorPath[i], lastCompletedVectorPath[i + 1]);
        //     }
        // }
    }
    public void SetDestinationTarget(Vector3 pos)
    {

        if (transform.GetComponent<StarRoute_Ship>().PlayerControl == false)
        {
            if (_TargetWayPoint == null)
            {
                _TargetWayPoint = Instantiate(Resources.Load<GameObject>("Prefabs/MISC/_TargetWayPoint-Prefab"), transform.position, Quaternion.identity, GameObject.Find("TargetWayPoint_List").transform);
                _TargetWayPoint.name = transform.name + " _TargetWayPoint";
                lineRenderer = _TargetWayPoint.GetComponent<LineRenderer>();

            }
            lineRenderer.SetPosition(1, transform.position);
            lineRenderer.SetPosition(0, _TargetWayPoint.transform.position);
            _TargetWayPoint.transform.position = pos;
            _destinationSetter.target = _TargetWayPoint.transform;
        }

    }

}