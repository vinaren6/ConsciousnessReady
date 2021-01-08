using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEssentialsSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerEssentials;
    static bool haveSpaned = false;
    void Start()
    {
        if (!haveSpaned) {
            haveSpaned = true;
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
        Destroy(this);
    }
}
