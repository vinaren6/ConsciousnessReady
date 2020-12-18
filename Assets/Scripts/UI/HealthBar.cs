using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    Health hp;

    private void FixedUpdate()
    {
        transform.localScale = new Vector2(hp.PlayerHealth / hp.MaxHealth, 1f);
    }
}
