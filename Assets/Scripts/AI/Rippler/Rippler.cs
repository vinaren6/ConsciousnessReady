using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Rippler : MonoBehaviour
{
    [SerializeField]
    Material material;

    [SerializeField]
    int numPosCount = 32;
    [SerializeField]
    int numAddisonalPos = 32;

    [SerializeField, Range(0f, 1f)]
    float hitSize = 0.05f;
    [SerializeField, Range(0f, 1f)]
    float smoothSize = 0.01f;

    [SerializeField]
    float lineWidth = 0.075f;
    [SerializeField]
    float rippleSize = 5f;
    [SerializeField]
    float time = 15f;
    [SerializeField]
    float delay = 0.5f;

    [SerializeField]
    float smoothing = 1f;

    [SerializeField]
    float pussingForce = 5f;

    [SerializeField]
    int damage = 50;



    LineRenderer lineRenderer;

    float size = 0;

    CircleCollider2D coll;

    AnimationCurve actualCurve;

    Animator animator;

    void Start()
    {
        lineRenderer = NewLine();
        coll = GetComponent<CircleCollider2D>();
        animator = gameObject.transform.parent.GetComponent<Animator>();
        ResetCurve();
    }

    void FixedUpdate()
    {

        size += Time.fixedDeltaTime / time;
        if (size < 1) {

            coll.radius = size * rippleSize;
            lineRenderer.widthMultiplier = lineWidth * (1 - size);
            Line(size * rippleSize);
            Smooth();

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
        lineRenderer.positionCount = numPosCount + (int)(numAddisonalPos * size);
        Vector3[] poss = new Vector3[lineRenderer.positionCount];
        for (int i = 0; i < lineRenderer.positionCount; i++) {
            float ang = (float)i / lineRenderer.positionCount * Mathf.PI * 2f;
            poss[i] = new Vector3(Mathf.Sin(ang) * distMutliplier, Mathf.Cos(ang) * distMutliplier);
        }
        lineRenderer.SetPositions(poss);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 hit = collision.ClosestPoint(transform.position);
        hit -= pos;
        float orgAngle = Mathf.Atan2(hit.y, hit.x);
        float angle = 360f - Mathf.Atan2(hit.y, hit.x) * Mathf.Rad2Deg;
        angle += 90f;
        angle %= 360f;
        angle /= 360f;


        //if linw width is less than 0.75%
        if (lineRenderer.widthCurve.Evaluate(angle) > 0.75f) {
            //hit
            if (collision.transform.tag == "Player") {

                //apply force
                Rigidbody2D collRB2D;
                if (collision.TryGetComponent<Rigidbody2D>(out collRB2D)) {
                    //get angle as positive and inside 360* / 2PI
                    float radAngle = (orgAngle + Mathf.PI) % (Mathf.PI * 2f);
                    collRB2D.AddForce(new Vector2(Mathf.Sin(radAngle) * pussingForce, Mathf.Cos(radAngle)) * pussingForce, ForceMode2D.Impulse);
                }

                //apply damage
                Health player;
                if (collision.TryGetComponent<Health>(out player))
                    player.TakeDamage(damage);
            }
        }


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
        for (int i = 0; i < keys.Count; i++) {
            if (IsBetwin(keys[i].time, angle, hitSize)) {
                keys.RemoveAt(i);
                i--;
            }
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

        //cleen up, remove unnessesary keys
        for (int i = 1; i < keys.Count - 1; i++) {
            if (keys[i].value == 0 && keys[i - 1].value == 0 && keys[i + 1].value == 0) {
                keys.RemoveAt(i);
                i--;
            }
        }


        actualCurve = new AnimationCurve(keys.ToArray());

        for (int i = 0; i < keys.Count; i++) {
            keys[i] = NewKeyframeRaw(keys[i].time, lineRenderer.widthCurve.Evaluate(keys[i].time));
        }

        lineRenderer.widthCurve = new AnimationCurve(keys.ToArray());


    }

    void Smooth()
    {
        Keyframe[] keys = actualCurve.keys;
        for (int i = 0; i < keys.Length; i++) {
            if (!keys[i].value.Equals(1))
                keys[i] = NewKeyframeRaw(keys[i].time, Mathf.Max(lineRenderer.widthCurve.keys[i].value - Time.fixedDeltaTime / smoothing, 0));
        }

        lineRenderer.widthCurve = new AnimationCurve(keys);
    }

    void ResetCurve()
    {
        Keyframe[] keys = new Keyframe[] { NewKeyframeRaw(0, 1) };
        actualCurve = new AnimationCurve(keys);
        lineRenderer.widthCurve = new AnimationCurve(keys);

        //animator.Play("Ripple");
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
