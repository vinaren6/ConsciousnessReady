using UnityEngine;

public class EnableScriptOnTrigger : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour[] scripts;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (MonoBehaviour script in scripts) {
            if (collision.tag == "Player")
                script.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (MonoBehaviour script in scripts) {
            if (collision.tag == "Player")
                script.enabled = false;
        }
    }
}
