using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToOrbit;

    [SerializeField]
    private ParticleSystem particleSystem;

    [SerializeField]
    private float speed;

    private bool hasBeenMoved;

    private void Start()
    {
       // particleSystem.Emit(200);

    }


    void Update()
    {
        transform.RotateAround(objectToOrbit.transform.position, new Vector3(0f, 0f, 1f), speed * Time.deltaTime);
        //particleSystem.playOnAwake;
        particleSystem.Emit(200);
    }


}