using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private GameObject player;
    public int damage;
    
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == player.name)
        {
            player.GetComponent<TakeDamage>().takeDamage(1, 5);
            StartCoroutine("Immunity");
        }
    }

    IEnumerator Immunity()
    {
        //Make invunrable
        Physics2D.IgnoreLayerCollision(10, 11, true);
        Debug.Log("invunrable!!!");
        yield return new WaitForSeconds(3f);
        //Make vaunrable 
        Physics2D.IgnoreLayerCollision(10, 11, false);
        Debug.Log("vunrable!!!!");
    }
}
