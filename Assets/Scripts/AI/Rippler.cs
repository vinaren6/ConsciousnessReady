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
    [SerializeField]
    float smoothSize = 0.01f;

    [SerializeField]
    float lineWidth = 0.075f;
    [SerializeField]
    float time = 15f;
    [SerializeField]
    float rippleSize = 5f;
    [SerializeField]
    float delay = 1f;


    LineRenderer lineRenderer;

    float size = 0;

    CircleCollider2D coll;

    void Start()
    {
        lineRenderer = NewLine();
        coll = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate()
    {

        size += Time.fixedDeltaTime / time;
        if (size < 1) {

            coll.radius = size * rippleSize;
            lineRenderer.widthMultiplier = lineWidth * (1 - size);
            Line(size * rippleSize);

        } else {

            if (size >= 1 + delay) {
                size -= 1 + delay;
                ResetCurve();
            } else {
                coll.radius = 0;
            }

        }
    }

    LineRenderer NewLine()
    {
        LineRenderer lineRenderer;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.material = material;
        lineRenderer.alignment = LineAlignment.TransformZ;
        lineRenderer.loop = true;
        lineRenderer.widthMultiplier = lineWidth;
        //lineRenderer.numCornerVertices = 1;

        lineRenderer.positionCount = numPosCount;
        //lineRenderer.SetPositions(new Vector3[] { new Vector3(1,0), new Vector3(0, 1), new Vector3(-1, 0), new Vector3(0, -1) });
        Line(lineRenderer, size * rippleSize);
        return lineRenderer;
    }

    void Line(float distMutliplier)
    {
        Line(lineRenderer, distMutliplier);
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
        /*
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
        */



        List<Keyframe> keys;
        if (lineRenderer.widthCurve.keys.Length == 1) {
            keys = new List<Keyframe>();

        } else {
            keys = new List<Keyframe>(lineRenderer.widthCurve.keys);
        }

        //fixes issues whnen trying to create a hole inside a hole.
        //bool angle1 = true, angle2 = true;
        for (int i = 0; i < keys.Count; i++) {
            if (IsBetwin(keys[i].time, angle, hitSize)) {
                keys.RemoveAt(i);
                i--;
            }
            /*
            else if (IsBetwin(keys[i].time, angle, hitSize + smoothSize)) {
                
                    angle1 = false;
                
                    angle2 = false;
                
            }
            */

        }

        //set value 0 for bottom
        keys.AddRange(new Keyframe[] {
            NewKeyframe(angle - hitSize, 0),
            NewKeyframe(angle + hitSize, 0)
        });

        //set value 1 for slope
        float v = (angle - hitSize - smoothSize) % 1f;
        if (lineRenderer.widthCurve.Evaluate(v) == 1)
            keys.Add(NewKeyframeRaw(v, 1));
        v = (angle + hitSize + smoothSize) % 1f;
        if (lineRenderer.widthCurve.Evaluate(v) == 1)
            keys.Add(NewKeyframeRaw(v, 1));

        //cleen up, remve single tops
        /*
        for (int i = 1; i < keys.Count - 1; i++) {
            if (keys[i].value == 1 && 
                keys[i - 1].value == 0 && 
                keys[i + 1].value == 0) {
                keys.RemoveAt(i);
                i--;
            }
        }
        */

        //cleen up, remove unnessesary keys

        for (int i = 1; i < keys.Count - 1; i++) {
            if (keys[i].value == 0 && keys[i - 1].value == 0 && keys[i + 1].value == 0) {
                keys.RemoveAt(i);
                i--;
            }
        }


        AnimationCurve ac = new AnimationCurve();
        ac.keys = keys.ToArray();
        lineRenderer.widthCurve = ac;

    }

    void ResetCurve()
    {
        AnimationCurve ac = new AnimationCurve();
        ac.keys = new Keyframe[] { NewKeyframeRaw(0, 1) };
        lineRenderer.widthCurve = ac;
    }

    Keyframe NewKeyframe(float time, float value)
    {
        return NewKeyframeRaw(time % 1f, value);
    }

    Keyframe NewKeyframeRaw(float time, float value)
    {
        Keyframe k = new Keyframe(time, value, 0, 0);
        k.weightedMode = WeightedMode.None;
        return k;
    }

    bool IsBetwin(float target, float baseValue, float diff)
    {
        return target > baseValue - diff && target < baseValue + diff;
    }
}
