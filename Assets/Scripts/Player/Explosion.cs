using UnityEngine;

public class Explosion : MonoBehaviour
{

    [SerializeField]
    private float SelfDestroyTimer = 1f;

    void Start()
    {
        Destroy(gameObject, SelfDestroyTimer);
    }

}
