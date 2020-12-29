using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhantom : MonoBehaviour
{
    [SerializeField]
    private GameObject player;


    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }
}
