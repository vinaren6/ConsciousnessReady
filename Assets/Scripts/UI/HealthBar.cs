using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    Health hp = null;

    private void Start()
    {
        if (hp == null) {
            hp = PlayerMovement.playerObject.GetComponent<Health>();
        }
    }

    private void FixedUpdate()
    {
        transform.localScale = new Vector2(hp.GetHealth / hp.MaxHealth, 1f);
    }
}
