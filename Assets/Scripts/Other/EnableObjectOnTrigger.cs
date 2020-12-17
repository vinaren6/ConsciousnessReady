using UnityEngine;

public class EnableObjectOnTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private bool defaultState;

    private void Start()
    {
        target.SetActive(defaultState);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        target.SetActive(!defaultState);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        target.SetActive(defaultState);
    }
}
