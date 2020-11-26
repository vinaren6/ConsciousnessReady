using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{


    [SerializeField]
    private Behavor behavor;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float maxSpeed = 5.5f;
    [SerializeField]
    private float acceleration = 2f;
    Rigidbody2D rb2d;

    [SerializeField]
    Transform target;
    Rigidbody2D targetRB2D;

    [SerializeField]
    private float maxDistance = 6.5f;

    private Quaternion angle1, angle2;

    private float turning;
    [SerializeField]
    private float turnSpeed = 20f;

    [SerializeField]
    private float targetDistanceMultiplier = 0.1f;

    private Vector3 lastPos = new Vector3();

    enum Behavor
    {
        Hunt,
        Run
    }

    private void Awake()
    {
        Physics2D.queriesStartInColliders = false;
        angle1 = Quaternion.AngleAxis(-15, new Vector3(0, 0, 1));
        angle2 = Quaternion.AngleAxis(15, new Vector3(0, 0, 1));

    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        targetRB2D = target.GetComponent<Rigidbody2D>();

        Time.timeScale = 5f;
    }


    private void FixedUpdate()
    {
        float dist = 1;

        if (lastPos == transform.position)
            turning += 180;
        else
            lastPos = transform.position;

        if (behavor == Behavor.Run) {
            Vector3 dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle += 90;
            transform.rotation = Quaternion.AngleAxis(angle + turning, Vector3.forward);
        } else {
            dist = Vector3.Distance(target.position, transform.position);
            Vector3 dir = target.position + targetDistanceMultiplier * dist * new Vector3(targetRB2D.velocity.x, targetRB2D.velocity.y) - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle -= 90;
            transform.rotation = Quaternion.AngleAxis(angle + turning, Vector3.forward);

        }

        bool hits = false;

        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, angle1 * transform.up, maxDistance, layerMask);
        if (hit1.distance != 0) {
            hits = true;
#if UNITY_EDITOR
            Debug.DrawRay(transform.position, angle1 * transform.up * hit1.distance, Color.yellow);
#endif
        } else {
            hit1.distance = maxDistance;
#if UNITY_EDITOR
            Debug.DrawRay(transform.position, angle1 * transform.up * maxDistance, Color.green);
#endif
        }
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, angle2 * transform.up, maxDistance, layerMask);
        if (hit2.distance != 0) {
            hits = true;
#if UNITY_EDITOR
            Debug.DrawRay(transform.position, angle2 * transform.up * hit2.distance, Color.yellow);
#endif
        } else {
            hit2.distance = maxDistance;
#if UNITY_EDITOR
            Debug.DrawRay(transform.position, angle2 * transform.up * maxDistance, Color.green);
#endif
        }
        float speedMultiplier = 1;
        if (hits) {
            if (hit1.distance < hit2.distance) {
                speedMultiplier = hit1.distance * 1.2f / maxDistance - 0.2f;
                turning += Time.fixedDeltaTime * turnSpeed;
                if (turning > 360)
                    turning -= 360;
            } else {
                speedMultiplier = Mathf.Min(hit2.distance * 3f / maxDistance - 0.5f, 1f);
                turning -= Time.fixedDeltaTime * turnSpeed;
                if (turning < -360)
                    turning += 360;
            }
        } else {

            if (turning > 180) {
                turning += Time.fixedDeltaTime * turnSpeed;
                if (turning > 360)
                    turning -= 360;
            } else if (turning < -180) {
                turning -= Time.fixedDeltaTime * turnSpeed;
                if (turning < -360)
                    turning += 360;
            } else

                turning -= Mathf.Clamp(turning * 0.05f, -Time.fixedDeltaTime * turnSpeed, Time.fixedDeltaTime * turnSpeed);
        }

        if (behavor == Behavor.Hunt)
            speedMultiplier *= Mathf.Min(dist - 1.5f, 1f);

        if (rb2d.velocity.magnitude < maxSpeed) {
            rb2d.AddForce(transform.up * acceleration * speedMultiplier * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }

    }

}
