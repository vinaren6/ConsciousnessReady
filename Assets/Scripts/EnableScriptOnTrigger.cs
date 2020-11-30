using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableScriptOnTrigger : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour script;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        script.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        script.enabled = false;
    }
}
