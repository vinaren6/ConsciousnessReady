using UnityEngine;

public class NeedleHook : MonoBehaviour
{

    [HideInInspector]
    public bool struckAnObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        struckAnObject = true;

        other.transform.SetParent(transform);
    }


}
