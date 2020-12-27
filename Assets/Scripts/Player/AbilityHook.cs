using UnityEngine;

public class AbilityHook : MonoBehaviour
{

    [SerializeField]
    private Transform pointOfFire;

    [SerializeField]
    private float knockback = 0.15f;

    [SerializeField]
    private float coolDown = 0.2f;

    static private Hook hook;


    private InputActions inputActions;
    private float elapsedTime = 0f;

    private void Awake()
    {
        inputActions = new InputActions();

        inputActions.Player.Hook.performed +=
        context =>
        {
            Hook();
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


    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    public void Hook()
    {
        if (elapsedTime > coolDown)
        {

            if (knockback != 0)
            {
                GetComponent<Rigidbody2D>().AddForce(transform.up * -knockback, ForceMode2D.Impulse);
            }

            elapsedTime = 0f;
        }
    }


}
