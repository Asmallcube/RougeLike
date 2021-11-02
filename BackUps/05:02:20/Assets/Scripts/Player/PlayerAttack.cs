using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public float attackRange;

    public LayerMask whatIsEnemy;

    public int damage;

    public GameObject mainCamera;

    public Animator playerAnim;

    public CameraShake cameraShake;
    private void Update()
    {
        if (timeBtwAttack >= 0)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {

                mainCamera.GetComponent<Shake>().CamShake();
                playerAnim.SetTrigger("attack");

                //StartCoroutine(cameraShake.Shake(0.15f, 0.4f));
         
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);

                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }

                timeBtwAttack = startTimeBtwAttack;
            }
        } else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
