using System.Collections;
using UnityEngine;
using DG.Tweening;

public class AbilityHook : MonoBehaviour
{

    [SerializeField]
    private float hookSpeed = 1f;

    [SerializeField]
    private float hookLength = 1.5f;

    [SerializeField]
    private Ease easingType;

    [SerializeField]
    private GameObject hookGameObject;

    private Hook hook;

    private InputActions inputActions;
    private bool hookMoving = false;
    Tween hookAnimation;


    private void Awake()
    {
        inputActions = new InputActions();

        inputActions.Player.NeedleHook.performed +=
        context =>
        {
            if (!hookMoving)
                StartCoroutine("ExtendHook");
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

    IEnumerator ExtendHook()
    {
        hookMoving = true;

        hookAnimation =
            hookGameObject
            .transform.DOScaleY(hookLength, hookSpeed)
            .SetEase(easingType);

        yield return hookAnimation.WaitForCompletion();

        hookMoving = false;
    }


}
