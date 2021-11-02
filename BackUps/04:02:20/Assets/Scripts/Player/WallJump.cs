using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// NOT IN USE!!!!!!!!
/// NOT IN USE!!!!!!!!
/// NOT IN USE!!!!!!!!

public class WallJump : MonoBehaviour
{
    private PlayerController movement;

    private Vector3 scale;

    public float speed;


    public float distance = 1f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        

        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

        if (Input.GetKeyDown(KeyCode.Space) && !movement.isGrounded && hit.collider != null)
        {
            scale = GetComponent<Transform>().localScale;

            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * hit.normal.x,speed);

            transform.localScale = transform.localScale.x == 1 ? new Vector2(-1, 1) : Vector2.one;


            rb.velocity = new Vector2(speed * hit.normal.x * movement.speed, rb.velocity.y);

            GetComponent<Transform>().localScale = scale;
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);


    }
}
