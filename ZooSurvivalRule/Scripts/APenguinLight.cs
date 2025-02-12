using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APenguinLight : MonoBehaviour
{
    [SerializeField] private GameObject light1;
    [SerializeField] private GameObject light2;

    private void Start()
    {
        light1.SetActive(false);
        light2.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            light1.SetActive(true);
            light2.SetActive(true);
        }
    }
}
