using UnityEngine;

public class UI_NodeSelect : MonoBehaviour
{

    private Turret_Node turret_Node;

    public void UI_position(Turret_Node _turret_Node)
    {
        turret_Node = _turret_Node;
        //transform.position = Camera.main.WorldToScreenPoint(_turret_Node.transform.position);

        gameObject.SetActive(true);
    }
    public void UI_Hide()
    {
        gameObject.SetActive(false);
    }
   
}
