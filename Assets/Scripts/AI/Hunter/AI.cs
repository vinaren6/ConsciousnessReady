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

    [Space(10)]
    [Header("Guns")]
    [SerializeField]
    float fireDelay = 3f;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform[] guns;

    float activeFireDelay = 1f;
    bool skip = false;

    [System.Flags]
    enum Behavor
    {
        Nothing = 0,
        Hunt = 1 << 0,
        Stationary = 1 << 1,
        Fire = 1 << 2,
        Kamikaze = 1 << 3,
        Everything = ~0
    }

    private void Awake()
    {
        //Physics2D.queriesStartInColliders = false;
        angle1 = Quaternion.AngleAxis(-15, new Vector3(0, 0, 1));
        angle2 = Quaternion.AngleAxis(15, new Vector3(0, 0, 1));

        rb2d = GetComponent<Rigidbody2D>();
        targetRB2D = target.GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        float dist = 1f;
        skip = false;
        //rotate baseed on targets position
        if (behavor.HasFlag(Behavor.Hunt)) {
            dist = Vector3.Distance(target.position, transform.position);
            Vector3 dir = target.position + targetDistanceMultiplier * dist * new Vector3(targetRB2D.velocity.x, targetRB2D.velocity.y) - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle -= 90;
            transform.rotation = Quaternion.AngleAxis(angle + turning, Vector3.forward);
        } else {
            Vector3 dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle += 90;
            transform.rotation = Quaternion.AngleAxis(angle + turning, Vector3.forward);
        }


        if (!behavor.HasFlag(Behavor.Stationary)) {

            //turn around if stuck
            if (lastPos == transform.position)
                turning += 180;
            else
                lastPos = transform.position;

            //avoid colition will wall
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
            // turn away from opsticle
            if (hits) {
                if (hit1.distance < hit2.distance) {
                    speedMultiplier = Mathf.Min(hit1.distance * 3f / maxDistance - 0.5f, 1f);
                    turning += Time.fixedDeltaTime * turnSpeed;
                } else {
                    speedMultiplier = Mathf.Min(hit2.distance * 3f / maxDistance - 0.5f, 1f);
                    turning -= Time.fixedDeltaTime * turnSpeed;
                }
            } else {

                if (turning > 180)
                    turning += Time.fixedDeltaTime * turnSpeed;
                else if (turning < -180)
                    turning -= Time.fixedDeltaTime * turnSpeed;
                else
                    turning -= Mathf.Clamp(turning * 0.05f, -Time.fixedDeltaTime * turnSpeed, Time.fixedDeltaTime * turnSpeed);
            }
            turning %= 360;

            if (!behavor.HasFlag(Behavor.Kamikaze) && behavor.HasFlag(Behavor.Hunt)) {
                //dont ram the target
                speedMultiplier *= Mathf.Min(dist - 1.75f, 1f);
            }

            //apply acceleration
            if (rb2d.velocity.magnitude < maxSpeed) {
                rb2d.AddForce(transform.up * acceleration * speedMultiplier * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }

        }

        if (behavor.HasFlag(Behavor.Fire)) {
            //fire at target
            activeFireDelay -= Time.fixedDeltaTime;
            if (activeFireDelay < 0) {
                if ((turning < 45 || turning < -315) && (turning > -45 || turning > 315)) {
                    RaycastHit2D hitFire = Physics2D.Raycast(transform.position, transform.up, dist, layerMask);
                    if (hitFire.distance == 0) {
                        activeFireDelay = fireDelay;
                        skip = true;
                        foreach (Transform gun in guns) {
                            Instantiate(bullet, gun.position, gun.rotation);
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy Bullet") {
            if (!skip)
                Destroy(collision.gameObject);
        }
    }


}
