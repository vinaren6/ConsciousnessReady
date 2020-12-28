using System.Collections;
using UnityEngine;

public class AbilityNeedleHook : MonoBehaviour
{

    [SerializeField]
    private float needleSpeed = 5f;

    [SerializeField]
    private float needleLength = 1.5f;

    [SerializeField]
    private GameObject needleHook;

    private InputActions inputActions;


    private void Awake()
    {
        inputActions = new InputActions();

        inputActions.Player.NeedleHook.performed +=
        context =>
        {
            StartCoroutine("Pierce");
        };
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }


    IEnumerator Pierce()
    {
        needleHook.SetActive(true);

        while (needleHook.transform.localScale.y < needleLength)
        {
            needleHook.transform.localScale += new Vector3(0, needleSpeed * Time.deltaTime, 0);
            yield return null;
        }

        while (needleHook.transform.localScale.y >= 0)
        {
            needleHook.transform.localScale -= new Vector3(0, needleSpeed * Time.deltaTime, 0);
            yield return null;
        }

    }


}
