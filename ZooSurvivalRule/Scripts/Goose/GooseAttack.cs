using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseAttack : MonoBehaviour
{
    [SerializeField] private Animator gooseAni;
    [SerializeField] private Rigidbody2D gooseRig;
    [SerializeField] private float dashSpeed = 8f;
    private bool atk = false;

    private void Update()
    {
        if (atk)
        {
            gooseRig.velocity = new Vector2(dashSpeed, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gooseAni.SetBool("Attacking",true);
            atk = true;
        }
    }
}
