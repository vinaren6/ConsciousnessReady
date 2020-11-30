using UnityEngine;

public class EnableScriptOnTrigger : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour script;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            script.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            script.enabled = false;
    }
}
