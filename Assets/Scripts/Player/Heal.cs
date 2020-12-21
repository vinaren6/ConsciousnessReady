using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField]
    private int healAmount = 5;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Health player = other.gameObject.GetComponent<Health>();

        if (player != null)
            player.Heal(healAmount);
    }
}
