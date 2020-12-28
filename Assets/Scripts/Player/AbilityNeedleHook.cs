using System.Collections;
using UnityEngine;
using DG.Tweening;

public class AbilityNeedleHook : MonoBehaviour
{

    [SerializeField]
    private float needleTime = 1f;

    [SerializeField]
    private float needleLength = 1.5f;

    [SerializeField]
    private Ease easingType;

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

        needleHook
            .transform.DOScaleY(needleLength, needleTime)
            .SetEase(easingType)
            .SetLoops(2, LoopType.Yoyo);
        yield return null;

    }


}
