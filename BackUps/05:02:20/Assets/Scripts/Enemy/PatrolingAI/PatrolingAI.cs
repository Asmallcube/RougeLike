using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolingAI : MonoBehaviour
{
     public float speed;
    public float distance;

    private bool moivingRight = true;

    public Transform groundDetection;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
             if (moivingRight == true)
             {
                transform.eulerAngles = new Vector3(0, -180, 0);
                moivingRight = false;
             } else
             {
                transform.eulerAngles = new Vector3(0, 0, 0);
                moivingRight = true;
            }
        }
    }
}
