using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichThrowable : MonoBehaviour
{
    void Update()
    {
        if(transform.position.y <= -8) Destroy(gameObject);
    }
}
