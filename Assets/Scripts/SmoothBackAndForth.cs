using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothBackAndForth : MonoBehaviour
{
    public Vector3 start, end;
    public float time;
    float t;

    private void FixedUpdate()
    {
        t += Mathf.PI * 2 * Time.fixedDeltaTime / time;
        float v1 = (1f + Mathf.Sin(t)) / 2f;
        float v2 = (1f - Mathf.Sin(t)) / 2f;

        transform.position = start * v1 + end * v2;

    }

}
