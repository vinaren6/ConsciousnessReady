using UnityEngine;
public class RestPos : MonoBehaviour
{
    [SerializeField] private Vector3 pos = new Vector3(0, 0, -10);
    private void OnEnable()
    {
        transform.position = pos;
    }
}
