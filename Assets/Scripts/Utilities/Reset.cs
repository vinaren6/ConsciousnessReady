using UnityEngine;
using UnityEngine.InputSystem;
public class Reset : MonoBehaviour
{
    [SerializeField] private InputAction reset;

    private Vector3 pos;

    void Start()
    {
        reset.Enable();
        reset.performed += context => ResetPos();

        pos = PlayerMovement.playerObj.transform.position;
    }
    private void ResetPos()
    {
        PlayerMovement.playerObj.transform.position = pos;
    }
}
