using UnityEngine;

public class Death : MonoBehaviour
{

    [SerializeField] GameObject[] enable;
    [SerializeField] GameObject[] desable;

    private void OnDestroy()
    {
        foreach (GameObject obj in enable) {
            obj.SetActive(true);
        }
        foreach (GameObject obj in desable) {
            obj.SetActive(false);
        }
    }
}
