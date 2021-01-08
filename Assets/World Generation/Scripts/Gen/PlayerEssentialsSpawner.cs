using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEssentialsSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerEssentials;
    static bool haveSpaned = false;
    bool isActive = false;
    void Start()
    {
        if (!haveSpaned) {
            haveSpaned = true;
            isActive = true;
            StartCoroutine(Wait());
        }
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(.1f);
        Instantiate(playerEssentials, transform);
        Vector3 pos = transform.position;
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneAt(0));
        transform.position = pos;
    }
    private void OnDestroy()
    {
        if (isActive)
            haveSpaned = false;
    }
}
