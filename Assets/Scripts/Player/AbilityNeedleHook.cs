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
    private bool needleMoving = false;
    Tween needleAnimation;


    private void Awake()
    {
        inputActions = new InputActions();

        inputActions.Player.NeedleHook.performed +=
        context =>
        {
            if (!needleMoving)
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

    private void Update()
    {
        if (needleHook.GetComponent<NeedleHook>().struckAnObject)
        {
            needleAnimation.Rewind();
            needleMoving = false;
            needleHook.GetComponent<NeedleHook>().struckAnObject = false;
        }
    }


    IEnumerator Pierce()
    {
        needleMoving = true;

        needleAnimation =
            needleHook
            .transform.DOScaleY(needleLength, needleTime)
            .SetEase(easingType)
            .SetLoops(2, LoopType.Yoyo);

        yield return needleAnimation.WaitForCompletion();

        needleMoving = false;
    }


}
