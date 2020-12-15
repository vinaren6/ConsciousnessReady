using UnityEngine;
using UnityEngine.InputSystem;

public class WorldmapManager : MonoBehaviour
{
    [SerializeField]
    private InputAction toggleWorldmap;

    public GameObject[] enableObjs;
    public GameObject[] desableObjs;

    private bool toggle;

    public static WorldmapManager instace;

    private void OnEnable()
    {
        toggleWorldmap.Enable();
    }
    private void OnDisable()
    {
        toggleWorldmap.Disable();
    }
    private void OnDestroy()
    {
        if (enabled)
            instace = null;
    }

    void Awake()
    {

        if (instace != null) {
            Debug.LogWarning("Multible instances detected!");
            enabled = false;
            return;
        }
        instace = this;

        toggleWorldmap.performed += context =>
        {
            if (toggle) {
                Time.timeScale = 0;
                foreach (GameObject obj in enableObjs) {
                    obj.SetActive(true);
                }
                foreach (GameObject obj in desableObjs) {
                    obj.SetActive(false);
                }

            } else {
                Time.timeScale = 1;
                foreach (GameObject obj in enableObjs) {
                    obj.SetActive(false);
                }
                foreach (GameObject obj in desableObjs) {
                    obj.SetActive(true);
                }
            }
            toggle = !toggle;
        };
    }

}
