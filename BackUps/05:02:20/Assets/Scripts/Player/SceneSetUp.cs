using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(10, 11, false);
        Physics2D.IgnoreLayerCollision(9, 11, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
