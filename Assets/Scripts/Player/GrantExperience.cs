using UnityEngine;

public class GrantExperience : MonoBehaviour
{
    [SerializeField]
    private int experiance = 1;
    [SerializeField]
    private int permanentExperiance = 0;

    private bool haveBeenTrigered = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!haveBeenTrigered && collision.transform.CompareTag("Player")) {
            ExperiancePoints.Experiance += experiance;
            ExperiancePoints.PermanentExperiance += permanentExperiance;
            haveBeenTrigered = true;
            enabled = false;
        }
    }

}
