using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Drag : MonoBehaviour
{
    Camera cam;
    [SerializeField]
    RectTransform boxVisual;
    Rect selectionBox;
    Vector2 startPosition;
    Vector2 endPosition;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
        Draw_Visual();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            selectionBox = new Rect();
        }
        if (Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;
            Draw_Visual();
            Draw_Selection();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Selcet_Units();
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            Draw_Visual();
        }
    }

    private void Draw_Visual()
    {
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;
        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        boxVisual.position = boxCenter;
        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
        boxVisual.sizeDelta = boxSize;
    }
    private void Draw_Selection()
    {
        //! X axis
        if (Input.mousePosition.x < startPosition.x)
        {
            //drag from left
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = startPosition.x;
        }
        else
        {
            //drag from right
            selectionBox.xMin = startPosition.x;
            selectionBox.xMax = Input.mousePosition.x;
        }
        //! X axis
        //! Y axis
        if (Input.mousePosition.y < startPosition.y)
        {
            //drag from left
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = startPosition.y;
        }
        else
        {
            //drag from right
            selectionBox.yMin = startPosition.y;
            selectionBox.yMax = Input.mousePosition.y;
        }
        //! Y axis
    }
    private void Selcet_Units()
    {
        foreach (var unit in Unit_Selections.instance.Unit_List)
        {
            if (selectionBox.Contains(cam.WorldToScreenPoint(unit.transform.position)))
            {
                Unit_Selections.instance.Unit_Drag_Select(unit);
            }
        }
    }
}
