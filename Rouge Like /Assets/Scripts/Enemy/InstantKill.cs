using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKill : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == player.name)
        {
            player.GetComponent<TakeDamage>().takeDamage(player.GetComponent<Hearts>().numberOfHearts, 0);

        }
    }

}
