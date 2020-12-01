using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rippler : MonoBehaviour
{
    [SerializeField]
    Material material;

    [SerializeField]
    int numPosCount = 16;

    [SerializeField]
    float hitSize = 0.05f;

    LineRenderer lineRenderer;

    float size = 1;

    void Start()
    {
        lineRenderer = NewLine();
    }

    LineRenderer NewLine()
    {
        LineRenderer lineRenderer;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.material = material;
        lineRenderer.alignment = LineAlignment.TransformZ;
        lineRenderer.loop = true;
        lineRenderer.widthMultiplier = 0.1f;
        //lineRenderer.numCornerVertices = 1;

        lineRenderer.positionCount = numPosCount;
        //lineRenderer.SetPositions(new Vector3[] { new Vector3(1,0), new Vector3(0, 1), new Vector3(-1, 0), new Vector3(0, -1) });
        Line(lineRenderer, size);
        return lineRenderer;
    }

    void Line(LineRenderer lineRenderer, float distMutliplier)
    {
        lineRenderer.positionCount = numPosCount;
        Vector3[] poss = new Vector3[lineRenderer.positionCount];
        for (int i = 0; i < lineRenderer.positionCount; i++) {
            float ang = (float)i / numPosCount * Mathf.PI * 2f;
            poss[i] = new Vector3(Mathf.Sin(ang) * distMutliplier, Mathf.Cos(ang) * distMutliplier);
        }
        lineRenderer.SetPositions(poss);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 hit = collision.ClosestPoint(transform.position);
        hit -= pos;
        float angle = 360f - Mathf.Atan2(hit.y, hit.x) * Mathf.Rad2Deg;
        angle += 90f;
        angle %= 360f;
        angle /= 360f;

        Hole(angle);
    }

    void Hole(float angle)
    {
        Gradient g = new Gradient();
        g.mode = GradientMode.Fixed;
        if (lineRenderer.colorGradient.alphaKeys.Length == 2) {
            GradientAlphaKey[] keys = new GradientAlphaKey[] {
                new GradientAlphaKey(1, (angle - hitSize) % 1f),
                new GradientAlphaKey(0, (angle + hitSize) % 1f),
                new GradientAlphaKey(1, 1)
            };
            g.SetKeys(lineRenderer.colorGradient.colorKeys, keys);
        } else {

            List<GradientAlphaKey> keys = new List<GradientAlphaKey>(lineRenderer.colorGradient.alphaKeys);
            keys.Add(new GradientAlphaKey(1, (angle - hitSize) % 1f));
            keys.Add(new GradientAlphaKey(0, (angle + hitSize) % 1f));
            g.SetKeys(lineRenderer.colorGradient.colorKeys, keys.ToArray());
        }
        lineRenderer.colorGradient = g;
    }
}
