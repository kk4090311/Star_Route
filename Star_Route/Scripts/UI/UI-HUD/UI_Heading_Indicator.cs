using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Heading_Indicator : MonoBehaviour
{

    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(0, 0, Player.transform.rotation.eulerAngles.z);
    }
}
