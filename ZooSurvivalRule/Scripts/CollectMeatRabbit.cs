using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMeatRabbit : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(Input.GetKey(KeyCode.F))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
