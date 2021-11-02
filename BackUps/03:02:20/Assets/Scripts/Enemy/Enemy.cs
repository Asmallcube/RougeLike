using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health;
    public float startHealth;

    private float timer;

    public float startTimer;

    public GameObject bloodSplatter;

    public GameObject bloodSplat;

    public GameObject feet;

    // Start is called before the first frame update
    void Start()
    {
        health = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Instantiate(bloodSplatter, transform.position, Quaternion.identity);
            Instantiate(bloodSplat, feet.transform.position, Quaternion.identity);
            if (timer <= 0)
            {
                Destroy(gameObject);
                
            }
            else
            {
                timer = timer - Time.deltaTime;
            }
           
        }
    }

    public void TakeDamage(int damage)
    {
        //Instantiate(//effect here//,
            //transform.position, Quaternion.identity);
        health -= damage;
    }
}
