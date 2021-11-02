using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{

    public void takeDamage(int damage, int knockBackForce)
    {
        GetComponent<Hearts>().health -= damage;
        GetComponent<PlayerController>().knockBack(knockBackForce);
    }

    
}
